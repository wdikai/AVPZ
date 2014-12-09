
using System.Collections.Generic;

namespace Server.Concrete.Entities
{
  public class UserGameData
  {
    public string Name;
    public string SecondName;
    public string NickName;

    public Resources CurrentResources;
    public List<Troops> AllTroops;

    public List<UserMessage> PendingMessages;
  }

  public class Troops
  {
    public int Id;
    public int Count;
  }
}