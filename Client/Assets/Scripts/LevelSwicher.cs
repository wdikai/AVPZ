using UnityEngine;
using System.Collections;

public class LevelSwicher : MonoBehaviour {
    public void SwichLevel(int levelNumber) 
    {
        Application.LoadLevel(levelNumber);
    }
}
