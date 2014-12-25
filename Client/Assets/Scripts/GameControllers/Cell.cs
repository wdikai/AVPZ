using UnityEngine;
using System.Collections;
using Assets.Scripts.Field;
using System;

public class Cell : MonoBehaviour
{
    public Soldier Sold;
    public GameObject CellModel;
    public Material DeafultCell;
    public Material SelectedCell;
    public Material EnteredCell;
    public EventsController EventControll;
    public int Index;
    bool selected;

    void Start()
    { 

    }
    public void SetSoldier(Soldier _soldier)// Метод добавления солдата в ячейку
    {
        Sold = _soldier;
        selected = false;
    }
    void OnMouseEnter()
    {
        CellModel.renderer.material = EnteredCell;
    }// Событие при входе курсора в границы ячейки 
    void OnMouseExit()
    {
        CellModel.renderer.material = DeafultCell;
    }// Событие при выходе курсора за границы ячейки 
    void OnMouseUp()// Событие при нажатии на ячейку 
    {
            CellModel.renderer.material = SelectedCell;
            EventControll.CellClick(this);
    }
}
