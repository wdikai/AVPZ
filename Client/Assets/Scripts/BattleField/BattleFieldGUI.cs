using UnityEngine;
using System.Collections;
using Assets.Scripts.Field;

public class BattleFieldGUI : MonoBehaviour
{
    public GUISkin Skin;

    void Start()
    {

    }
    void OnGUI()
    {
        GUI.skin = Skin;
        if (GUI.Button(new Rect(920, 20, 200, 50), "Покинуть бой"))
        {
            //Application.LoadLevel(1);
        }

    }
}

