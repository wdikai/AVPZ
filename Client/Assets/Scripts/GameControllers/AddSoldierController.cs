using UnityEngine;
using System.Collections;
using Assets.Scripts.Field;
using Assets.Scripts;
using Connection;

public class AddSoldierController : MonoBehaviour
{
    private Soldier warior;
    private Soldier troll;
    private Soldier golem;

    public GameObject Warior;
    public GameObject Troll;
    public GameObject IceGolem;

    public Transform Trasnsf;

    private int CharterIndex;
    public bool IsChecked;
    private Model.User user;

    private Soldier currentCharter;
    private int[] troopsCount;
    public Soldier GetCharter()
    {
        return currentCharter;
    }
    void Start()
    {
        
    }
    public void SetCharter(int index)
    {
        if (warior == null)
        {
            //////////////////////////////////////////////////
            SignInCommand signIn = new SignInCommand("Urka","qwerty");
            signIn.Execute();
            Singleton.CreateInstance(signIn.Data);
            /////////////////////////////////////////////////
            user = Singleton.GetInstance().UserData;
            GetStaticDataCommand getData = new GetStaticDataCommand();
            getData.Execute();
            golem = Soldier.Create(IceGolem, getData.Data.response[0].SoliderData, user.UserId);
            warior = Soldier.Create(Warior, getData.Data.response[1].SoliderData, user.UserId);
            troll = Soldier.Create(Troll, getData.Data.response[2].SoliderData, user.UserId);
            Warior = null;
            Troll = null;
            IceGolem = null;
        }

        switch (index)
        {
            case 0:
                if (troopsCount[0] > 0)
                {
                    currentCharter = warior;
                    CharterIndex = 0;
                    troopsCount[0]--;
                }
                break;

            case 1:
                if (troopsCount[1] > 0)
                {
                    currentCharter = troll;
                    CharterIndex = 1;
                    troopsCount[1]--;
                }
                break;
            case 2:
                if (troopsCount[2] > 0)
                {
                    currentCharter = golem;
                    CharterIndex = 2;
                    troopsCount[2]--;
                }
                break;
        }

        IsChecked = true;
    }
    private void CountTroops()
    {
        troopsCount = new int[3];
        troopsCount[0] = 3;
        troopsCount[1] = 3;
        troopsCount[2] = 3;

    }
    private void RefreshSoldiers()
    {
        user.GameData.AllTroops[0].Count = troopsCount[0];
        user.GameData.AllTroops[1].Count = troopsCount[1];
        user.GameData.AllTroops[2].Count = troopsCount[2];
    }
    public AddSoldierController()
    {

        CountTroops();

    }
}
