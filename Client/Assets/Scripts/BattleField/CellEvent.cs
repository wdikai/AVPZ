using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CellEvent : MonoBehaviour
{

    public Material Mat1;
    public Material Mat2;
    public GameObject CellTexture;

    public Translator Trans;
    public Cell cell;
    public Soldiers sold;

    protected void OnMouseEnter()
    {
        CellTexture.renderer.material = Mat2;
    }
    protected void OnMouseExit()
    {
        CellTexture.renderer.material = Mat1;
    }
    protected void OnMouseUp()
    {
        if (Trans.GetCharter() != null && cell.Soldier == null && sold && sold.troopsCount[Trans.CharterIndex] > 0)
        {

                cell.SetSoldier(1, 1, Trans.GetCharter());
                Instantiate(cell.Soldier, CellTexture.transform.position, Quaternion.identity);
                sold.troopsCount[Trans.CharterIndex] = -1;
        }
    }

    public CellEvent(Cell _cell)
    {
        cell = _cell;
    }
}
