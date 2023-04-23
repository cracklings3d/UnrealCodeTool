using UCT;

namespace UserConfTest;

public class UserConfTest {
  [Fact]
  public void Validation() {
    UCT.UserConf userConf = new UCT.UserConf();
    userConf.EngineInstall.Add();
  }
}
