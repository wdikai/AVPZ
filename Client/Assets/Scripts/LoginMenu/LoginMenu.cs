using UnityEngine;
using System.Collections;
using Connection;
using Model;
using Validators;
using Assets.Scripts;

public class LoginMenu : MonoBehaviour
{
    public GUISkin GuiSkin;
    public Font GuiFont;
    private Rect AuthorizeWindowRect = new Rect(200, 150, 800, 500);
    private Rect RegestrationWindowRect = new Rect(200, 150, 800, 500);
    private string loginStr = "";
    private string passwordStr = "";
    private string usrName = "";
    private string surname = "";
    private string login = "";
    private string pass = "";
    private string passRetry = "";
    private string nick = "";
    private int form = 0;
    private User user;
    private string erorr = "";
    void Start()
    {

    }
    void OnGUI()
    {
        GUI.skin = GuiSkin;
        GUI.skin.label.font = GuiFont;
        GUI.skin.label.alignment = TextAnchor.UpperRight;
        GUI.skin.label.fontSize = 16;

        if (form == 0)
        {
            AuthorizeWindowRect = GUI.Window(0, AuthorizeWindowRect, Authorize, "");
        }
        else if (form == 1)
        {
            RegestrationWindowRect = GUI.Window(0, RegestrationWindowRect, Regestration, "");
        }
        else if (form == 2)
        {
            RegestrationWindowRect = GUI.Window(0, RegestrationWindowRect, LoginErorr, "");
        }
        else if (form == 3)
        {
            RegestrationWindowRect = GUI.Window(0, RegestrationWindowRect, RegestrationErorr, "");
        }

    }
    void RegestrationErorr(int windowID)
    {
        GUI.BeginGroup(RegestrationWindowRect);
        GUI.Label(new Rect(50, 30, 400, 150), erorr, GUIStyle.none);

        if (GUI.Button(new Rect(160, 250, 150, 50), "Назад"))
        {
            erorr = "";
            form = 1;
        }
        GUI.EndGroup();
    }
    void LoginErorr(int windowID)
    {
        GUI.BeginGroup(RegestrationWindowRect);

        GUI.Label(new Rect(70, 30, 400, 150), erorr, GUIStyle.none);

        if (GUI.Button(new Rect(130, 210, 150, 50), "Назад"))
        {
            erorr = "";
            form = 0;
        }
        GUI.EndGroup();
    }
    void Regestration(int windowID)
    {
        GUI.BeginGroup(RegestrationWindowRect);

        usrName = LableAndText(0, 0, 195, 60, "Имя:", usrName);
        surname = LableAndText(0, 40, 195, 60, "Фамилия:", surname);
        nick = LableAndText(0, 80, 195, 60, "Никнейм:", nick);
        login = LableAndText(0, 120, 195, 60, "Логин:", login);
        pass = LableAndText(0, 160, 195, 60, "Пароль:", pass);
        passRetry = LableAndText(0, 200, 195, 60, "Пдтверждение:", passRetry);

        if (GUI.Button(new Rect(220, 250, 150, 50), new GUIContent("Назад", "Нажав на эту кнопку вы вернетесь в меню авторизации")))
        {
            form = 0;
        }

        if (GUI.Button(new Rect(0, 250, 200, 50), new GUIContent("Зарегестрироваться", "Нажав эту кнопку вы отправите запрос на регистрацию")))
        {
            if (new SignUpValidator(usrName, surname, login, pass, passRetry, nick).Check())
            {
                SignUpCommand signUp = new SignUpCommand(usrName, surname, login, pass, nick);
                if (signUp.Error == "")
                {
                    form = 0;
                }
                else
                {
                    erorr = signUp.Error;
                    form = 3;
                }
            }
            else
            {

            }
        }


        GUI.EndGroup();
        GUI.Label(new Rect(230, 100, 270, 80), GUI.tooltip, GUIStyle.none);
    }
    void Authorize(int windowID)
    {
        GUI.BeginGroup(AuthorizeWindowRect);

        loginStr = LableAndText(10, 50, 170, 60, "Логин :", loginStr);
        passwordStr = LableAndPassText(10, 90, 170, 60, "Пароль :", passwordStr);

        if (GUI.Button(new Rect(0, 200, 180, 50), new GUIContent("Регистрация", "Нажав вы перейдете на форму регистрации")))
        {
            form = 1;
        }

        if (GUI.Button(new Rect(250, 200, 180, 50), new GUIContent("Войти", "Нажав на эту кнопку вы войдете в игру")))
        {
            if (new SignInValidator(loginStr, passwordStr).Check())
            {
                SignInCommand singIn = new SignInCommand(loginStr, passwordStr);
                singIn.Execute();
                User user = new User();
                if (singIn.Error.Equals(""))
                {
                    user = singIn.Data;
                    Singleton.CreateInstance(user);
                    Application.LoadLevel(1);
                }
                else
                {
                    erorr = singIn.Error;
                    form = 2;
                }
            }
            
        }
        GUI.EndGroup();
        GUI.Label(new Rect(270, 100, 270, 80), GUI.tooltip, GUIStyle.none);
    }
    string LableAndText(int left, int top, int width, int height, string lableText, string value)//Метод рисующий лейбел и текстовое поле
    {
        GUI.Label(new Rect(left, top, width, height), lableText);
        value = GUI.TextField(new Rect(left + 10 + width, top, width + 50, height - 10), value, 30);
        return value;
    }
    string LableAndPassText(int left, int top, int width, int height, string lableText, string value)//Метод рисующий лейбел и текстовое поле
    {
        GUI.Label(new Rect(left, top, width, height), lableText);
        value = GUI.PasswordField(new Rect(left + 10 + width, top, width + 50, height - 10), value,'*',30);
        return value;
    }
}
