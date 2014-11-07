using UnityEngine;
using System.Collections;

public class State1Controller : EventController {

    public new void OnGUI()
    {
        if (base.enabledMenu)
        {
            GUI.BeginGroup(new Rect(700, 50, 400, 400));
            GUI.Box(new Rect(0, 0, 400, 400), "");
            GUI.Label(new Rect(10, 10, 300, 22), "Меню первого состояния");
            if (GUI.Button(new Rect(350, 0, 50, 20), "Выйти"))
            {
                this.enabledMenu = false;
            }
            GUI.EndGroup();
        }
    }
}
