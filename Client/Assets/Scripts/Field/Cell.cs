using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using UnityEngine;

namespace Assets.Scripts.Field
{
    public class Cell : MonoBehaviour
    {
        Soldier Soldier
        {
            public get;
            private set;
        }//Солдат находящийся в ячейке
        Vector2 Position
        {
            public get;
            private set;
        }//Положение ячейки в сетке поля
        public UnityEngine.GameObject cellModel;// Модель ячейки
        public Texture2D deafultCell;// Стандартная текстура ячейки
        public Texture2D selectedCell;// Текстура выбраной ячейки
        public Texture2D enteredCell;// Текстура ячейки при наведении мышью
        private Texture2D currentCellTexture;// Текущая текстура ячейки
        public AddSoldierController AddController;// Контроллер добавления солдат
        bool selected
        {
            public get;
            private set;
        }// Выбрана ли кнопка
        public void SetSoldier(Soldier _soldier)
        {
            Soldier = _soldier;
            selected = false;
            currentCellTexture = deafultCell;
        }// Метод добавления солдата в ячейку
        public Cell(Vector2 _position)
        {
            Position = _position;
        }// Конструктор
        void OnMouseEnter()
        {
            currentCellTexture = enteredCell;
        }// Событие при входе курсора в границы ячейки 
        void OnMouseExit()
        {
            currentCellTexture = deafultCell;
        }// Событие при выходе курсора за границы ячейки 
        void OnMoudeDown()// Событие при нажатии на ячейку 
        {
            currentCellTexture = selectedCell;
            selected = true;
            CheckAdd();
        }
        public void InstantinateCell(Vector3 _position)
        {
            Instantiate(cellModel, _position, Quaternion.identity);
        }// Отрисовка ячейки в игре
        void CheckAdd()// Проверка на добавление персонажа
        {
            if (AddController.IsChecked)
            {
                //Soldier = new Soldier(AddController.GetCharter(),;
            }
        }
    }
}
