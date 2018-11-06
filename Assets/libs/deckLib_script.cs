using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deckLib_script : Singleton<deckLib_script> {

    public questDeckLib curLib = new questDeckLib();
    public TextAsset jsonTextFile;

    [System.Serializable]
    public class questDeckLib : System.Object
    {
        public List<quest> deck = new List<quest>();
    }

    private void Start()
    {
        curLib = JsonUtility.FromJson<questDeckLib>(jsonTextFile.text);
    }
}
