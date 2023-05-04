using System.Text.Json;

namespace UCT;

public enum EngineInstallValidationError {
  None,
  LocationDoesNotExist,
  NotAnEngineRoot,
}

public class EngineInstall {
  public string  RootLocation { get; set; } = "";
  public Version Version      { get; set; }
  // TODO: write serializer for Either<T,U>
  // public Either<Version, Guid> Version      { get; set; }

  public EngineInstallValidationError Validate() {
    if (!Directory.Exists(RootLocation)) return EngineInstallValidationError.LocationDoesNotExist;
    if (!File.Exists(Path.Combine(RootLocation, "Engine", "Binaries", "Win64", "UE4Editor.exe"))
     && !File.Exists(Path.Combine(RootLocation, "Engine", "Binaries", "Win64", "UnrealEditor.exe")))
      return EngineInstallValidationError.NotAnEngineRoot;
    return EngineInstallValidationError.None;
  }
}

public class UserConf {
  public List<EngineInstall> EngineInstalls        { get; set; }
  public string              SvnBinPath            { get; set; } = "C:/Program Files/TortoiseSVN/bin/svn.exe";
  public string              SvnEnginePluginRepos  { get; set; }
  public string              SvnProjectPluginRepos { get; set; }

  public UserConf() {
    EngineInstalls        = new List<EngineInstall>();
    SvnBinPath            = "";
    SvnEnginePluginRepos  = "";
    SvnProjectPluginRepos = "";
  }

  private static readonly string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

  private static readonly string configFilePath = Path.Combine(homeDir, "UnrealCodeTool", "user.conf");

  public static bool Validate(UserConf? userConf) { 
    if (userConf == null) return false;
    if (!File.Exists(userConf.SvnBinPath)) return false;
    return true;
  }

  public static void Save(UserConf? userConf) {
    if (userConf == null) return;
    string json = JsonSerializer.Serialize(userConf);
    File.WriteAllText(configFilePath, json);
  }

  public static UserConf? Load() {
    if (!File.Exists(configFilePath)) return new UserConf();
    string json = File.ReadAllText(configFilePath);
    return JsonSerializer.Deserialize<UserConf>(json);
  }
}
