using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class quest : System.Object {
    public string name;
    public int ind;
    public int age;

    public int daggers;
    public int fireball;
    public int helmet;
    public int armor;
    public int sword;

    public List<craftItem> crafts = new List<craftItem>();
}

[System.Serializable]
public class craftItem : System.Object
{
    public string name;
    public int weaArm;
    public int chestType;
    public int coin;

    public int ore;
    public int wood;
    public int hide;
    public int cloth;
    public int gem;
    public int arcanum;
}
