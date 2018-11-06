using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class networktest : NetworkBehaviour {

    public int thisInd;

	[ClientRpc]
    public void RpcOrderOthers(int sender)
    {
        print("sender:" + sender.ToString()+", executed by:"+thisInd.ToString());
    }

    [Command]
    public void CmdShoutOut(int ID)
    {
        RpcOrderOthers(ID);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isLocalPlayer)
            {
                CmdShoutOut(thisInd);
            }
        }
    }

    private void Start()
    {
        GameObject pObject = GameObject.Find("PlayerList");
        thisInd = pObject.GetComponent<playerList>().players.Count;
        this.name = "Player_" + thisInd.ToString();
        pObject.GetComponent<playerList>().players.Add(gameObject);
    }
}
