using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questUIBehavior : MonoBehaviour {

    public board thisBoard;
    public GameObject questPrefab;
    public List<GameObject> questPrefabs = new List<GameObject>();
    public List<Text> rescTexts = new List<Text>();

    public void displayCard(quest QClass, int num)
    {
        resetCard(num);
        questIdentifier thisQuest = questPrefabs[num].GetComponent<questIdentifier>();
        //display title
        thisQuest.title.text = QClass.name;

        //display symbols
        int symbolCount = 0;
        for(int n = 0; n < QClass.armor; n++)
        {
            thisQuest.symbols[symbolCount].gameObject.SetActive(true);
            thisQuest.symbols[symbolCount].sprite = thisQuest.symbolsPNG[0];
            symbolCount++;
        }
        for (int n = 0; n < QClass.daggers; n++)
        {
            thisQuest.symbols[symbolCount].gameObject.SetActive(true);
            thisQuest.symbols[symbolCount].sprite = thisQuest.symbolsPNG[1];
            symbolCount++;
        }
        for (int n = 0; n < QClass.fireball; n++)
        {
            thisQuest.symbols[symbolCount].gameObject.SetActive(true);
            thisQuest.symbols[symbolCount].sprite = thisQuest.symbolsPNG[2];
            symbolCount++;
        }
        for (int n = 0; n < QClass.helmet; n++)
        {
            thisQuest.symbols[symbolCount].gameObject.SetActive(true);
            thisQuest.symbols[symbolCount].sprite = thisQuest.symbolsPNG[3];
            symbolCount++;
        }
        for (int n = 0; n < QClass.sword; n++)
        {
            thisQuest.symbols[symbolCount].gameObject.SetActive(true);
            thisQuest.symbols[symbolCount].sprite = thisQuest.symbolsPNG[4];
            symbolCount++;
        }

        //display crafts
        thisQuest.craftTitle1.text = QClass.crafts[0].name;
        thisQuest.rescs1[0].text = QClass.crafts[0].ore.ToString();
        thisQuest.rescs1[1].text = QClass.crafts[0].wood.ToString();
        thisQuest.rescs1[2].text = QClass.crafts[0].hide.ToString();
        thisQuest.rescs1[3].text = QClass.crafts[0].cloth.ToString();
        thisQuest.rescs1[4].text = QClass.crafts[0].gem.ToString();
        thisQuest.rescs1[5].text = QClass.crafts[0].arcanum.ToString();
        thisQuest.coins[0].text = QClass.crafts[0].coin.ToString();
        thisQuest.treasures[0].sprite = thisQuest.treasuresPNG[QClass.crafts[0].chestType];

        if (QClass.crafts.Count > 1)
        {
            thisQuest.craftHolders[1].SetActive(true);
            thisQuest.craftHolders[0].GetComponent<RectTransform>().localPosition = new Vector3(-45, -88, 0);

            thisQuest.craftTitle2.text = QClass.crafts[1].name;
            thisQuest.rescs2[0].text = QClass.crafts[1].ore.ToString();
            thisQuest.rescs2[1].text = QClass.crafts[1].wood.ToString();
            thisQuest.rescs2[2].text = QClass.crafts[1].hide.ToString();
            thisQuest.rescs2[3].text = QClass.crafts[1].cloth.ToString();
            thisQuest.rescs2[4].text = QClass.crafts[1].gem.ToString();
            thisQuest.rescs2[5].text = QClass.crafts[1].arcanum.ToString();
            thisQuest.coins[1].text = QClass.crafts[1].coin.ToString();
            thisQuest.treasures[1].sprite = thisQuest.treasuresPNG[QClass.crafts[1].chestType];
        }

    }

    public void resetCard(int num)
    {
        questIdentifier thisQuest = questPrefabs[num].GetComponent<questIdentifier>();

        thisQuest.title.text = "Title";

        foreach (Image img in thisQuest.symbols)
        {
            img.gameObject.SetActive(false);
        }

        thisQuest.craftHolders[1].SetActive(false);
        thisQuest.craftHolders[0].GetComponent<RectTransform>().localPosition = new Vector3(0, -88, 0);
    }

    public void initCraftUI()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject newQuest = GameObject.Instantiate(questPrefab);
            newQuest.transform.SetParent(this.transform);
            questPrefabs.Add(newQuest);
        }

        int[] xpos = new int[] { -500, -250, 0, 250, 500 };
        for(int n = 0; n < questPrefabs.Count; n++)
        {
            displayCard(thisBoard.qDeck.aDeck[n], n);
            questPrefabs[n].GetComponent<RectTransform>().localPosition = new Vector3(xpos[n], 80, 0);
        }


    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //displayCard(deckLib_script.Instance.curLib.deck[0]);
            //thisBoard.qDeck.aDeck[0];
            //deckLib_script.Instance.curLib.deck[1]
        }
    }
}
