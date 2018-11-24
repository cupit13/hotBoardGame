using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resourceUIBehavior : MonoBehaviour {

    public board thisBoard;
    public GameObject collectGrp;
    public GameObject returnGrp;

    public Text[] resTexts;

    public int[] resCount;

    public GameObject[] allowObjs;

    public bool[] allow;

    int resourceLimit;
    List<resource> tempRepo = new List<resource>();
    string[] nameList = {"ore",
                         "wood",
                         "hide",
                         "cloth",
                         "gem",
                         "pot" };

    public GameObject nextButton;
    public GameObject[] optObjs;

    public bool limitClear =false;
    public Text[] retTexts;
    public int[] retCount;
    public Text limitText;
    List<resource> tempReturn = new List<resource>();
    int curResc;
    int warehouseLimit;
    bool nextReturn;
    public bool[] retAllow;

    public List<Sprite> rescPNGs = new List<Sprite>();

    void syncAllow()
    {
        for(int n =0;n<allow.Length;n++)
        {
            if (resCount[n] < 1)
            {
                allow[n] = false;
            }
            allowObjs[n].SetActive(allow[n]);
        }
    }

    void allowFalseAll()
    {
        for (int n = 0; n < allow.Length; n++)
        {
            allow[n] = false;
        }
        nextButton.SetActive(true);
    }

    void allowFalseAllExcept(int exceptInd)
    {
        for (int n = 0; n < allow.Length; n++)
        {
            if(n != exceptInd)
            {
                allow[n] = false;
            }
        }
        nextButton.SetActive(true);
    }

    void setColor(int optInd, int rescInd)
    {
        Image imageObj = resTexts[rescInd].gameObject.transform.parent.gameObject.GetComponent<Button>().targetGraphic.GetComponent<Image>();
        //optObjs[optInd].GetComponent<Image>().color = imageObj.color;
        optObjs[optInd].GetComponent<Image>().sprite = rescPNGs[rescInd];
    }

    public void ClickThis(int resInd)
    {
        if (allow[resInd])
        {
            resCount[resInd] -= 1;
            //set color for picked up obj
            setColor(3 - resourceLimit, resInd);
            optObjs[3-resourceLimit].SetActive(true);
            resourceLimit -= 1;
            resTexts[resInd].text = resCount[resInd].ToString();
            resource nuRes = new resource();
            nuRes.name = nameList[resInd];
            nuRes.rescInd = resInd;
            tempRepo.Add(nuRes);
            if (tempRepo.Count > 1)
            {
                if (tempRepo[0].name != tempRepo[1].name)
                {
                    allowFalseAll();
                }
                else
                {
                    allowFalseAllExcept(resInd);
                }
            }
            if (resourceLimit < 1)
            {
                allowFalseAll();
            }
        }
        syncAllow();
    }

    public void reset()
    {
        tempRepo.Clear();
        resourceLimit = 3;
        foreach(GameObject obj in optObjs)
        {
            obj.SetActive(false);
        }
        for(int i = 0; i < resCount.Length; i++)
        {
            resCount[i] = thisBoard.rStore.rescList[i].boardCount;
            resTexts[i].text = resCount[i].ToString();
        }
        for (int n = 0; n < allow.Length; n++)
        {
            allow[n] = true;
        }
        allow[4] = false;
        allow[5] = false;
        syncAllow();
        nextButton.SetActive(false);
        nextReturn = false;
    }

    public void checkOverFlow()
    {
        curResc = thisBoard.dashboards.playerBoards[thisBoard.curPlayer - 1].resc.Count;
        warehouseLimit = thisBoard.dashboards.playerBoards[thisBoard.curPlayer - 1].rescLimit;
        nextButton.SetActive(false);
        if(curResc > warehouseLimit)
        {
            collectGrp.SetActive(false);
            returnGrp.SetActive(true);
            limitText.color = new Color(.868f,0, 0);
            returnReset();
            limitClear = false;
            nextReturn = true;
        }
        else
        {
            limitClear = true;
            nextReturn = false;
            gameObject.SetActive(false);
        }
    }

    void returnReset()
    {
        nextButton.SetActive(false);
        tempReturn.Clear();

        for (int n=0;n< retCount.Length;n++)
        {
            retCount[n] = 0;
            retAllow[n] = false;
        }

        foreach (resource res in thisBoard.dashboards.playerBoards[thisBoard.curPlayer - 1].resc)
        {
            retCount[res.rescInd] += 1;
            retAllow[res.rescInd] = true;
        }

        for (int n = 0; n < retCount.Length; n++)
        {
            retTexts[n].text = retCount[n].ToString();
        }
        limitText.text = curResc.ToString() + "/" + warehouseLimit.ToString();
    }

    public void returnThis(int resInd)
    {
        if (retAllow[resInd])
        {
            curResc -= 1;
            retCount[resInd] -= 1;
            for (int n = 0; n < retCount.Length; n++)
            {
                retTexts[n].text = retCount[n].ToString();
            }
            limitText.text = curResc.ToString() + "/" + warehouseLimit.ToString();

            resource nuRet = new resource();
            nuRet.name = nameList[resInd];
            nuRet.rescInd = resInd;

            tempReturn.Add(nuRet);

            if (curResc <= warehouseLimit)
            {
                nextButton.SetActive(true);
                limitText.color = new Color(0, .868f, 0);
            }
        }

    }

    public void rescNextTurn()
    {
        if (!nextReturn)
        {
            for (int n = 0; n < tempRepo.Count; n++)
            {
                print("player" + thisBoard.curPlayer.ToString() + " is taking " + tempRepo[n].name);
                thisBoard.rStore.rescList[tempRepo[n].rescInd].boardCount -= 1;
                thisBoard.dashboards.playerBoards[thisBoard.curPlayer - 1].resc.Add(tempRepo[n]);
            }
        }
        else
        {
            List<int> indexesToIgnore = new List<int>();
            List<resource> forRemoval = new List<resource>();
            List<resource> toKeep = new List<resource>();

            for (int n = 0; n < tempReturn.Count; n++)
            {
                forRemoval.Add(tempReturn[n]);
            }

            // create a copy of resources of what the current player has
            for (int r = 0; r < thisBoard.dashboards.playerBoards[thisBoard.curPlayer - 1].ResourceCount; r++)
            {
                toKeep.Add(thisBoard.dashboards.playerBoards[thisBoard.curPlayer - 1].resc[r]);
            }

            // loop through all of the items to be returned
            for (int n = 0; n < tempReturn.Count; n++)
            {
                print("player" + thisBoard.curPlayer.ToString() + " is returning " + tempReturn[n].name);
                thisBoard.rStore.rescList[tempReturn[n].rescInd].boardCount += 1;

                // loop through all of the resources of the current player
                for (int r = 0; r < thisBoard.dashboards.playerBoards[thisBoard.curPlayer - 1].ResourceCount; r++)
                {
                    if (!indexesToIgnore.Contains(r))
                    {
                        // if the resource type e.g. ore or wood matches then we are removing it
                        if (thisBoard.dashboards.playerBoards[thisBoard.curPlayer - 1].resc[r].rescInd == tempReturn[n].rescInd)
                        {
                            forRemoval.Remove(tempReturn[n]);
                            toKeep.Remove(thisBoard.dashboards.playerBoards[thisBoard.curPlayer - 1].resc[r]);
                            indexesToIgnore.Add(r);
                            break;
                            //r = thisBoard.dashboards.playerBoards[thisBoard.curPlayer - 1].resc.Count;
                        }
                    }                    
                }
            }
            thisBoard.dashboards.playerBoards[thisBoard.curPlayer - 1].resc = toKeep;


        }
        checkOverFlow();

    }

    //void Update()
    //{
    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        print(limitText.color);
            
    //    }
    //}
}

    
