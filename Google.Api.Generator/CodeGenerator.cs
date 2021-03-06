﻿// Copyright 2018 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Google.Api.Gax;
using Google.Api.Generator.Utils.Formatting;
using Google.Api.Generator.Generation;
using Google.Api.Generator.ProtoUtils;
using Google.Api.Generator.Utils;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Grpc.ServiceConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Google.Api.Generator
{
    internal static class CodeGenerator
    {
        private static readonly IReadOnlyDictionary<string, string> s_wellknownNamespaceAliases = new Dictionary<string, string>
            {
                { typeof(System.Int32).Namespace, "sys" }, // Don't use "s"; one-letter aliases cause a compilation error!
                { typeof(System.Net.WebUtility).Namespace, "sysnet" },
                { typeof(System.Collections.Generic.IEnumerable<>).Namespace, "scg" },
                { typeof(System.Collections.ObjectModel.Collection<>).Namespace, "sco" },
                { typeof(System.Linq.Enumerable).Namespace, "linq" },
                { typeof(Google.Api.Gax.Expiration).Namespace, "gax" },
                { typeof(Google.Api.Gax.Grpc.CallSettings).Namespace, "gaxgrpc" },
                { typeof(Google.Api.Gax.Grpc.GrpcCore.GrpcCoreAdapter).Namespace, "gaxgrpccore" },
                { typeof(Grpc.Core.CallCredentials).Namespace, "grpccore" },
                { typeof(Grpc.Core.Interceptors.Interceptor).Namespace, "grpcinter" },
                { typeof(Google.Protobuf.WellKnownTypes.Any).Namespace, "wkt" },
                { typeof(Google.LongRunning.Operation).Namespace, "lro" },
                { typeof(Google.Protobuf.ByteString).Namespace, "proto" },
                { typeof(Moq.Mock).Namespace, "moq" },
                { typeof(Xunit.Assert).Namespace, "xunit" },
            };

        public static IEnumerable<ResultFile> Generate(byte[] descriptorBytes, string package, IClock clock,
            string grpcServiceConfigPath, IEnumerable<string> commonResourcesConfigPaths)
        {
            var descriptors = GetFileDescriptors(descriptorBytes);
            var filesToGenerate = descriptors.Where(x => x.Package == package).Select(x => x.Name).ToList();
            return Generate(descriptors, filesToGenerate, clock, grpcServiceConfigPath, commonResourcesConfigPaths);
        }

        public static IEnumerable<ResultFile> Generate(IReadOnlyList<FileDescriptor> descriptors, IEnumerable<string> filesToGenerate, IClock clock,
            string grpcServiceConfigPath, IEnumerable<string> commonResourcesConfigPaths)
        {
            // Load side-loaded configurations; both optional.
            var grpcServiceConfig = grpcServiceConfigPath != null ? ServiceConfig.Parser.ParseJson(File.ReadAllText(grpcServiceConfigPath)) : null;
            var commonResourcesConfigs = commonResourcesConfigPaths != null ?
                commonResourcesConfigPaths.Select(path => CommonResources.Parser.ParseJson(File.ReadAllText(path))) : null;
            // TODO: Multi-package support not tested.
            var filesToGenerateSet = filesToGenerate.ToHashSet();
            var byPackage = descriptors.Where(x => filesToGenerateSet.Contains(x.Name)).GroupBy(x => x.Package).ToList();
            if (byPackage.Count == 0)
            {
                throw new InvalidOperationException("No packages specified to build.");
            }
            foreach (var singlePackageFileDescs in byPackage)
            {
                var namespaces = singlePackageFileDescs.Select(x => x.CSharpNamespace()).Distinct().ToList();
                if (namespaces.Count > 1)
                {
                    throw new InvalidOperationException(
                        "All files in the same package must have the same C# namespace. " +
                        $"Found namespaces '{string.Join(", ", namespaces)}' in package '{singlePackageFileDescs.Key}'.");
                }
                var catalog = new ProtoCatalog(singlePackageFileDescs.Key, descriptors, commonResourcesConfigs);
                foreach (var resultFile in GeneratePackage(namespaces[0], singlePackageFileDescs, catalog, clock, grpcServiceConfig))
                {
                    yield return resultFile;
                }
            }
        }

        private static IEnumerable<ResultFile> GeneratePackage(string ns, IEnumerable<FileDescriptor> packageFileDescriptors, ProtoCatalog catalog, IClock clock,
            ServiceConfig grpcServiceConfig)
        {
            var clientPathPrefix = $"{ns}{Path.DirectorySeparatorChar}";
            var snippetsPathPrefix = $"{ns}.Snippets{Path.DirectorySeparatorChar}";
            var unitTestsPathPrefix = $"{ns}.Tests{Path.DirectorySeparatorChar}";
            bool hasLro = false;
            bool hasContent = false;
            foreach (var fileDesc in packageFileDescriptors)
            {
                foreach (var service in fileDesc.Services)
                {
                    // Generate settings and client code for requested package.
                    var serviceDetails = new ServiceDetails(catalog, ns, service, grpcServiceConfig);
                    var ctx = SourceFileContext.CreateFullyAliased(clock, s_wellknownNamespaceAliases);
                    var code = ServiceCodeGenerator.Generate(ctx, serviceDetails);
                    var filename = $"{clientPathPrefix}{serviceDetails.ClientAbstractTyp.Name}.g.cs";
                    yield return new ResultFile(filename, code);
                    // Generate snippets for the service
                    var snippetCtx = SourceFileContext.CreateUnaliased(clock);
                    var snippetCode = SnippetCodeGenerator.Generate(snippetCtx, serviceDetails);
                    var snippetFilename = $"{snippetsPathPrefix}{serviceDetails.ClientAbstractTyp.Name}Snippets.g.cs";
                    yield return new ResultFile(snippetFilename, snippetCode);
                    // Generate unit tests for the the service.
                    var unitTestCtx = SourceFileContext.CreateFullyAliased(clock, s_wellknownNamespaceAliases);
                    var unitTestCode = UnitTestCodeGeneration.Generate(unitTestCtx, serviceDetails);
                    var unitTestFilename = $"{unitTestsPathPrefix}{serviceDetails.ClientAbstractTyp.Name}Test.g.cs";
                    yield return new ResultFile(unitTestFilename, unitTestCode);
                    // Record whether LRO is used.
                    hasLro |= serviceDetails.Methods.Any(x => x is MethodDetails.Lro);
                    hasContent = true;
                }
                var resCtx = SourceFileContext.CreateFullyAliased(clock, s_wellknownNamespaceAliases);
                var (resCode, resCodeClassCount) = ResourceNamesGenerator.Generate(catalog, resCtx, fileDesc);
                // Only produce an output file if it contains >0 [partial] classes.
                if (resCodeClassCount > 0)
                {
                    var filenamePrefix = Path.GetFileNameWithoutExtension(fileDesc.Name).ToUpperCamelCase();
                    var resFilename = $"{clientPathPrefix}{filenamePrefix}ResourceNames.g.cs";
                    yield return new ResultFile(resFilename, resCode);
                    hasContent = true;
                }
            }
            // Only output csproj's if there is any other generated content.
            // When processing a (proto) package without any services there will be no generated content.
            if (hasContent)
            {
                // Generate client csproj.
                var csprojContent = CsProjGenerator.GenerateClient(hasLro);
                var csprojFilename = $"{clientPathPrefix}{ns}.csproj";
                yield return new ResultFile(csprojFilename, csprojContent);
                // Generate snippets csproj.
                var snippetsCsprojContent = CsProjGenerator.GenerateSnippets(ns);
                var snippetsCsProjFilename = $"{snippetsPathPrefix}{ns}.Snippets.csproj";
                yield return new ResultFile(snippetsCsProjFilename, snippetsCsprojContent);
                // Generate unit-tests csproj.
                var unitTestsCsprojContent = CsProjGenerator.GenerateUnitTests(ns);
                var unitTestsCsprojFilename = $"{unitTestsPathPrefix}{ns}.Tests.csproj";
                yield return new ResultFile(unitTestsCsprojFilename, unitTestsCsprojContent);
            }
        }

        private static IReadOnlyList<FileDescriptor> GetFileDescriptors(byte[] bytes)
        {
            // TODO: Remove this when equivalent is in Protobuf.
            // Manually read repeated field of `FileDescriptorProto` messages.
            int i = 0;
            var bss = new List<ByteString>();
            while (i < bytes.Length)
            {
                if (bytes[i] != 0xa)
                {
                    throw new InvalidOperationException($"Expected 0xa at offset {i}");
                }
                i += 1;
                int len = 0;
                int shift = 0;
                while (true)
                {
                    var b = bytes[i];
                    i += 1;
                    len |= (b & 0x7f) << shift;
                    shift += 7;
                    if ((b & 0x80) == 0)
                    {
                        break;
                    }
                }
                bss.Add(ByteString.CopyFrom(bytes, i, len));
                i += len;
            }
            return FileDescriptor.BuildFromByteStrings(bss);
        }
    }
}
