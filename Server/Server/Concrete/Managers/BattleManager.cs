using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Server.Abstract;
using Server.Concrete.Battle;
using Server.Concrete.Entities;

namespace Server.Concrete.Managers
{
  public class BattleManager: IServerListener
  {
    public static readonly List<BattleField> BattleFields = new List<BattleField>(); 

    public static BattleField StartBattle(User attacker, User defender)
    {
      var battleField = new BattleField(8,6, attacker, defender);
      battleField.TryStartTurn(attacker.UserId);
      return battleField;
    }

    [ServerMethod]
    public JObject TryStartTurn(JObject parameters)
    {
      var userId = Int32.Parse(parameters["userid"].ToString());
      var battleField = BattleFields.FirstOrDefault(bf => bf.Owners.Item1 == userId || bf.Owners.Item2 == userId);
      if (battleField.TryStartTurn(userId))
        return new JObject(new JProperty("response", JObject.FromObject(battleField)));
      else
      {
        return new JObject(new JProperty("response", "nihuya"));
      }
    }

    [ServerMethod]
    public JObject FillCell(JObject parameters)
    {
      var userId = Int32.Parse(parameters["userid"].ToString());
      var unitId = Int32.Parse(parameters["unitId"].ToString());
      var posX = Int32.Parse(parameters["x"].ToString());
      var posY = Int32.Parse(parameters["y"].ToString());

      var user = DatabaseManager.GetUser(userId);
      var unit = StaticDataManager.GetGameObjectbyId(user.GameData.AllTroops.First(u => u.Id == unitId).Id);
      var battleField = BattleFields.FirstOrDefault(bf => bf.Owners.Item1 == userId || bf.Owners.Item2 == userId);
      if (battleField.TryStartTurn(userId))
      {
        if(battleField.FillCell(unit, posX, posY))
          return new JObject(new JProperty("response", JObject.FromObject(battleField)));
        else
        {
          return new JObject(new JProperty("response", "cant fill this sell"));
        }
      }
      else
      {
        return new JObject(new JProperty("response", "not your turn"));
      }
    }

    [ServerMethod]
    public JObject MoveUnit(JObject parameters)
    {
      var userId = Int32.Parse(parameters["userid"].ToString());
      var unitId = Int32.Parse(parameters["unitId"].ToString());
      var sposX = Int32.Parse(parameters["sx"].ToString());
      var sposY = Int32.Parse(parameters["sy"].ToString());
      var posX = Int32.Parse(parameters["x"].ToString());
      var posY = Int32.Parse(parameters["y"].ToString());

      var user = DatabaseManager.GetUser(userId);
      var unit = StaticDataManager.GetGameObjectbyId(user.GameData.AllTroops.First(u => u.Id == unitId).Id);
      var battleField = BattleFields.FirstOrDefault(bf => bf.Owners.Item1 == userId || bf.Owners.Item2 == userId);
      if (battleField.TryStartTurn(userId))
      {
        if (battleField.MoveUnit(sposX, sposY, posX, posY, userId))
          return new JObject(new JProperty("response", JObject.FromObject(battleField)));
        else
        {
          return new JObject(new JProperty("response", "cant fill this sell"));
        }
      }
      else
      {
        return new JObject(new JProperty("response", "not your turn"));
      }
    }

    [ServerMethod]
    public JObject FinishBattle(JObject parameters)
    {
      var userId = Int32.Parse(parameters["userid"].ToString());
      var battleField = BattleFields.FirstOrDefault(bf => bf.Owners.Item1 == userId || bf.Owners.Item2 == userId);

      var attacker = DatabaseManager.GetUser(battleField.Owners.Item1);
      var defender = DatabaseManager.GetUser(battleField.Owners.Item2);

      var attackerLooses = new Dictionary<long, int>();
      foreach (var go in battleField.KilledTroopsByUser[defender.UserId])
      {
        if (attackerLooses.ContainsKey(go.Id))
          attackerLooses[go.Id]++;
        else
        {
          attackerLooses[go.Id] = 1;
        }
      }
      var defenderLosses = new Dictionary<long, int>();
      foreach (var go in battleField.KilledTroopsByUser[attacker.UserId])
      {
        if (defenderLosses.ContainsKey(go.Id))
          defenderLosses[go.Id]++;
        else
        {
          defenderLosses[go.Id] = 1;
        }
      }

      foreach (var attackerLoose in attackerLooses)
      {
        attacker.GameData.AllTroops.First(t => t.Id == attackerLoose.Key).Count -= attackerLoose.Value;
      }
      foreach (var dl in defenderLosses)
      {
        defender.GameData.AllTroops.First(t => t.Id == dl.Key).Count -= dl.Value;
      }

      DatabaseManager.UpdateUserGameData(attacker);
      DatabaseManager.UpdateUserGameData(defender);

      return new JObject(new JProperty("response","battle finished motherfuckers"));
    }
  }
}