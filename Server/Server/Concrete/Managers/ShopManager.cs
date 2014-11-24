using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        return new JObject(new JProperty("response", "Invalid unit id"));

      if (!ResourcesManager.IsEnough(user, StaticDataManager.GetGameObjectbyId(unitid).SoliderData.Price))
        return new JObject(new JProperty("response", "Not enough resources"));

      if (user.GameData.AllTroops.ContainsKey(unitid))
        user.GameData.AllTroops[unitid]++;
      else
      {
        user.GameData.AllTroops[unitid] = 1;
      }
      DatabaseManager.UpdateUserGameData(user);
      return new JObject(new JProperty("response", JObject.FromObject(user.GameData).ToString()));
    }
  }
}