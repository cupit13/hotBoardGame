using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class firstCard : MonoBehaviour {

    board gameBoard;
    public Text numPlay;
    int curNumPlay = 0;

	// Use this for initialization
	void Start () {
        gameBoard = GameObject.Find("boardManager").GetComponent<board>();
        curNumPlay = gameBoard.numPlayers;
	}
	
	// Update is called once per frame
	void Update () {
        if(curNumPlay != gameBoard.numPlayers)
        {
            if (gameBoard.numPlayers > 0)
            {
                if (numPlay.text != gameBoard.numPlayers.ToString())
                {
                    numPlay.text = "num players: " + gameBoard.numPlayers.ToString();

                }
            }
        }


    }
}
