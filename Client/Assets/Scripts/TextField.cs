using UnityEngine;
using System.Collections;

public class TextField : MonoBehaviour {

	private string loginStr="";
	private string passwordStr="";
	public GameObject[]cams;


	void OnGUI()
	{
				if (cams [0].camera.enabled) {
						cams [1].camera.enabled = false;
						GUI.backgroundColor = Color.gray;
						GUI.Label (new Rect (390, 200, 250, 22), "Логин");
						loginStr = GUI.TextField (new Rect (430, 200, 200, 22), loginStr, 30);

						GUI.Label (new Rect (380, 230, 250, 22), "Пароль");
						passwordStr = GUI.TextField (new Rect (430, 230, 200, 22), passwordStr, 30);


						if (GUI.Button (new Rect (560, 265, 70, 22), "Войти")) {
								cams [0].camera.enabled = false;
								cams [1].camera.enabled = true;
						}
				}

		}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
