using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Connection;
using Model;
using Assets.Scripts;

public class State2Controller : EventController
{
    public Texture2D WariorTexture2D;
    public Texture2D TrollTexture2D;
    public Texture2D IceGolemTexture2D;
    public List<Model.GameObject> soldierModel = new List<Model.GameObject>();
    User user;
    private int[] troopsCount = new int[3];
    void CountTroops()
    { 

    foreach(Troop t in user.GameData.AllTroops)
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
    void Start()
    {
        GetStaticDataCommand get = new GetStaticDataCommand();
        get.Execute();
        soldierModel = get.Data.response;
        user = Singleton.GetInstance().UserData;
        CountTroops();
        
    }
    public new void OnGUI()
    {

        if (base.enabledMenu)
        {
            GUI.skin = GuiSkin;
            GUI.skin.label.fontSize = 16;
            windowRect = GUI.Window(0, windowRect, OnWindow, "Меню 1 ");
        }
        else 
        {
        
        }
    }

    void OnWindow(int windowID)
    {
        GUI.BeginGroup(windowRect);
        GUI.skin.label.alignment = TextAnchor.UpperCenter;
        GUI.Label(new Rect(400, 5, 300, 50), " Трактирщик ");
        if (GUI.Button(new Rect(990, 50, 100, 40), new GUIContent("Выход", "Нажав на эту кнопку вы вернетесь в таверну")))
        {
            this.enabledMenu = false;
            
            
        }
        GUI.BeginGroup(windowRect);
        DrawCharter(0, 115, 100, 110, 110, WariorTexture2D, "Warior",0);
        DrawCharter(0, 455, 100, 110, 110, TrollTexture2D, "Troll",1);
        DrawCharter(0, 780, 100, 110, 110, IceGolemTexture2D, "Ice Golem",2);

            if (GUI.Button(new Rect(360, 420, 300, 50), new GUIContent("Просмотреть наемников","Нажав, вы перейдете в меню для просмотра персонажей")))
            {
                Application.LoadLevel(3);
            }
            GUI.Label(new Rect(365, 25, 270, 80), GUI.tooltip, GUIStyle.none);
        GUI.EndGroup();
    }

    void DrawCharter(int count,int left,int top,int width, int height,Texture2D Image,string CharterName, int index)
    {
        if (GUI.Button(new Rect(left + 127, top + 5, 50, 50), new GUIContent("+", "Нажмите, чтобы купить персонажа")))
        {
            BuyCommand bc = new BuyCommand(user.UserId,index);
            bc.Execute();
            var singIn = new SignInCommand(user.Login, user.Password);
            singIn.Execute();
            user = singIn.Data;
            Singleton.CreateInstance(user);
            CountTroops();

        }
        GUI.skin.label.fontSize = 16;
        GUI.DrawTexture(new Rect(left, top, width, height), Image, ScaleMode.ScaleToFit, true, 1.0f);
        GUI.Box(new Rect(left-20, top-25, width+50, height+70), CharterName);
        GUI.Label(new Rect(left-15,top+120,150,40),new GUIContent("Нанято: "+troopsCount[index],"Количество нанятых персонажей "));

    }
    
}
