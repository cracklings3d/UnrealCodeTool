using System.Text.Json;
using System.Text.Json.Serialization;

namespace EngineInsight;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BuildPlatform {
  Android,
  DotNET,
  IOS,
  Linux,
  LinuxArm64,
  Mac,
  Win64,
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BuildConfiguration {
  Debug,
  Development,
  Test,
  Shipping
}

// TODO: Incomplete due to lack of knowledge
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BuildTargetType {
  Game,
  Editor,
  Client,
  Server,
}

public class BuildVersion {
  public int    MajorVersion         { get; set; }
  public int    MinorVersion         { get; set; }
  public int    PatchVersion         { get; set; }
  public int    Changelist           { get; set; }
  public int    CompatibleChangelist { get; set; }
  public int    IsLicenseeVersion    { get; set; }
  public int    IsPromotedBuild      { get; set; }
  public string BranchName           { get; set; } = "";
  public string BuildId              { get; set; } = "";
}

public class BuildProduct {
  public string Path { get; set; } = "";
  public string Type { get; set; } = "";
}

[Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
public enum RuntimeDependencyType {
  UFS,
  NonUFS,
}

public class RuntimeDependency {
  public string                Path { get; set; } = "";
  public RuntimeDependencyType Type { get; set; }
}

public class Property {
  public string Name  { get; set; } = "";
  public string Value { get; set; } = "";
}

// [JsonObject(MemberSerialization.Fields)]
public class BuildTarget {
  public                                                  string                  TargetName           { get; set; }
  [JsonConverter(typeof(JsonStringEnumConverter))] public BuildPlatform           Platform             { get; set; }
  [JsonConverter(typeof(JsonStringEnumConverter))] public BuildConfiguration      Configuration        { get; set; }
  [JsonConverter(typeof(JsonStringEnumConverter))] public BuildTargetType         TargetType           { get; set; }
  public                                                  bool                    IsTestTarget         { get; set; }
  public                                                  string                  Architecture         { get; set; }
  public                                                  string                  Launch               { get; set; }
  public                                                  string                  LaunchCmd            { get; set; }
  public                                                  BuildVersion            Version              { get; set; }
  public                                                  List<BuildProduct>      BuildProducts        { get; set; }
  public                                                  List<RuntimeDependency> RuntimeDependencies  { get; set; }
  public                                                  List<Property>          AdditionalProperties { get; set; }

  // Update the parse() method to use the private constructor
  // public static BuildTarget? parse(string json) {
  //   var traceWriter = new MemoryTraceWriter();
  //   var va = JsonConvert.DeserializeObject<BuildTarget>(
  //       json,
  //       new JsonSerializerSettings {
  //         ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
  //         TraceWriter         = traceWriter
  //       }
  //   );
  //   Console.WriteLine(traceWriter.ToString());
  //   return va;
  // }
  public static BuildTarget? parse(string json) {
    var options = new JsonSerializerOptions {
      PropertyNameCaseInsensitive = true, // this makes the deserializer case-insensitive when matching property names
      AllowTrailingCommas =
          true, // this allows the JSON input to include trailing commas (which are technically invalid, but some libraries allow them)
    };
    var target = System.Text.Json.JsonSerializer.Deserialize<BuildTarget>(json, options);
    return target;
  }


  public Version get_version() {
    return new Version(Version.MajorVersion, Version.MinorVersion, Version.PatchVersion);
  }
}
