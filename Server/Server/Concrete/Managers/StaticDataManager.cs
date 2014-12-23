using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Abstract;
using Server.Concrete.Entities;

namespace Server.Concrete.Managers
{
  public class StaticDataManager: IServerListener
  {
    private static readonly List<GameObject> _gameObjects; 

    static StaticDataManager()
    {
      _gameObjects = new List<GameObject>
      {
        new GameObject
        {
          Id = 0,
          SoliderData = new SoliderData
          {
            Name = "Swordsman",
            Attack = 10,
            Defence = 10,
            MovementSpeed = 3,
            Price = new Resources {Gold = 10, Crystals = 0},
            Health = 10,
            Priority = 4
          }
        },
        new GameObject
        {
          Id = 1,
          SoliderData = new SoliderData
          {
            Name = "Urka",
            Attack = 20,
            Defence = 5,
            MovementSpeed = 5,
            Price = new Resources {Gold = 20, Crystals = 1},
            Health = 20,
            Priority = 2
          }
        },
        new GameObject
        {
          Id = 2,
          SoliderData = new SoliderData
          {
            Name = "Frost golem",
            Attack = 40,
            Defence = 30,
            MovementSpeed = 1,
            Price = new Resources {Gold = 30, Crystals = 5},
            Health = 50,
            Priority = 3
          }
        }
      };
    }

    [ServerMethod]
    public JObject GetStaticData()
    {
      return new JObject(new JProperty("response", JsonConvert.SerializeObject(_gameObjects)));
    }

    public static bool IsValidUnit(int id)
    {
      return _gameObjects.Any(go => go.Id == id);
    }

    public static GameObject GetGameObjectbyId(int id)
    {
      return _gameObjects.FirstOrDefault(go => go.Id == id);
    }
  }
}