using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Concrete.Entities;

namespace Server.Concrete.Battle
{
  public class Cell
  {
    public int X;
    public int Y;

    public CellPayload Payload;
  }

  public class CellPayload
  {
    public int OwnerId;
    public GameObject ContentGameObject;
  }

  public class BattleField
  {
    public List<Cell> Cells;
    public int? UserId; // 
    public int? LastId;
    public Tuple<int, int> Owners;
    public Dictionary<int, List<UnitUpgrade>> UpgradesByUsers;
    public Dictionary<int, List<GameObject>> KilledTroopsByUser;

    public BattleField(int sizeX,int sizeY, User attacker, User defender)
    {
      Cells = new List<Cell>();
      Owners = new Tuple<int, int>(attacker.UserId, defender.UserId);
      KilledTroopsByUser = new Dictionary<int, List<GameObject>>
      {
        {attacker.UserId, new List<GameObject>()},
        {defender.UserId, new List<GameObject>()}
      };

      UpgradesByUsers = new Dictionary<int, List<UnitUpgrade>>
      {
        {attacker.UserId, attacker.GameData.UnitUpgrades},
        {defender.UserId, defender.GameData.UnitUpgrades}
      };

      for (int i = 0; i < sizeX; i++)
      {
        for (int j = 0; j < sizeY; j++)
        {
          Cells.Add(new Cell {X = i, Y = j});
        }
      }
    }

    public bool FillCell(GameObject gameObject, int x, int y)
    {
      var cell = Cells.FirstOrDefault(a => a.X == x && a.Y == y);
      if (cell == null || cell.Payload != null)
        return false;

      cell.Payload = new CellPayload {OwnerId = UserId.Value, ContentGameObject = gameObject};
      LastId = UserId.Value;
      UserId = null;
      return true;
    }

    public bool MoveUnit(int startX, int startY, int destinationX, int destinationY, long userId)
    {
      if (userId != UserId)
        return false;

      var startCell = Cells.FirstOrDefault(a => a.X == startX && a.Y == startY);
      if (startCell == null || startCell.Payload == null)
        return false;

      var destinationCell = Cells.FirstOrDefault(a => a.X == destinationX && a.Y == destinationY);
      if (destinationCell == null)
        return false;

      if (destinationCell.Payload == null)
      {
        destinationCell.Payload = new CellPayload
        {
          OwnerId = startCell.Payload.OwnerId,
          ContentGameObject = startCell.Payload.ContentGameObject
        };

        startCell.Payload = null;
        LastId = UserId.Value;
        UserId = null;
        return true;
      }
      else
      {
        if (destinationCell.Payload.OwnerId != userId)
        {
          destinationCell.Payload = ProccessFight(startCell.Payload, destinationCell.Payload);
          LastId = UserId.Value;
          UserId = null;
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    private CellPayload ProccessFight(CellPayload attacker, CellPayload defender)
    {
      var attackerPower = attacker.ContentGameObject.SoliderData.Attack + attacker.ContentGameObject.SoliderData.Defence +
                          attacker.ContentGameObject.SoliderData.Health +
                          UpgradesByUsers[attacker.OwnerId].Where(
                            unitUpgrade => unitUpgrade.UnitId == attacker.ContentGameObject.Id)
                            .Sum(upgrade => upgrade.AttackPoints + upgrade.DefencePoints);

      var defenderPower = defender.ContentGameObject.SoliderData.Attack + defender.ContentGameObject.SoliderData.Defence +
                          defender.ContentGameObject.SoliderData.Health +
                          UpgradesByUsers[defender.OwnerId].Where(
                            unitUpgrade => unitUpgrade.UnitId == defender.ContentGameObject.Id)
                            .Sum(upgrade => upgrade.AttackPoints + upgrade.DefencePoints);
      if (attackerPower - defenderPower < 0)
      {
        KilledTroopsByUser[defender.OwnerId].Add(attacker.ContentGameObject);
        return defender;
      }
      else
      {
        KilledTroopsByUser[attacker.OwnerId].Add(defender.ContentGameObject);
        return attacker;
      }
    }

    public bool TryStartTurn(int userId)
    {
      if ((UserId == userId || UserId == null) && LastId != userId)
      {
        UserId = userId;
        return true;
      }
      return false;
    }
  }
}