using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Concrete.Entities;

namespace Server.Concrete.Managers
{
  public class UserFixer
  {
    public static void FixUser(User user)
    {
      if (user.GameData == null)
        user.GameData = new UserGameData
        {
          AllTroops = new Dictionary<int, int>(),
          CurrentResources = new Resources(),
        };
      if(user.GameData.AllTroops==null)
        user.GameData.AllTroops = new Dictionary<int, int>();
      if(user.GameData.CurrentResources==null)
        user.GameData.CurrentResources=new Resources();

      DatabaseManager.UpdateUserGameData(user);
    }
  }
}