using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUtility.Unity
{
    [System.Serializable]
    public class GridCell
    {
        public string value;
        public int fCost;
        public Vector2Int pos { get; private set; }
        public GridCell parent = null;

        public bool visited;
        public GridCell(Vector2Int pos)
        {
            this.pos = pos;
            visited = false;
        }
    }
}