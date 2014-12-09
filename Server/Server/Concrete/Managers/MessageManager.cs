using System.Collections.Generic;
using Server.Concrete.Entities;

namespace Server.Concrete.Managers
{
  public static class MessageManager
  {
    public static List<UserMessage> GetUserMessages(long userId)
    {
      return DatabaseManager.GetUserMessages(userId);
    }

    public static void RemoveReadMessages(List<int> messages)
    {
      DatabaseManager.RemoveUserMessages(messages);
    }
  }
}