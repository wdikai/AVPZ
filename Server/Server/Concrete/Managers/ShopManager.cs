using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Concrete.Entities;

namespace Server.Concrete.Managers
{
  public class ShopManager:IServerListener
  {
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
  }
}