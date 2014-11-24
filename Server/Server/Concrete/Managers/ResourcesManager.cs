using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Concrete.Entities;

namespace Server.Concrete.Managers
{
  public class ResourcesManager
  {
    public static bool IsEnough(User user, Resources price)
    {
      return Resources.IsPositive(user.GameData.CurrentResources - price);
    }
  }
}