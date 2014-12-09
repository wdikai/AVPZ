using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Concrete.Entities;

namespace Server.Concrete.Managers
{
  public class UserCache
  {
    public static List<User> Cache = new List<User>();

    public static void UpdateOrAdd(User user)
    {
      var u1 = Cache.FirstOrDefault(u => u.UserId == user.UserId);
      if (u1!=null)
      {
        Cache.Remove(u1);
      }
      Cache.Add(user);
    }

    public static void OnSignOut(long userId)
    {
      var u1 = Cache.FirstOrDefault(u => u.UserId == userId);
      if (u1 != null)
      {
        Cache.Remove(u1);
      }
    }
  }
}