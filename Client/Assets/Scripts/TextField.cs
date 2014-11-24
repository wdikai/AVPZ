using UnityEngine;
using System.Collections;

public class TextField : MonoBehaviour
{

    private string loginStr = "";
    private string passwordStr = "";
    public GameObject[] cams;
    private int form = 0;
    string name = "";
    string  sourname ="";
    string  age ="";
    string  login ="";
    string  password ="";
    string passwordRetry = "";
    string nick = "";
    


    void OnGUI()
    {
        if (cams[0].camera.enabled && form ==0)
        {
            cams[1].camera.enabled = false;
            GUI.backgroundColor = Color.gray;
            GUI.Label(new Rect(390, 200, 250, 22), "Логин");
            loginStr = GUI.TextField(new Rect(430, 200, 200, 22), loginStr, 30);

            GUI.Label(new Rect(380, 230, 250, 22), "Пароль");
            passwordStr = GUI.TextField(new Rect(430, 230,200, 22), passwordStr, 30);
            if (GUI.Button(new Rect(440, 245, 80, 22), "Регистрация")) 
            {
                form = 1;
               
            }


            if (GUI.Button(new Rect(560, 265, 70, 22), "Войти"))
            {
                cams[0].camera.enabled = false;
                cams[1].camera.enabled = true;
            }

        }
        else if (cams[0].camera.enabled && form == 1)
        {
            GUI.BeginGroup(new Rect(200, 50, 400, 400));
            GUI.Label(new Rect(300, 180, 200, 50), "Регистрация");
            GUI.Label(new Rect(205, 205, 50, 22), "Имя:");
            GUI.Label(new Rect(205, 232, 50, 22), "Фамилия:");
            GUI.Label(new Rect(205, 259, 50, 22), "Возраст:");
            GUI.Label(new Rect(205, 288, 50, 22), "Логин:");
            GUI.Label(new Rect(205, 315, 50, 22), "Пароль:");
            GUI.Label(new Rect(205, 343, 50, 22), "Подтверждение пароля:");
            GUI.Label(new Rect(205, 371, 50, 22), "Никнейм:");


            name = GUI.TextField(new Rect(260, 205, 100, 22), name, 30);
            sourname = GUI.TextField(new Rect(260, 232, 100, 22), sourname, 30);
            age = GUI.TextField(new Rect(260, 259, 100, 22), age, 30);
            login = GUI.TextField(new Rect(260, 288, 100, 22), login, 30);
            password = GUI.TextField(new Rect(260, 315, 100, 22), password, 30);
            passwordRetry = GUI.TextField(new Rect(260, 343, 100, 22), passwordRetry, 30);
            nick = GUI.TextField(new Rect(260, 371, 100, 22), nick, 30);

            if (GUI.Button(new Rect(100, 370, 70, 22), "назад"))
            {
                form = 0;
            }
            GUI.EndGroup();
        }

    }
}
