using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Abstract;
using Server.Concrete.Entities;

namespace Server.Concrete.Managers
{
  public class ShopManager:IServerListener
  {
    private readonly Resources _upgradePrice = new Resources{ Gold = 1 };

    public enum UpgradeType
    {
      Defence = 0,
      Attack = 1
    }

    [ServerMethod]
    public JObject BuyUnit(JObject parameters)
    {
      var userid = Int32.Parse(parameters["userid"].ToString());
      var unitid = Int32.Parse(parameters["unitid"].ToString());
      var user = DatabaseManager.GetUser(userid);
      if (!StaticDataManager.IsValidUnit(unitid))
        return new JObject(new JProperty("response","Invalid unit id"));

      var gameObject = StaticDataManager.GetGameObjectbyId(unitid);
      if (!ResourcesManager.IsEnough(user, gameObject.SoliderData.Price))
        return new JObject(new JProperty("response", "Not enough resources"));

      var troop = user.GameData.AllTroops.FirstOrDefault(t => t.Id == unitid);
      if (troop!=null)
        troop.Count++;
      else
      {
        user.GameData.AllTroops.Add(new Troops
        {
          Id = unitid,
          Count = 1
        });
      }

      user.GameData.CurrentResources -= gameObject.SoliderData.Price;
      DatabaseManager.UpdateUserGameData(user);
      return new JObject(new JProperty("response", JObject.FromObject(user.GameData).ToString()));
    }

    [ServerMethod]
    public JObject BuyTalant(JObject parameters)
    {
      var userId = Int32.Parse(parameters["userid"].ToString());
      var unitid = Int32.Parse(parameters["unitid"].ToString());
      var upgradeType = (UpgradeType) Int32.Parse(parameters["upgradetype"].ToString());

      var user = DatabaseManager.GetUser(userId);
      if (!StaticDataManager.IsValidUnit(unitid))
        return new JObject(new JProperty("response", "Invalid unit id"));

      if (!ResourcesManager.IsEnough(user, _upgradePrice))
        return new JObject(new JProperty("response", "Not enough resources"));

      var unitUpgrade = user.GameData.UnitUpgrades.FirstOrDefault(upgrade => upgrade.UnitId == unitid);

      if (unitUpgrade != null)
      {
        if (upgradeType == UpgradeType.Defence)
        {
          unitUpgrade.DefencePoints++;
        }
        else
        {
          unitUpgrade.DefencePoints++;
        }
      }
      else
      {
        if (upgradeType == UpgradeType.Defence)
        {
          user.GameData.UnitUpgrades.Add(new UnitUpgrade{DefencePoints = 1,UnitId = unitid});
        }
        else
        {
          user.GameData.UnitUpgrades.Add(new UnitUpgrade { AttackPoints = 1, UnitId = unitid });
        }
      }

      user.GameData.CurrentResources -= _upgradePrice;
      DatabaseManager.UpdateUserGameData(user);
      return new JObject(new JProperty("response", JObject.FromObject(user.GameData).ToString()));
    }
  }
}