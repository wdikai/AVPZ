using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Connection;
using Model;
using Assets.Scripts;

public class Soldiers : MonoBehaviour
{
    private User user;
    public int[] troopsCount = new int[3];
    void CountTroops()
    {

        foreach (Troop t in user.GameData.AllTroops)
        {
            troopsCount = new int[3];

            if (t.Id == 0)
            {
                troopsCount[0] += 1;
            }
            else if (t.Id == 1)
            {
                troopsCount[1] += 1;
            }
            else if (t.Id == 2)
            {
                troopsCount[2] += 1;
            }

        }
    }
    // Use this for initialization
    void Start()
    {
        user=Singleton.GetInstance().UserData;
        CountTroops();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
