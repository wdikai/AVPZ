using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server.Concrete.Managers
{
  public class PollingManager:IServerListener
  {
    [ServerMethod]
    public JObject Refresh(JObject parameters)
    {
      JObject response = null;
      var login = parameters["login"].ToString();
      var password = parameters["password"].ToString();
      var ids = JsonConvert.DeserializeObject<List<int>>(parameters["messages"].ToString());
      var user = DatabaseManager.GetUser(login, password);
      if (user == null)
      {
        response = new JObject(new JProperty("response", string.Format("Invalid password for user {0}", login)));
        return response;
      }
      UserFixer.FixUser(user);
      MessageManager.RemoveReadMessages(ids);
      var messages = MessageManager.GetUserMessages(user.UserId);
      if (messages == null || messages.Count == 0)
      {
        response = new JObject(new JProperty("response", "No changes"));
        return response;
      }

      user.GameData.PendingMessages.AddRange(messages);
      response = new JObject(new JProperty("response", JObject.FromObject(user).ToString()));
      return response;
    }
  }
}