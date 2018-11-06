using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class playerNetworkSync : NetworkBehaviour {

    public board thisBoard;
    public GameObject canvas;
    
    public int playerInd;

    public static buildingStore DeserializeBFromNetwork(byte[] networkSerializedEffect)
    {
        MemoryStream stream = new MemoryStream(networkSerializedEffect);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        return (buildingStore)binaryFormatter.Deserialize(stream);
    }

    public byte[] SerializeBForNetwork(buildingStore serObj)
    {
        MemoryStream stream = new MemoryStream();
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(stream, serObj);
        return stream.GetBuffer();
    }

    public static specialistStore DeserializeSFromNetwork(byte[] networkSerializedEffect)
    {
        MemoryStream stream = new MemoryStream(networkSerializedEffect);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        return (specialistStore)binaryFormatter.Deserialize(stream);
    }

    public byte[] SerializeSForNetwork(specialistStore serObj)
    {
        MemoryStream stream = new MemoryStream();
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(stream, serObj);
        return stream.GetBuffer();
    }

    public static resourceStore DeserializeRFromNetwork(byte[] networkSerializedEffect)
    {
        MemoryStream stream = new MemoryStream(networkSerializedEffect);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        return (resourceStore)binaryFormatter.Deserialize(stream);
    }

    public byte[] SerializeRForNetwork(resourceStore serObj)
    {
        MemoryStream stream = new MemoryStream();
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(stream, serObj);
        return stream.GetBuffer();
    }

    public static questDeck DeserializeQFromNetwork(byte[] networkSerializedEffect)
    {
        MemoryStream stream = new MemoryStream(networkSerializedEffect);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        return (questDeck)binaryFormatter.Deserialize(stream);
    }

    public byte[] SerializeQForNetwork(questDeck serObj)
    {
        MemoryStream stream = new MemoryStream();
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(stream, serObj);
        return stream.GetBuffer();
    }

    public static treasureDeck DeserializeTFromNetwork(byte[] networkSerializedEffect)
    {
        MemoryStream stream = new MemoryStream(networkSerializedEffect);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        return (treasureDeck)binaryFormatter.Deserialize(stream);
    }

    public byte[] SerializeTForNetwork(treasureDeck serObj)
    {
        MemoryStream stream = new MemoryStream();
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(stream, serObj);
        return stream.GetBuffer();
    }

    public static dashBoardList DeserializePFromNetwork(byte[] networkSerializedEffect)
    {
        MemoryStream stream = new MemoryStream(networkSerializedEffect);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        return (dashBoardList)binaryFormatter.Deserialize(stream);
    }

    public byte[] SerializePForNetwork(dashBoardList serObj)
    {
        MemoryStream stream = new MemoryStream();
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(stream, serObj);
        return stream.GetBuffer();
    }

    [Command]
    void CmdClientTells()
    {
        print("I am client telling server to do this");
        RpcClientTellsClient();
    }

    [Command]
    void CmdAddPlayer()
    {
        thisBoard.numPlayers += 1;
        RpcServerNumPlayerSync(thisBoard.numPlayers);
    }

    [ClientRpc]
    void RpcServerTells()
    {
        print("I am server telling client to do this");
    }


    [ClientRpc]
    void RpcServerBoardSync(byte[] networkSerializedB, byte[] networkSerializedS, byte[] networkSerializedR,
                            byte[] networkSerializedQ, byte[] networkSerializedT, byte[] networkSerializedP, int curply)
    {
        thisBoard.bStore = DeserializeBFromNetwork(networkSerializedB);
        thisBoard.sStore = DeserializeSFromNetwork(networkSerializedS);
        thisBoard.rStore = DeserializeRFromNetwork(networkSerializedR);
        thisBoard.qDeck = DeserializeQFromNetwork(networkSerializedQ);
        thisBoard.tDeck = DeserializeTFromNetwork(networkSerializedT);
        thisBoard.dashboards = DeserializePFromNetwork(networkSerializedP);
        thisBoard.curPlayer = curply;
        print("board sync executed");
    }

    [ClientRpc]
    void RpcServerNumPlayerSync(int num)
    {
        thisBoard.numPlayers = num;
        if (isLocalPlayer)
        {
            playerInd = num;
        }
    }

    [ClientRpc]
    void RpcClientTellsClient()
    {
        print("I am client telling other clients to do this");
    }

    [ClientRpc]
    void RpcMoveForwardWithTurn()
    {
        canvas.GetComponent<canvasIdentifier>().waitOpts.SetActive(false);
        canvas.GetComponent<canvasIdentifier>().turnOpts.SetActive(false);
        if (playerInd != thisBoard.curPlayer)
        {
            print("num is " + thisBoard.curPlayer.ToString() + "player ind is " + playerInd.ToString());
            canvas.GetComponent<canvasIdentifier>().waitOpts.SetActive(true);
            Text waitText = canvas.GetComponent<canvasIdentifier>().waitOpts.GetComponentInChildren<Text>();
            waitText.text = "waiting for player " + thisBoard.curPlayer + " ...";
        }
        else if (playerInd == thisBoard.curPlayer)
        {
            print("num is " + thisBoard.curPlayer.ToString() + "player ind is " + playerInd.ToString() + "so im going");
            canvas.GetComponent<canvasIdentifier>().waitOpts.SetActive(false);
            canvas.GetComponent<canvasIdentifier>().turnOpts.SetActive(true);
        }

    }

    [ClientRpc]
    void RpcMoveFromBegin()
    {
        canvas.GetComponent<canvasIdentifier>().beginBtn.gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void initBoard()
    {
        if (isServer && isLocalPlayer)
        {
            thisBoard.initAll();
            syncBoard();
            RpcMoveFromBegin();
            RpcMoveForwardWithTurn();
        }
    }

    public void syncBoard()
    {
        RpcServerBoardSync(SerializeBForNetwork(thisBoard.bStore),
                            SerializeSForNetwork(thisBoard.sStore),
                            SerializeRForNetwork(thisBoard.rStore),
                            SerializeQForNetwork(thisBoard.qDeck),
                            SerializeTForNetwork(thisBoard.tDeck),
                            SerializePForNetwork(thisBoard.dashboards),
                            thisBoard.curPlayer);
    }

    [Command]
    public void CmdclientSyncRAndDash(byte[] netR, byte[] netD)
    {
        thisBoard.rStore = DeserializeRFromNetwork(netR);
        thisBoard.dashboards = DeserializePFromNetwork(netD);
        thisBoard.moveTurn();
        syncBoard();
        StartCoroutine(moveForwardAfter2Secs());
    }

    IEnumerator moveForwardAfter2Secs()
    {
        yield return new WaitForSeconds(1);
        thisBoard.players[thisBoard.curPlayer-1].GetComponent<playerNetworkSync>(). RpcMoveForwardWithTurn();
    }

    public void clientToServerSync()
    {
        if (canvas.GetComponent<canvasIdentifier>().rescUI.GetComponent<resourceUIBehavior>().limitClear)
        {
            if (isLocalPlayer)
            {
                CmdclientSyncRAndDash(SerializeRForNetwork(thisBoard.rStore), SerializePForNetwork(thisBoard.dashboards));
            }
        }
    }

    private void Start()
    {
        thisBoard = GameObject.Find("boardManager").GetComponent<board>();
        canvas = GameObject.Find("Canvas");
        Button nextBtn = canvas.GetComponent<canvasIdentifier>().rescUI.GetComponent<resourceUIBehavior>().nextButton.GetComponent<Button>();

        if (isServer)
        {
            canvas.GetComponent<canvasIdentifier>().beginBtn.gameObject.SetActive(true);
            canvas.GetComponent<canvasIdentifier>().beginBtn.onClick.AddListener(initBoard);
        }

        if (isClient && isLocalPlayer)
        {
            nextBtn.onClick.AddListener(clientToServerSync);
            CmdAddPlayer();
        }
        thisBoard.players.Add(this.gameObject);

    }

    // Update is called once per frame
    void Update() {
        if (isLocalPlayer)
        { 
        if (Input.GetButtonDown("Jump"))
        {
                thisBoard.initAll();
            }
        }

    }
}
