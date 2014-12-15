using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Field;
using System;
using Assets.Scripts;
using Connection;
using TCP.P2P;

public class EventsController : MonoBehaviour
{
    public AddSoldierController AddController;
    public bool ReadyForBattle;
    public Field GameField;
    public List<Soldier> PlayerSoldiers;
    private int temp=-1;
    private Server server;
    public Client client;
    public struct Sold
    {
        public int Index;
        public int Id;
        public int UserID;
    }
    void Start()
    {
        server = new Server("",20);// ip 
    }
    public void CheckCell(Cell _cell)
    {
        bool isFull=_cell.Sold != null;

        if (isFull && temp == -1)
        {
            temp = _cell.Index;
        }
        else if(!(temp==-1||isFull) )
        {
            GameField.Cells[temp].Sold.model.transform.position = GameField.Cells[_cell.Index].CellModel.transform.position;
            GameField.Cells[_cell.Index].SetSoldier(GameField.Cells[temp].Sold);
            GameField.Cells[temp].SetSoldier(null);
            temp = -1;
        }
        else if(!(temp==-1||!isFull))
        {
            if (_cell.Sold.PlayerID != Singleton.GetInstance().UserData.UserId)
            {
                if (CheckEnemy(temp, _cell.Index))
                {
                    _cell.Sold.Data.Defence -= GameField.Cells[temp].Sold.Data.Attack;
                    if (_cell.Sold.Data.Defence < 0)
                    {
                        Destroy(_cell.Sold.model);
                        _cell.Sold = null;
                    }
                }
            }
        }
    }
    public void CellClick(Cell _cell)
    {
        if (ReadyForBattle)
        {
            CheckCell(_cell);
        }
        else if (!ReadyForBattle)
        {
            AddSoldier(_cell);
        }
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Готов к бою"))
        {
            ReadyForBattle = true;
            client = new Client("",20);// ip


            foreach(Cell c in GameField.Cells)
            {
                if (c.Sold != null)
                { 
                
                }
            }
            client.SendMessageSocket();
        }
    }
    void AddSoldier(Cell _cell)
    {
        if (AddController.IsChecked&&_cell.Sold==null)
        {
            _cell.Sold = new Soldier();
            _cell.Sold.Data = AddController.GetCharter().Data;
            _cell.Sold.model = Instantiate(AddController.GetCharter().model, _cell.CellModel.transform.position, Quaternion.identity) as GameObject;
            _cell.Sold.PlayerID = Singleton.GetInstance().UserData.UserId;

        }
    }
    bool CheckEnemy(int t, int c)
    {
        bool b=false;
        b = b || ((t - 8) == c);
        b = b || ((t - 1) == c);
        b = b || ((t + 1) == c);
        b = b || ((t + 8) == c);
        return b;
    }
    void Atack()
    { 
    
    }
}

