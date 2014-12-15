using UnityEngine;
using System.Collections;
using Connection;
using Model;
using System.Collections.Generic;

public class State3Controller : EventController
{
    private Vector2 scrollViewVector = Vector2.zero;
    Rect ScrollWindowRect = new Rect(10, 150, 500, 550);
    public Texture2D UserImage;
    List<User> Ulist = new List<User>();
    public new void OnGUI()
    {
        if (base.enabledMenu)
        {
            GUI.skin = GuiSkin;
            GUI.skin.label.fontSize = 16;
            windowRect = GUI.Window(0, windowRect, OnWindow, "Меню 1 ");
        }
    }
    void Start()
    {
        GetUsersReadyForBattleCommand get = new GetUsersReadyForBattleCommand();
        get.Execute();
        Ulist = get.Data.response;
    }

    void OnWindow(int windowID)
    {
        GUI.BeginGroup(windowRect);
        GUI.skin.label.alignment = TextAnchor.UpperCenter;
        GUI.Label(new Rect(400, 5, 300, 50), " Генерал ");
        if (GUI.Button(new Rect(990, 50, 100, 40), new GUIContent("Выход", "Нажав на эту кнопку вы вернетесь в таверну")))
        {
            this.enabledMenu = false;
        }
        if (GUI.Button(new Rect(800, 450, 200, 60), new GUIContent("В бой","Нажав на эту кнопку вы начнете бой")))
        {
            Application.LoadLevel(2);
        }
        GUI.EndGroup();

        GUI.BeginGroup(new Rect(20, 100, 500, 5000));
        scrollViewVector = GUI.BeginScrollView(new Rect(20, 0, 420, 400), scrollViewVector, new Rect(10, 0, 300, 5000));
        int i=0;
        foreach (User u in Ulist)
        {
            ShowUsers(new Rect(0, i * 100, 0, 0), u.Login, UserImage);
            i++;
        }
            GUI.EndScrollView();
        GUI.EndGroup();
        GUI.BeginGroup(windowRect);
        GUI.Label(new Rect(470, 65, 270, 80), GUI.tooltip, GUIStyle.none);
        GUI.EndGroup();
    }

    void ShowUsers(Rect Position,string name,Texture2D Image)
    {
        GUI.Box(new Rect(Position.left+15, Position.top, 395, 100),"");
        GUI.Label(new Rect(Position.left+20, Position.top+10, 70, 70), new GUIContent(Image,"Фотграфия: "+ name),GUIStyle.none);
        GUI.Label(new Rect(Position.left + 100, Position.top + 20, 200, 50), new GUIContent(name, "Игрок: " + name));
        GUI.Button(new Rect(Position.left + 250, Position.top + 50, 150, 50), new GUIContent("Вызвать на бой","Нажав на эту кнопку в вызовите "+name+" на бой"));
    }
    
}
