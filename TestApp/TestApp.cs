using UCT;


UserConf? userConf = UserConf.Load();

if (userConf == null) {
  Console.WriteLine("Failed to load user configuration");
  return;
}

userConf.EngineInstalls.Add(
    new EngineInstall {
      RootLocation = "C:/Program Files/Epic Games/UE_4.27",
      Version      = Version.Parse("4.27.1"),
    }
);
userConf.SvnBinPath           = "C:/Program Files/TortoiseSVN/bin/svn.exe";
userConf.SvnEnginePluginRepos = "https://svn.example.com/my_project";

UserConf.Save(userConf);
