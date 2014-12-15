using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	public Material Mat1;
	public Material Mat2;
	public int index;
	public GameObject ButtonPlane;
    public Translator Trans;
	
	protected void OnMouseEnter()
	{
		ButtonPlane.renderer.material = Mat2;
	}
	protected void OnMouseExit()
	{
		ButtonPlane.renderer.material = Mat1;
	}
	protected void OnMouseUp()
	{
		ButtonPlane.renderer.material = Mat2;
        Trans.SetCharter(index);
	}
}
