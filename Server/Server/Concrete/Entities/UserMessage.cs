using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Concrete.Entities
{
  public enum UserMessageId
  {
    BattleCall = 0,
    BattleAccept = 1,
    StartBattle = 2
  }

  public class UserMessage
  {
    public long MessageId;
    public long UserId;
    public long TargetUserId;
    public UserMessageId MessageTypeId;
    public bool IsRead;
  }
}