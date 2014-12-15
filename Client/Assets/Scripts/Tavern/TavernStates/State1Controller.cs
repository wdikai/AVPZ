using UnityEngine;
using System.Collections;

public class State1Controller : EventController {
    
    public new void OnGUI()
    {
        if (base.enabledMenu)
        {
            GUI.skin = GuiSkin;
            GUI.skin.label.fontSize = 16;
            windowRect = GUI.Window(0, windowRect, OnWindow, "Меню 1 ");
        }
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
        GUI.Label(new Rect(470, 65, 270, 80), GUI.tooltip, GUIStyle.none);
        GUI.EndGroup();
    }
}
