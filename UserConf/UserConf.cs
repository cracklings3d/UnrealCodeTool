using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Utilities;

using UCT.Util;

namespace UCT;

public enum EngineInstallValidationError {
  None,
  LocationDoesNotExist,
  NotAnEngineRoot,
}

[JsonObject("engine_install")]
public class EngineInstall {
  [JsonProperty("root_location")] public string  RootLocation { get; set; } = "";
  [JsonProperty("version")]       public Version Version      { get; set; }
  // public Either<Version, Guid> Version      { get; set; }

  public EngineInstallValidationError Validate() {
    if (!Directory.Exists(RootLocation)) return EngineInstallValidationError.LocationDoesNotExist;
    if (!File.Exists(Path.Combine(RootLocation, "Engine", "Binaries", "Win64", "UE4Editor.exe"))
     && !File.Exists(Path.Combine(RootLocation, "Engine", "Binaries", "Win64", "UnrealEditor.exe")))
      return EngineInstallValidationError.NotAnEngineRoot;
    return EngineInstallValidationError.None;
  }
}

[JsonObject("user_conf")]
public class UserConf {
  [JsonProperty("engine_installs")] public List<EngineInstall> EngineInstalls { get; set; }
  [JsonProperty("svn_path")]        public string              SvnPath        { get; set; }

  [JsonProperty("svn_engine_plugin_repos")]
  public string SvnEnginePluginRepos { get; set; }

  [JsonProperty("svn_project_plugin_repos")]
  public string SvnProjectPluginRepos { get; set; }

  public UserConf() {
    EngineInstalls        = new List<EngineInstall>();
    SvnPath               = "";
    SvnEnginePluginRepos  = "";
    SvnProjectPluginRepos = "";
  }

  public static UserConf? Load() {
    return LoadUserConf();
  }

  private static readonly string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

  private static readonly string configFilePath = Path.Combine(homeDir, "UnrealCodeTool", "user.conf");

  public static void SaveUserConf(UserConf? userConf) {
    if (userConf == null) return;
    string json = JsonConvert.SerializeObject(userConf, Formatting.Indented);
    File.WriteAllText(configFilePath, json);
  }

  public static UserConf? LoadUserConf() {
    if (!File.Exists(configFilePath)) return new UserConf();
    string json = File.ReadAllText(configFilePath);
    return JsonConvert.DeserializeObject<UserConf>(json);
  }
}
