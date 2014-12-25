using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Field;
using System;
using Assets.Scripts;
using Connection;
using TCP.P2P;
using TCP.Message;
using Assets.Scripts.ServerTools.Model;

public class EventsController : MonoBehaviour
{
    public AddSoldierController AddController;
    public bool ReadyForBattle;
    public Field GameField;
    public List<Soldier> PlayerSoldiers;
    private int temp=-1;
    private Server server;
    public Client client;
    public bool IsMyStep=true;
    public bool Win;
    void Start()
    {
        client = new Client("", 20);// ip
        server = new Server("",20,this);// ip 
    }
    public void CheckCell(Cell _cell)
    {

            bool isFull = _cell.Sold != null;

            if (isFull && temp == -1)
            {
                temp = _cell.Index;
            }
            else if (!(temp == -1 || isFull))
            {
                Move(temp, _cell.Index);
                StepMessage st = new StepMessage();
                client.SendMessageSocket(st.CreateMessage(temp, _cell.Index).ToJson());
                temp = -1;
                IsMyStep = false;
            }
            else if (!(temp == -1 || !isFull))
            {
                if (_cell.Sold.PlayerID != Singleton.GetInstance().UserData.UserId)
                {
                    if (CheckEnemy(temp, _cell.Index))
                    {
                        int demage = (int)GameField.Cells[temp].Sold.Data.Attack;
                        AttackMessage at = new AttackMessage();
                        client.SendMessageSocket(at.CreateMessage(_cell.Index, demage).ToJson());
                        Atack(_cell.Index, demage);
                        IsMyStep = false;
                    }
                }
            }
        
    }
    public void CellClick(Cell _cell)
    {
        if (ReadyForBattle)
        {
            if (IsMyStep)
            {
                CheckCell(_cell);
            }
        }
        else
        {
            AddSoldier(_cell);
        }
    }
    void OnGUI()
    {
        if (!Win)
        {
            if (GUI.Button(new Rect(10, 10, 100, 50), "Готов к бою"))
            {
                ReadyForBattle = true;


                InitMessage Init = new InitMessage();

                foreach (Cell c in GameField.Cells)
                {
                    if (c.Sold != null)
                    {
                        Init.AddSoldier(c.Sold.PlayerID, c.Index, CheckTroop(c.Sold.Data.Name));
                    }
                }

                client.SendMessageSocket(Init.CreateMessage().ToJson());
            }

            if (GUI.Button(new Rect(110, 10, 100, 50), "Сдаться"))
            {
                DefeatMessage df = new DefeatMessage();
                client.SendMessageSocket(df.CreateMessage().ToJson());
                Application.LoadLevel(1);

            }
            if (GUI.Button(new Rect(210, 10, 100, 50), "Пропустить ход"))
            {
                SkipMessage sk = new SkipMessage();
                client.SendMessageSocket(sk.CreateMessage().ToJson());
                IsMyStep = false;
            }
        }
        else
        {
            GUI.Box(new Rect(300, 100, 600, 400), "");
            GUI.Label(new Rect(400, 200, 200, 50), "Победа!");
            GUI.Button(new Rect(550, 260, 50, 50), "Ок");
            server.Close();
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
    int CheckTroop(string s)
    {
        switch (s)
        {
            case "Swordsman":
                return 0;
            case "Urka":
                return 1;
            case "Frost golem":
                return 2;
            default: return 0;
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
    public void Atack(int index,int demage)
    {
        GameField.Cells[index].Sold.Data.Defence -= demage;

        if (GameField.Cells[index].Sold.Data.Defence < 0)
        {
            Destroy(GameField.Cells[index].Sold.model);
            GameField.Cells[index].Sold = null;
            if(!IsAlive())
            {
                DefeatMessage df = new DefeatMessage();
                client.SendMessageSocket(df.CreateMessage().ToJson());
                Application.LoadLevel(1);
            }
        }
    }

    bool IsAlive()
    {
        foreach (Cell c in GameField.Cells)
        {
            if (c.Sold.PlayerID == Singleton.GetInstance().UserData.UserId)
            {
                return true;
            }

        }
        return false;
    }
    public void InitEnemy(List<Sold> troops)
    {
        GetStaticDataCommand getData = new GetStaticDataCommand();
        getData.Execute();

        foreach (Sold s in troops)
        {
            GameField.Cells[s.Index].Sold = new Soldier();
            GameField.Cells[s.Index].Sold.Data = getData.Data.response[s.Id].SoliderData;
            GameField.Cells[s.Index].Sold.model = Instantiate(AddController.GetCharter().model, GameField.Cells[s.Index].CellModel.transform.position, Quaternion.identity) as GameObject;
            GameField.Cells[s.Index].Sold.PlayerID = s.UserID;
        }
    }

    public void Move(int current,int target)
    {
        GameField.Cells[current].Sold.model.transform.position = GameField.Cells[target].CellModel.transform.position;
        GameField.Cells[target].SetSoldier(GameField.Cells[current].Sold);
        GameField.Cells[current].SetSoldier(null);
    }

}

