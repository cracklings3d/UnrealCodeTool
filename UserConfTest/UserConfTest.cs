using Newtonsoft.Json;
using UCT;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace UserConfTest;

public class UserConfTest {
  private readonly ITestOutputHelper _test_output_helper;

  public UserConfTest(ITestOutputHelper testOutputHelper) {
    _test_output_helper = testOutputHelper;
  }

  [Fact]
  public void Validation() {
    var invalid_engine_install = new EngineInstall {
        RootLocation = "C:/",
        Version = new Version(4, 27, 0)
    };
    Assert.Equal(EngineInstallValidationError.NotAnEngineRoot, invalid_engine_install.Validate());
    // user_conf.EngineInstalls.Add(invalid_engine_install);
  }

  [Fact(Skip = "Cannot find an unreal engine install if conf file is not present or corrupt")]
  public void Load() {
    const string filePath = "~/UCT/user.conf";

    if (!File.Exists(filePath)) {
      throw new SkipException($"Cannot find file: {filePath}");
    }

    var user_conf_string = File.ReadAllText(filePath);
    var user_conf = JsonConvert.DeserializeObject<UCT.UserConf>(user_conf_string);
    if (user_conf == null) {
      throw new SkipException($"Invalid content in {filePath}, file might be corrupt.");
    }

    foreach (var engine_install in user_conf.EngineInstalls) {
      Assert.True(Directory.Exists(engine_install.RootLocation));
      Assert.True(File.Exists(Path.Combine(engine_install.RootLocation, "Engine", "Binaries", "Win64", "UE4Editor.exe"))
                  || File.Exists(Path.Combine(engine_install.RootLocation, "Engine", "Binaries", "Win64",
                      "UnrealEditor.exe")));
      // TODO: Check version
      // TODO: Check version
      // TODO: Check version
      // TODO: Check version
    }

    _test_output_helper.WriteLine($"Loaded user configuration from {filePath}");

    user_conf?.EngineInstalls.ForEach(engineInstall => {
      _test_output_helper.WriteLine($"Engine install: {engineInstall.RootLocation}");
    });
  }
}