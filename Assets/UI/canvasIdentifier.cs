using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasIdentifier : MonoBehaviour {

    public Button beginBtn;
    public GameObject turnOpts;
    public GameObject waitOpts;
    public GameObject rescUI;

    public void initRescUI()
    {
        turnOpts.SetActive(false);
        rescUI.SetActive(true);
        rescUI.GetComponent<resourceUIBehavior>().collectGrp.SetActive(true);
        rescUI.GetComponent<resourceUIBehavior>().returnGrp.SetActive(false);
        rescUI.GetComponent<resourceUIBehavior>().reset();
    }
}
