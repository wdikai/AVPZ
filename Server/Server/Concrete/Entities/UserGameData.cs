
using System.Collections.Generic;

namespace Server.Concrete.Entities
{
  public class UserGameData
  {
    public string Name;
    public string SecondName;
    public string NickName;

    public Resources CurrentResources;
    public Dictionary<int, int> AllTroops;
  }
}