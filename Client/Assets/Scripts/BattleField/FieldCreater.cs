using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class FieldCreater : MonoBehaviour {
	
	public GameObject Cell;
	
	public void CreateField () 
	{
		for (int y = 0; y < 5; y++) 
		{
			for (int x = 0; x < 5; x++) 
			{
				Instantiate(Cell, new Vector3(x*20, y*20, 0), Quaternion.identity);
			}
}
		}
	}

