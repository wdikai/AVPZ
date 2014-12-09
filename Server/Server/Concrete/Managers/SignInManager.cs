
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Concrete.Entities;
using Server.Concrete.Managers;

namespace Server
{
  public class SignInManager: IServerListener
  {
    [ServerMethod]
    public JObject SignIn(JObject signInParams)
    {
      var login = signInParams["login"].ToString();
      var password = signInParams["password"].ToString();
      JObject response = null;
      User user = null;
      if (!DatabaseManager.FindUser(login, password))
      {
        var nickname = signInParams["nickname"].ToString();
        var name = signInParams["name"].ToString();
        var secondname = signInParams["secondname"].ToString();
        user = new User
        {
          Login = login,
          Password = password,
          GameData = new UserGameData
          {
            Name = name,
            SecondName = secondname,
            NickName = nickname,
            CurrentResources = new Resources{Crystals = 1, Gold = 300},
            AllTroops = new List<Troops>()
          }
        };
        DatabaseManager.AddUser(user);
        response = new JObject(new JProperty("response", JObject.FromObject(user).ToString()));
        return response;
      }

      user = DatabaseManager.GetUser(login, password);
      if (user == null)
      {
        response = new JObject(new JProperty("response", string.Format("Invalid password for user {0}", login)));
        return response;
      }
      UserFixer.FixUser(user);
      UserCache.UpdateOrAdd(user);
      response = new JObject(new JProperty("response", JObject.FromObject(user).ToString()));
      return response;
    }
  }
}