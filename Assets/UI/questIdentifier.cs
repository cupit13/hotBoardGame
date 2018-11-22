using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questIdentifier : MonoBehaviour {

    public Text title;
    public List<Sprite> symbolsPNG = new List<Sprite>();
    public List<Sprite> treasuresPNG = new List<Sprite>();

    public List<Image> symbols = new List<Image>();
    public List<GameObject> craftHolders;
    public Text craftTitle1;
    public Text craftTitle2;
    public List<Text> rescs1 = new List<Text>();
    public List<Text> rescs2 = new List<Text>();
    public List<Text> coins = new List<Text>();
    public List<Image> treasures = new List<Image>();
}
