using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Field
{
    class Field
    {
        private List<Cell> Cells;// Ячейки поля
        public void CreateField(int x,int y,Vector2 _position)// Иницыализация поля
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Cells.Add(new Cell(new Vector2(x,y)));
                    Cells[Cells.Count-1].InstantinateCell(new Vector3(_position.x*x,1,_position.y*y));
                }
            }
        }
    }
}
