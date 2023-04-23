using UCT;
using UCT.Util;

UserConf? userConf = JsonHandler.LoadUserConf();

if (userConf == null) {
  Console.WriteLine("Failed to load user configuration");
  return;
}

userConf.EngineInstall.Add(
    new EngineInstall {
      RootLocation = "C:\\Program Files\\Epic Games\\UE_4.27",
      Version      = Version.Parse("4.27.1"),
    }
);
userConf.SvnPath              = "C:\\Program Files\\TortoiseSVN\\bin\\svn.exe";
userConf.SvnEnginePluginRepos = "https://svn.example.com/my_project";

JsonHandler.SaveUserConf(userConf);
