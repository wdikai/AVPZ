using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using Server.Concrete.Entities;

namespace Server.Concrete.Managers
{
  public class ResourcesManager
  {
    public static bool IsEnough(User user, Resources price)
    {
      return Resources.IsPositive(user.GameData.CurrentResources - price);
    }

    [ServerMethod]
    public JObject AddResources(JObject parameters)
    {
      var userid = Int32.Parse(parameters["userid"].ToString());
      var resourcesCount = Int32.Parse(parameters["count"].ToString());
      var user = DatabaseManager.GetUser(userid);
      if(!DatabaseManager.FindUser(userid))
        return new JObject(new JProperty("response", "Invalid unit id"));

      user.GameData.CurrentResources.Crystals += resourcesCount;
      user.GameData.CurrentResources.Gold += resourcesCount;
      DatabaseManager.UpdateUserGameData(user);
      return new JObject(new JProperty("response", JObject.FromObject(user.GameData).ToString()));
    }
  }
}