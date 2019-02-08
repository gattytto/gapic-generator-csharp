// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: google/api/resources.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Google.Api {

  /// <summary>Holder for reflection information generated from google/api/resources.proto</summary>
  public static partial class ResourcesReflection {

    #region Descriptor
    /// <summary>File descriptor for google/api/resources.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ResourcesReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Chpnb29nbGUvYXBpL3Jlc291cmNlcy5wcm90bxIKZ29vZ2xlLmFwaRogZ29v",
            "Z2xlL3Byb3RvYnVmL2Rlc2NyaXB0b3IucHJvdG8iJgoIUmVzb3VyY2USDAoE",
            "cGF0aBgBIAEoCRIMCgRuYW1lGAIgASgJImEKC1Jlc291cmNlU2V0EgwKBG5h",
            "bWUYASABKAkSJwoJcmVzb3VyY2VzGAIgAygLMhQuZ29vZ2xlLmFwaS5SZXNv",
            "dXJjZRIbChNyZXNvdXJjZV9yZWZlcmVuY2VzGAMgAygJQmUKDmNvbS5nb29n",
            "bGUuYXBpQg5SZXNvdXJjZXNQcm90b1ABWkFnb29nbGUuZ29sYW5nLm9yZy9n",
            "ZW5wcm90by9nb29nbGVhcGlzL2FwaS9hbm5vdGF0aW9uczthbm5vdGF0aW9u",
            "c2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { pbr::FileDescriptor.DescriptorProtoFileDescriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Google.Api.Resource), global::Google.Api.Resource.Parser, new[]{ "Path", "Name" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Google.Api.ResourceSet), global::Google.Api.ResourceSet.Parser, new[]{ "Name", "Resources", "ResourceReferences" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// An annotation designating that this field designates a One Platform
  /// resource.
  /// </summary>
  public sealed partial class Resource : pb::IMessage<Resource> {
    private static readonly pb::MessageParser<Resource> _parser = new pb::MessageParser<Resource>(() => new Resource());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Resource> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Api.ResourcesReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Resource() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Resource(Resource other) : this() {
      path_ = other.path_;
      name_ = other.name_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Resource Clone() {
      return new Resource(this);
    }

    /// <summary>Field number for the "path" field.</summary>
    public const int PathFieldNumber = 1;
    private string path_ = "";
    /// <summary>
    /// Required. The resource's path. This is usually in a form such as:
    ///   projects/{project_id}/things/{thing_id}
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Path {
      get { return path_; }
      set {
        path_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 2;
    private string name_ = "";
    /// <summary>
    /// The colloquial name of the resource.
    /// If omitted, this is the name of the message.
    /// This is required if the resource is within a ResourceSet (see below).
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Resource);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Resource other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Path != other.Path) return false;
      if (Name != other.Name) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Path.Length != 0) hash ^= Path.GetHashCode();
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Path.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Path);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Path.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Path);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Resource other) {
      if (other == null) {
        return;
      }
      if (other.Path.Length != 0) {
        Path = other.Path;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Path = input.ReadString();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  /// An annotation designating that this field designates a set of One Platform
  /// resources.
  /// </summary>
  public sealed partial class ResourceSet : pb::IMessage<ResourceSet> {
    private static readonly pb::MessageParser<ResourceSet> _parser = new pb::MessageParser<ResourceSet>(() => new ResourceSet());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ResourceSet> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Api.ResourcesReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ResourceSet() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ResourceSet(ResourceSet other) : this() {
      name_ = other.name_;
      resources_ = other.resources_.Clone();
      resourceReferences_ = other.resourceReferences_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ResourceSet Clone() {
      return new ResourceSet(this);
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    /// <summary>
    /// The colloquial name of the resource.
    /// If omitted, this is the name of the message.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "resources" field.</summary>
    public const int ResourcesFieldNumber = 2;
    private static readonly pb::FieldCodec<global::Google.Api.Resource> _repeated_resources_codec
        = pb::FieldCodec.ForMessage(18, global::Google.Api.Resource.Parser);
    private readonly pbc::RepeatedField<global::Google.Api.Resource> resources_ = new pbc::RepeatedField<global::Google.Api.Resource>();
    /// <summary>
    /// Component resources that are part of the set.
    /// Resources declared within a resource set must have `name` set.
    ///
    /// The final set of resources in the resource set is the union of
    /// `resources` and `resource_references`.
    ///
    /// Resources defined here are only scoped within the parent ResourceSet;
    /// i.e. other messages cannot reference these contained Resources by name.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Google.Api.Resource> Resources {
      get { return resources_; }
    }

    /// <summary>Field number for the "resource_references" field.</summary>
    public const int ResourceReferencesFieldNumber = 3;
    private static readonly pb::FieldCodec<string> _repeated_resourceReferences_codec
        = pb::FieldCodec.ForString(26);
    private readonly pbc::RepeatedField<string> resourceReferences_ = new pbc::RepeatedField<string>();
    /// <summary>
    /// References to existing resources (messages of resource definitions)
    /// that are part of the set.
    ///
    /// These may be specified as fully-qualified (e.g. "google.pubsub.v1.Topic")
    /// or just the resource/proto name if it is defined within the same package.
    ///
    /// The final set of resources in the resource set is the union of
    /// `resources` and `resource_references`.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<string> ResourceReferences {
      get { return resourceReferences_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ResourceSet);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ResourceSet other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if(!resources_.Equals(other.resources_)) return false;
      if(!resourceReferences_.Equals(other.resourceReferences_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      hash ^= resources_.GetHashCode();
      hash ^= resourceReferences_.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      resources_.WriteTo(output, _repeated_resources_codec);
      resourceReferences_.WriteTo(output, _repeated_resourceReferences_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      size += resources_.CalculateSize(_repeated_resources_codec);
      size += resourceReferences_.CalculateSize(_repeated_resourceReferences_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ResourceSet other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      resources_.Add(other.resources_);
      resourceReferences_.Add(other.resourceReferences_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 18: {
            resources_.AddEntriesFrom(input, _repeated_resources_codec);
            break;
          }
          case 26: {
            resourceReferences_.AddEntriesFrom(input, _repeated_resourceReferences_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code