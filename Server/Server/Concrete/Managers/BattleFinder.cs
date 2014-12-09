using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Concrete.Entities;

namespace Server.Concrete.Managers
{
  public class BattleFinder: IServerListener
  {
    [ServerMethod]
    public JObject GetUsersReadyForBattle()
    {
      return new JObject(new JProperty("response", JsonConvert.SerializeObject(UserCache.Cache)));
    }

    [ServerMethod]
    public JObject SendBattleCall(JObject parameters)
    {
      var targetUid = Int64.Parse(parameters["target"].ToString());
      var userId = Int64.Parse(parameters["uid"].ToString());
      var typeId = (UserMessageId)Int32.Parse(parameters["typeId"].ToString());

      DatabaseManager.AddUserMessage(new UserMessage
      {
        MessageTypeId = typeId,
        TargetUserId = targetUid,
        UserId = userId
      });

      return new JObject(new JProperty("response", "message added"));
    }

    [ServerMethod]
    public JObject AcceptBattleCall(JObject parameters)
    {
      var targetUid = Int64.Parse(parameters["target"].ToString());
      var userId = Int64.Parse(parameters["uid"].ToString());
      var typeId = (UserMessageId)Int32.Parse(parameters["typeId"].ToString());

      DatabaseManager.AddUserMessage(new UserMessage
      {
        MessageTypeId = typeId,
        TargetUserId = targetUid,
        UserId = userId
      });

      return new JObject(new JProperty("response", "message added"));
    }
  }
}