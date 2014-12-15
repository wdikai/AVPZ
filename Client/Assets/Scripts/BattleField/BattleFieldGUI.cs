using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class BattleFieldGUI : MonoBehaviour
{
    public GUISkin Skin;
    public Field field;
    public GameObject cellTexture;

    void Start()
    {
        field = new Field(cellTexture);
        field.CreateField(8);
    }
    void OnGUI()
    {
        GUI.skin = Skin;
        if (GUI.Button(new Rect(920, 20, 200, 50), "Покинуть бой"))
        {
            Application.LoadLevel(1);
        }

    }
}

