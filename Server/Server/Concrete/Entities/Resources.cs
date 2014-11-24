using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Concrete.Entities
{
  public class Resources
  {
    public double Gold;
    public double Crystals;

    public static Resources operator -(Resources r1, Resources r2)
    {
      return new Resources
      {
        Gold = r1.Gold - r2.Gold,
        Crystals = r1.Gold - r2.Crystals
      };
    }

    public static bool IsPositive(Resources r)
    {
      return r.Crystals > 0 && r.Gold > 0;
    }
  }
}