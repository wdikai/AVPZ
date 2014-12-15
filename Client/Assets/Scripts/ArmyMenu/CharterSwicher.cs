using UnityEngine;
using System.Collections;
using System.Collections.ObjectModel;

public class CharterSwicher : MonoBehaviour {

    public GameObject[] Charters;
    public Camera Cam;
    GameObject currentCharter;
    public GUISkin Skin;
    public int i=0;

    void Start()
    {
        currentCharter = Charters[0];
    }
    void swichRight()
    {
        if (i < Charters.Length-1)
        {
            currentCharter = Charters[++i];
            Cam.transform.Rotate(Vector3.up, 60.0f);
        }
        else
        {
            i = 0;
            currentCharter = Charters[0];
            Cam.transform.Rotate(Vector3.up, -120.0f);
        }
    }

    void swichLeft()
    {
        if (i > 0)
        {
            currentCharter = Charters[--i];
            Cam.transform.Rotate(Vector3.up, -60.0f);
        }
        else
        {
            i = Charters.Length - 1;
            currentCharter = Charters[i];
            Cam.transform.Rotate(Vector3.up, 120.0f);
        }
    }

    void OnGUI()
    {
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

}
