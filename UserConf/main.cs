using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Utilities;

using UCT.Util;

namespace UCT;

public enum EngineInstallValidationError {
  None,
  LocationDoesNotExist,
  NotAnEngineRoot,
}

public class EngineInstall {
  public string                RootLocation { get; set; } = "";
  public Either<Version, Guid> Version      { get; set; }

  public EngineInstallValidationError Validate() {
    if (!Directory.Exists(RootLocation)) return EngineInstallValidationError.LocationDoesNotExist;
    if (!File.Exists(Path.Combine(RootLocation, "Engine", "Binaries", "Win64", "UE4Editor.exe"))
     && !File.Exists(Path.Combine(RootLocation, "Engine", "Binaries", "Win64", "UnrealEditor.exe")))
      return EngineInstallValidationError.NotAnEngineRoot;
    return EngineInstallValidationError.None;
  }
}

public class UserConf {
  public List<EngineInstall> EngineInstall         { get; set; }
  public string              SvnPath               { get; set; }
  public string              SvnEnginePluginRepos  { get; set; }
  public string              SvnProjectPluginRepos { get; set; }

  public UserConf() {
    EngineInstall         = new List<EngineInstall>();
    SvnPath               = "";
    SvnEnginePluginRepos  = "";
    SvnProjectPluginRepos = "";
  }
}

public static class JsonHandler {
  private static readonly string configFileName = "uct.conf.json";
  private static readonly string configFilePath = Path.Combine(
      Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
      "UnrealCodeTool",
      configFileName
  );

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
