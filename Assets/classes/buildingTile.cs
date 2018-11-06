using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class buildingTile : System.Object {
    public string name;
    public int ind;
    public int cost;
    public int vp;
    public bool available = true;
    public int ownerID = 0;
    public Grid2 grid;
}

[System.Serializable]
public class Grid2 : System.Object
{
    public int x;
    public int y;
}
