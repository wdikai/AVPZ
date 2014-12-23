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
          AllTroops = new List<Troops>(),
          CurrentResources = new Resources(),
        };
      if (user.GameData.AllTroops == null)
        user.GameData.AllTroops = new List<Troops>();
      if (user.GameData.CurrentResources == null)
        user.GameData.CurrentResources = new Resources();
      if (user.GameData.PendingMessages == null)
        user.GameData.PendingMessages = new List<UserMessage>();
      if (user.GameData.UnitUpgrades == null)
        user.GameData.UnitUpgrades = new List<UnitUpgrade>();

      DatabaseManager.UpdateUserGameData(user);
    }
  }
}