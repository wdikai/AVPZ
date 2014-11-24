using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class MainMenuGUI : MonoBehaviour {
	private StatusBar statusBar = new StatusBar();
	public Camera cam;
	public void OnGUI()
	{
		statusBar.ShowBar (cam.camera.enabled, 5034, 14, 1000);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
