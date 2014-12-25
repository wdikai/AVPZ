using UnityEngine;
using System.Collections;
using Connection;
using Model;
using System.Collections.Generic;

public class ArmyMenuGUI : MonoBehaviour {
    public GUISkin Skin;
    private Vector2 scrollViewVector = Vector2.zero;
    Rect ScrollWindowRect = new Rect(10, 150, 500, 550);
    public UnityEngine.GameObject Warior;
    List<Model.GameObject> soldiersModel = new List<Model.GameObject>();
    public UnityEngine.GameObject[] Charters;
    public Camera Cam;
    UnityEngine.GameObject currentCharter;
    public int index = 0;

    // Use this for initialization
    void Start()
    {
        GetStaticDataCommand Data = new GetStaticDataCommand();
        Data.Execute();
        soldiersModel = Data.Data.response;
        currentCharter = Charters[0];
    }
    void OnGUI()
    {
        GUI.skin = Skin;
        if (GUI.Button(new Rect(1080, 20, 130, 50), "Назад"))
        {
            Application.LoadLevel(1);
        }

        GUI.Window(0, ScrollWindowRect, ScrollArmy, "");
        GUI.skin = Skin;
        if (GUI.Button(new Rect(550, 600, 70, 50), " <<- "))
        {
            swichLeft();
        }

        if (GUI.Button(new Rect(1050, 600, 70, 50), " ->> "))
        {
            swichRight();
        }
        if (GUI.RepeatButton(new Rect(700, 600, 70, 50), " <- "))
        {

            currentCharter.transform.Rotate(Vector3.up, 5.1f);
        }

        if (GUI.RepeatButton(new Rect(900, 600, 70, 50), " -> "))
        {
            currentCharter.transform.Rotate(Vector3.up, -5.1f);
        }

    }

    void swichRight()
    {
        if (index < Charters.Length - 1)
        {
            currentCharter = Charters[++index];
            Cam.transform.Rotate(Vector3.up, 60.0f);
        }
        else
        {
            index = 0;
            currentCharter = Charters[0];
            Cam.transform.Rotate(Vector3.up, -120.0f);
        }
    }

    void swichLeft()
    {
        if (index > 0)
        {
            currentCharter = Charters[--index];
            Cam.transform.Rotate(Vector3.up, -60.0f);
        }
        else
        {
            index = Charters.Length - 1;
            currentCharter = Charters[index];
            Cam.transform.Rotate(Vector3.up, 120.0f);
        }
    }

    void ScrollArmy(int Id)
    {
        GUI.BeginGroup(new Rect(20, 100, 500, 700));
        GUI.Label(new Rect(120,0, 200, 50), new GUIContent(soldiersModel[index].SoliderData.Name));
        GUI.skin.label.fontSize = 16;
        GUI.Label(new Rect(50, 30, 200, 50), new GUIContent("Цена: "), GUIStyle.none);
        GUI.Label(new Rect(320, 30, 100, 50), new GUIContent(soldiersModel[index].SoliderData.Price.Gold.ToString()));
        GUI.Label(new Rect(50, 60, 200, 50), new GUIContent("Скорость перемещения: "), GUIStyle.none);
        GUI.Label(new Rect(320, 60, 100, 50), new GUIContent(soldiersModel[index].SoliderData.MovementSpeed.ToString()));
        GUI.Label(new Rect(50, 90, 200, 50), new GUIContent("Защита:   "), GUIStyle.none);
        GUI.Label(new Rect(320, 90, 100, 50), new GUIContent(soldiersModel[index].SoliderData.Defence.ToString()));
        GUI.EndGroup();
    
    }
}
