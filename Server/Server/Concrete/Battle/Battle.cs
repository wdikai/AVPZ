using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Concrete.Entities;

namespace Server.Concrete.Battle
{
  public class BattleField
  {
    public List<Sell> Sells;
  }

  public class Sell
  {
    public int X;
    public int Y;
    public SellObject ContentObject;
  }

  public class SellObject
  {
    public SellObjectTypeId TypeId;
    public GameObject InnerObject;
  }

  public enum SellObjectTypeId
  {
    Solider
  }
}