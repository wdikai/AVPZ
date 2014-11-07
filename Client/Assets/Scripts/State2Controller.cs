using UnityEngine;
using System.Collections;

public class State2Controller : EventController
{
    public Texture2D VarvarTexture2D;
    public Texture2D LuchTexture2D;
    public new void OnGUI()
    {
        if (base.enabledMenu)
        {
            GUI.BeginGroup(new Rect(500, 50, 700, 620));
            GUI.Box(new Rect(0, 0, 700, 620), "");
            GUI.Label(new Rect(10, 10, 300, 22), "Трактирщик");

            GUI.Button(new Rect(10, 40, 50,60 ), LuchTexture2D);
            GUI.Button(new Rect(10, 140, 50, 60), "Головорез");
            GUI.Button(new Rect(10, 240, 50, 60),VarvarTexture2D);
            GUI.Button(new Rect(10, 340, 50, 60), "Маг");
            GUI.Button(new Rect(10, 440, 50, 60), "Берсерк");

            if (GUI.Button(new Rect(350, 0, 50,20 ), "Выйти"))
            {
                this.enabledMenu = false;
            }
            GUI.EndGroup();
        }
    }
}
