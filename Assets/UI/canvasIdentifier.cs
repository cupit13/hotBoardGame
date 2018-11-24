using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasIdentifier : MonoBehaviour {

    public Button beginBtn;
    public GameObject turnOpts;
    public GameObject waitOpts;
    public GameObject rescUI;
    public GameObject craftUI;

    public void initRescUI()
    {
        turnOpts.SetActive(false);
        rescUI.SetActive(true);
        rescUI.GetComponent<resourceUIBehavior>().collectGrp.SetActive(true);
        rescUI.GetComponent<resourceUIBehavior>().returnGrp.SetActive(false);
        rescUI.GetComponent<resourceUIBehavior>().reset();
    }

    public void initCraftUI()
    {
        turnOpts.SetActive(false);
        craftUI.SetActive(true);
        craftUI.GetComponent<questUIBehavior>().initCraftUI();
        for(int n = 0; n < craftUI.GetComponent<questUIBehavior>().questPrefabs.Count; n++)
        {
            craftUI.GetComponent<questUIBehavior>().questPrefabs[n].SetActive(true);
        }
    }
}
