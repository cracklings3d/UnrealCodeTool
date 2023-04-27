using Newtonsoft.Json;

namespace EngineInsight;

// {
//   "TargetName": "UnrealEditor",
//   "Platform": "Win64",
//   "Configuration": "Development",
//   "TargetType": "Editor",
//   "IsTestTarget": false,
//   "Architecture": "",
//   "Launch": "$(EngineDir)/Binaries/Win64/UnrealEditor.exe",
//   "LaunchCmd": "$(EngineDir)/Binaries/Win64/UnrealEditor-Cmd.exe",
//   "Version":
//   {
//     "MajorVersion": 5,
//     "MinorVersion": 1,
//     "PatchVersion": 1,
//     "Changelist": 23901901,
//     "CompatibleChangelist": 23058290,
//     "IsLicenseeVersion": 0,
//     "IsPromotedBuild": 1,
//     "BranchName": "++UE5+Release-5.1",
//     "BuildId": "23058290"
//   },
//   "BuildProducts": [
//     {
//       "Path": "$(EngineDir)/Binaries/ThirdParty/USD/UsdResources/Win64/plugins/ar/resources/plugInfo.json",
//       "Type": "RequiredResource"
//     },
//     {
//       "Path": "$(EngineDir)/Binaries/ThirdParty/USD/UsdResources/Win64/plugins/ndr/resources/plugInfo.json",
//       "Type": "RequiredResource"
//     },
//   ]
// } 

public enum BuildPlatform {
  Android,
  DotNET,
  IOS,
  Linux,
  LinuxArm64,
  Mac,
  Win64,
}

public enum BuildConfiguration {
  Debug,
  Development,
  Test,
  Shipping
}

// TODO: Incomplete due to lack of knowledge
public enum BuildTargetType {
  Game,
  Editor,
  Client,
  Server,
}

[JsonObject]
public class BuildVersion {
  [JsonProperty] public int    MajorVersion         { get; set; }
  [JsonProperty] public int    MinorVersion         { get; set; }
  [JsonProperty] public int    PatchVersion         { get; set; }
  [JsonProperty] public int    Changelist           { get; set; }
  [JsonProperty] public int    CompatibleChangelist { get; set; }
  [JsonProperty] public int    IsLicenseeVersion    { get; set; }
  [JsonProperty] public int    IsPromotedBuild      { get; set; }
  [JsonProperty] public string BranchName           { get; set; } = "";
  [JsonProperty] public string BuildId              { get; set; } = "";
}

[JsonObject]
public class BuildProduct {
  [JsonProperty] public string Path { get; set; } = "";
  [JsonProperty] public string Type { get; set; } = "";
}

[JsonObject]
public class BuildTarget {
  [JsonProperty] public string             TargetName    { get; set; } = "";
  [JsonProperty] public BuildPlatform      Platform      { get; set; }
  [JsonProperty] public BuildConfiguration Configuration { get; set; }
  [JsonProperty] public BuildTargetType    TargetType    { get; set; }
  [JsonProperty] public bool               IsTestTarget  { get; set; }
  [JsonProperty] public string             Architecture  { get; set; } = "";
  [JsonProperty] public string             Launch        { get; set; } = "";
  [JsonProperty] public string             LaunchCmd     { get; set; } = "";
  [JsonProperty] public BuildVersion       Version       { get; set; } = new();
  [JsonProperty] public List<BuildProduct> BuildProducts { get; set; } = new();

  public Version get_version() {
    return new Version(Version.MajorVersion, Version.MinorVersion, Version.PatchVersion);
  }
}
