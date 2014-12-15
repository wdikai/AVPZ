using UnityEngine;
using System.Collections;

public class Translator : MonoBehaviour
{
    public GameObject Warior;
    public GameObject Troll;
    public GameObject IceGolem;
    private GameObject currentCharter;
    public int CharterIndex = 0;
    public GameObject GetCharter()
    {
        return currentCharter;
    }

    public void SetCharter(int index)
    {

        switch (index)
        {
            case 0:
                currentCharter = Warior;
                CharterIndex = 0;
                break;
            case 1:
                currentCharter = Troll;
                CharterIndex = 1;
                break;
            case 2:
                currentCharter = IceGolem;
                CharterIndex = 2;
                break;
        }

    }
}
