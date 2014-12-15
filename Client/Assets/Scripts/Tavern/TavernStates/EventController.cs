using UnityEngine;
using System.Collections;


public class EventController : MonoBehaviour {
    public Color MouseClickColor;
    public Color MouseEnterColor;
    public Light StateLight;
    protected bool enabledMenu;
    public GUISkin GuiSkin;
    protected Rect windowRect = new Rect(30,30,1140,600);

    protected void OnMouseEnter()
    {
        StateLight.enabled = true;
        StateLight.color = MouseEnterColor;
    }
    protected void OnMouseExit()
    {
        StateLight.enabled = false;
        StateLight.color = Color.white;
    }
    protected void OnMouseUp()
    {
        enabledMenu = true;
        StateLight.enabled = true;
        StateLight.color = MouseClickColor;
    }
   protected void OnGUI()
    {
        
    }

	// Use this for initialization
	protected void Start () {
        StateLight.enabled = false;
        enabledMenu = false;
	}
	
	// Update is called once per frame
	protected void Update () {
	
	}
}
