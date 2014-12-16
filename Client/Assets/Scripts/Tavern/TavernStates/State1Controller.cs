using UnityEngine;
using System.Collections;
using Connection;
using Assets.Scripts;
using Model;
using System.Collections.Generic;

public class State1Controller : EventController {

    public Texture2D WariorTexture2D;
    public Texture2D TrollTexture2D;
    public Texture2D IceGolemTexture2D;
    public Texture2D Sheild;
    public Texture2D Cristalys;
    public List<Model.GameObject> soldierModel = new List<Model.GameObject>();
    public int a=0;
    public int b=0;

    void Start()
    {
        GetStaticDataCommand get = new GetStaticDataCommand();
        get.Execute();
        soldierModel = get.Data.response;
    }

    public new void OnGUI()
    {
        if (base.enabledMenu)
        {
            GUI.skin = GuiSkin;
            GUI.skin.label.fontSize = 16;
            windowRect = GUI.Window(0, windowRect, OnWindow, "Меню 1 ");
        }
    }

    void Window()
    {
    
    }

    void OnWindow(int windowID)
    {
        GUI.BeginGroup(windowRect);
        GUI.skin.label.alignment = TextAnchor.UpperCenter;
        GUI.Label(new Rect(400, 5, 300, 50), " Бармен ");
        if (GUI.Button(new Rect(990, 50, 100, 40), new GUIContent("Выход", "Нажав на эту кнопку вы вернетесь в таверну")))
        {
            this.enabledMenu = false;
        }
        //GUI.Label(new Rect(470, 65, 270, 80), GUI.tooltip, GUIStyle.none);


        GUI.EndGroup();

        GUI.BeginGroup(windowRect);
        GUI.Label(new Rect(20, 80, 300, 50), "Общий бонус к Защите войск: "+a, GUIStyle.none);
        GUI.DrawTexture(new Rect(50, 110, 140, 170), Sheild, ScaleMode.ScaleToFit, true, 1.0f);
        if (GUI.Button(new Rect(160,250, 50, 40), new GUIContent("+1", "Увеличить количество брони")))
        {
            BuyTalantCommand bt = new BuyTalantCommand(Singleton.GetInstance().UserData.UserId, -1, UpgradeType.Attack);

            bt.Execute();
            a++;
            ///Покупка общего урона
        }
        DrawDefence(0, 270, 120, 110, 110, WariorTexture2D, "Warior", 0);
        DrawDefence(0, 560, 120, 110, 110, TrollTexture2D, "Troll", 1);
        DrawDefence(0, 835, 120, 110, 110, IceGolemTexture2D, "Ice Golem", 2);

        GUI.Label(new Rect(20, 310, 300, 0), "Общий бонус к Атаке войск: "+b, GUIStyle.none);
        GUI.DrawTexture(new Rect(50, 340, 140, 170), Cristalys, ScaleMode.ScaleToFit, true, 1.0f);
        if (GUI.Button(new Rect(160, 450, 50, 40), new GUIContent("+1", "Увеличить количество брони")))
        {
            BuyTalantCommand bt = new BuyTalantCommand(Singleton.GetInstance().UserData.UserId, -2, UpgradeType.Defence);

            bt.Execute();
            b++;
            ///Покупка общей брони
        }
        DrawDemage(0, 270, 340, 110, 110, WariorTexture2D, "Warior", 0);
        DrawDemage(0, 560, 340, 110, 110, TrollTexture2D, "Troll", 1);
        DrawDemage(0, 835, 340, 110, 110, IceGolemTexture2D, "Ice Golem", 2);


        GUI.Label(new Rect(520, 70, 270, 80), GUI.tooltip, GUIStyle.none);
    }

    void DrawDefence(int count, int left, int top, int width, int height, Texture2D Image, string CharterName, int index)
    {
        if (GUI.Button(new Rect(left + 127, top + 5, 50, 50), new GUIContent("+1", "Увеличить количество брони")))
        {
            BuyTalantCommand bt = new BuyTalantCommand(Singleton.GetInstance().UserData.UserId,index,UpgradeType.Defence);
            bt.Execute();
            if (bt.Error.Equals(""))
            {
                Singleton.CreateInstance(bt.UData);
            }

            ///Покупка брони
        }
        GUI.skin.label.fontSize = 16;
        GUI.DrawTexture(new Rect(left, top, width, height), Image, ScaleMode.ScaleToFit, true, 1.0f);
        GUI.Box(new Rect(left - 20, top - 25, width + 50, height + 70), CharterName);
        int Bonus = 0;
        foreach (UnitUpgrade u in Singleton.GetInstance().UserData.GameData.UnitUpgrades)
        {
            if (u.UnitId == index)
            {
                Bonus = u.DefencePoints;
            }
        }
        GUI.Label(new Rect(left - 20, top + 120, 165, 40), new GUIContent("Защита " + soldierModel[index].SoliderData.Defence+Bonus, "Количество здоровья "));

    }

    void DrawDemage(int count, int left, int top, int width, int height, Texture2D Image, string CharterName, int index)
    {
        if (GUI.Button(new Rect(left + 127, top + 5, 50, 50), new GUIContent("+1", "Увеличить количество урона")))
        {
            BuyTalantCommand bt = new BuyTalantCommand(Singleton.GetInstance().UserData.UserId, index, UpgradeType.Attack);
            
            bt.Execute();
            if (bt.Error.Equals(""))
            {
                Singleton.CreateInstance(bt.UData);
            }
            ///Покупка урона
        }
        GUI.skin.label.fontSize = 16;
        GUI.DrawTexture(new Rect(left, top, width, height), Image, ScaleMode.ScaleToFit, true, 1.0f);
        GUI.Box(new Rect(left - 20, top - 25, width + 50, height + 70), CharterName);
        int Bonus = 0;
        foreach (UnitUpgrade u in Singleton.GetInstance().UserData.GameData.UnitUpgrades)
        {
            if (u.UnitId == index)
            {
                Bonus = u.AttackPoints;
            }
        }
        GUI.Label(new Rect(left - 15, top + 120, 150, 40), new GUIContent("Атака " + soldierModel[index].SoliderData.Attack + Bonus, "Урон наносимый персонажем "));

    }
}
