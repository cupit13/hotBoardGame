using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board : MonoBehaviour {
    public int numPlayers;
    public int curPlayer;

    public buildingStore bStore = new buildingStore();
    public specialistStore sStore = new specialistStore();
    public resourceStore rStore = new resourceStore();
    public questDeck qDeck = new questDeck();
    public treasureDeck tDeck = new treasureDeck();
    public dashBoardList dashboards = new dashBoardList();
    public List<GameObject> players = new List<GameObject>();
    bool hasBeenInit = false;

    // Use this for initialization
    void Start () {

    }
	
    public void initAll()
    {
        if (!hasBeenInit)
        {
            initMasterTiles();
            initWLGA();
            initSpecialist();
            initResource();
            initBR();
            initQuestI();
            initQuestII();
            initTreasure();
            initPlayers();
            initCoins();
            print("board initialized");
            hasBeenInit = true;
        }
        else
        {
            print("board was already init once");
        }

    }

    public void moveTurn()
    {
        int tempMod = curPlayer - 1;
        tempMod = (tempMod + 1) % numPlayers;
        curPlayer = tempMod + 1;
        print("curPlayer is "+ curPlayer.ToString() + " out of " + numPlayers.ToString());
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            //moveCard(tDeck.redDeck, playerBoards[0].hand);
            //if(players.playerBoards.Count > 0)
            //{
            //    plot nuPlot = new plot();
            //    players.playerBoards[0].updatePlot(0, nuPlot, 0);
            //}
            //moveTurn();
            string text = JsonUtility.ToJson(qDeck);
            print(text);
        }
    }

    void initMasterTiles()
    {
        Dictionary<int, string> masterChart = new Dictionary<int, string>();
        masterChart.Add(1, "hallOfMages");
        masterChart.Add(2, "hallOfRogues");
        masterChart.Add(3, "hallOfWarriors");
        masterChart.Add(4, "hallOfCunning");
        masterChart.Add(5, "hallOfStrength");
        masterChart.Add(6, "hallOfWisdom");
        masterChart.Add(7, "armory");
        masterChart.Add(8, "arsenal");
        masterChart.Add(9, "gallery");
        masterChart.Add(10, "depot");
        masterChart.Add(11, "hallOfBuilders");
        masterChart.Add(12, "tavern");
        masterChart.Add(13, "throneRoom");
        masterChart.Add(14, "vaultOfRiches");

        int tiles = 14;
        int limit = 3 + numPlayers;
        List<int> blackList = new List<int>();
        for (int i=0; i < limit; i++)
        {

            int ran = Random.Range(1, tiles+1);
            if (blackList.Contains(ran))
                {
                i--;
                }
                else
                {
                buildingTile nuTile = new buildingTile();
                nuTile.name = masterChart[ran];
                nuTile.ind = ran;
                bStore.halls.Add(nuTile);
                blackList.Add(ran);
                }

        }
    }

    void initWLGA()
    {
        Dictionary<int, int> WLchart = new Dictionary<int, int>();
        WLchart.Add(1,1);
        WLchart.Add(2, 1);
        WLchart.Add(3, 2);
        WLchart.Add(4, 3);
        WLchart.Add(5, 3);
        WLchart.Add(6, 4);

        bStore.warehouseNum = WLchart[numPlayers];
        bStore.libraryNum = WLchart[numPlayers];

        Dictionary<int, int> GAchart = new Dictionary<int, int>();
        GAchart.Add(1, 1);
        GAchart.Add(2, 1);
        GAchart.Add(3, 2);
        GAchart.Add(4, 2);
        GAchart.Add(5, 3);
        GAchart.Add(6, 3);

        bStore.gemNum = GAchart[numPlayers];
        bStore.arcanumNum = GAchart[numPlayers];
    }

    void initSpecialist()
    {
        Dictionary<int, string> specChart = new Dictionary<int, string>();
        specChart.Add(1, "miner");
        specChart.Add(2, "lumberjack");
        specChart.Add(3, "tanner");
        specChart.Add(4, "weaver");
        specChart.Add(5, "oracle");
        specChart.Add(6, "courier");
        specChart.Add(7, "builder");
        specChart.Add(8, "pirate");
        specChart.Add(9, "inventor");
        specChart.Add(10, "appraiser");
        specChart.Add(11, "laborer");
        specChart.Add(12, "thug");
        specChart.Add(13, "recruiter");

        specChart.Add(14, "blacksmith");
        specChart.Add(15, "leatherworker");
        specChart.Add(16, "woodworker");
        specChart.Add(17, "tailor");
        specChart.Add(18, "peasant");
        specChart.Add(19, "carpenter");
        specChart.Add(20, "auctioneer");
        specChart.Add(21, "ranger");
        specChart.Add(22, "wizard");
        specChart.Add(23, "fighter");
        specChart.Add(24, "warlock");
        specChart.Add(25, "sorceress");
        specChart.Add(26, "bard");

        specChart.Add(27, "overseer");
        specChart.Add(28, "minstrel");
        specChart.Add(29, "aristocrat");
        specChart.Add(30, "curator");
        specChart.Add(31, "merchant");
        specChart.Add(32, "dealer");
        specChart.Add(33, "craftswoman");
        specChart.Add(34, "smuggler");
        specChart.Add(35, "doppleganger");
        specChart.Add(36, "alchemist");
        specChart.Add(37, "adventurer");
        specChart.Add(38, "vizier");
        specChart.Add(39, "witch");

        for (int abc = 0; abc < 3; abc++)
        {
            int tiles = 13;
            int limit = 4 + numPlayers;
            if (numPlayers <= 2)
            {
                limit = 3 + numPlayers;
            }

            List<int> blackList = new List<int>();
            for (int i = 0; i < limit; i++)
            {

                int ran = Random.Range(1, tiles + 1);
                if (blackList.Contains(ran))
                {
                    i--;
                }
                else
                {
                    specialist nuTile = new specialist();
                    int realRan = ran + (tiles * abc);
                    nuTile.name = specChart[realRan];
                    nuTile.ind = realRan;
                    nuTile.abc = abc;
                    sStore.deck.Add(nuTile);
                    blackList.Add(ran);
                }

            }
        }
        for (int s = 0; s < 5; s++)
        {
            specialist tempSpec = sStore.deck[s];
            sStore.aDeck.Add(sStore.deck[s]);
            sStore.deck.Remove(tempSpec);
        }
    }

    void initResource()
    {
        int rescTot = 6;

        for(int n=0; n < rescTot; n++)
        {
            resource nuResc = new resource();
            rStore.rescList.Add(nuResc);
        }

        Dictionary<int, int> OWHCchart = new Dictionary<int, int>();
        OWHCchart.Add(1, 5);
        OWHCchart.Add(2, 5);
        OWHCchart.Add(3, 6);
        OWHCchart.Add(4, 7);
        OWHCchart.Add(5, 8);
        OWHCchart.Add(6, 8);

        for(int rc = 0; rc < 4; rc++)
        {
            rStore.rescList[rc].boardCount = OWHCchart[numPlayers];
        }

        Dictionary<int, int> GAchart = new Dictionary<int, int>();
        GAchart.Add(1, 3);
        GAchart.Add(2, 3);
        GAchart.Add(3, 4);
        GAchart.Add(4, 5);
        GAchart.Add(5, 6);
        GAchart.Add(6, 6);

        for (int rc = 4; rc < 6; rc++)
        {
            rStore.rescList[rc].boardCount = GAchart[numPlayers];
        }
    }

    void initBR()
    {
        bStore.bedroom = numPlayers;
        bStore.barrack = numPlayers;
    }

    void initQuestI()
    {
        int tiles = 12;
        int limit = 4 + numPlayers;
        if (numPlayers <= 2)
        {
            limit = 3 + numPlayers;
        }
        List<quest> tempQuestDeck = new List<quest>();

        for (int age = 0; age < 2; age++)
        {
            List<int> blackList = new List<int>();

            for (int i = 0; i < limit; i++)
            {

                int ran = Random.Range(1, tiles + 1);
                if (blackList.Contains(ran))
                {
                    i--;
                }
                else
                {
                    quest nuTile = new quest();
                    int realRan = ran + (tiles * age);
                    nuTile = deckLib_script.Instance.curLib.deck[realRan-1];
                    //nuTile.name = realRan.ToString();//specChart[realRan];
                    //nuTile.ind = realRan;
                    //nuTile.age = age;
                    tempQuestDeck.Add(nuTile);
                    blackList.Add(ran);
                }

            }
        }

        limit = limit * 2;
        List<int> blackList2 = new List<int>();

        for (int i = 0; i < limit; i++)
        {
            int ran = Random.Range(0, tempQuestDeck.Count);
            if (blackList2.Contains(ran))
            {
                i--;
            }
            else
            {
                qDeck.deck.Add(tempQuestDeck[ran]);
                blackList2.Add(ran);
            }
        }
    }

    void initQuestII()
    {
        quest funeral = new quest();
        funeral.name = "funeral";

        qDeck.deck.Add(funeral);

        Dictionary<int, int> ageIIchart = new Dictionary<int, int>();
        ageIIchart.Add(1, 8);
        ageIIchart.Add(2, 8);
        ageIIchart.Add(3, 12);
        ageIIchart.Add(4, 14);
        ageIIchart.Add(5, 17);
        ageIIchart.Add(6, 19);

        int age = 2;
        int tiles = 24;
        int limit = ageIIchart[numPlayers];

        List<int> blackList = new List<int>();
        for (int i = 0; i < limit; i++)
        {

            int ran = Random.Range(1, tiles + 1);
            if (blackList.Contains(ran))
            {
                i--;
            }
            else
            {
                quest nuTile = new quest();
                int realRan = ran + (tiles);
                nuTile.name = realRan.ToString();//specChart[realRan];
                nuTile.ind = realRan;
                nuTile.age = age;
                qDeck.deck.Add(nuTile);
                blackList.Add(ran);
            }

        }
        int qAvail = 5;
        if (numPlayers > 4)
        {
            qAvail = 6;
        }

        List<quest> tempSpec = new List<quest>();

        for (int s = 0; s < qAvail; s++)
        {
            tempSpec.Add(qDeck.deck[s]);
            qDeck.aDeck.Add(qDeck.deck[s]);
        }

        for (int s = 0; s < qAvail; s++)
        {
            qDeck.deck.Remove(tempSpec[s]);
        }

        quest offering = new quest();
        offering.name = "offering";

        qDeck.deck.Add(offering);
    }

    void initTreasure()
    {
        treasureRand(28,tDeck.redDeck);
        treasureRand(28, tDeck.blueDeck);
        treasureRand(28, tDeck.yellowDeck);
    }

    void initPlayers()
    {
        int plots = 6;
        int guilds = 6;
        List<int> blackListPlot = new List<int>();
        List<int> blackListGuild = new List<int>();

        //generate dashboards
        for (int p = 1; p <= numPlayers; p++)
        {
            dashboard nuPlayer = new dashboard();
            nuPlayer.pInd = p;
            nuPlayer.name = "player" + (p.ToString());

            dashboards.playerBoards.Add(nuPlayer);
        }

        //randomize the plotboards
        for (int i = 0; i < dashboards.playerBoards.Count; i++)
        {

            int ran = Random.Range(1, plots + 1);
            if (blackListPlot.Contains(ran))
            {
                i--;
            }
            else
            {
                dashboards.playerBoards[i].initPlots(ran);
                blackListPlot.Add(ran);
            }
        }

        //randomize the starter guild
        for (int i = 0; i < dashboards.playerBoards.Count; i++)
        {

            int ran = Random.Range(1, guilds + 1);
            if (blackListGuild.Contains(ran))
            {
                i--;
            }
            else
            {
                dashboards.playerBoards[i].starterType = ran;
                blackListGuild.Add(ran);
            }
        }
    }

    void initCoins()
    {
        int[] coins = { 3, 4, 5, 5, 6, 6};

        for(int p=0;p< dashboards.playerBoards.Count;p++)
        {
            int player = (((curPlayer-1) + p) % numPlayers) + 1;
            dashboards.playerBoards[player-1].coin += coins[p];
        }
    }

    void treasureRand(int num, List<treasure> deck)
    {
        int tiles = num;
        int limit = num;
        List<int> blackList = new List<int>();
        for (int i = 0; i < limit; i++)
        {

            int ran = Random.Range(1, tiles + 1);
            if (blackList.Contains(ran))
            {
                i--;
            }
            else
            {
                treasure nuTile = new treasure();
                nuTile.name = ran.ToString();// masterChart[ran];
                nuTile.ind = ran;
                deck.Add(nuTile);
                blackList.Add(ran);
            }

        }
    }

    void moveCard(List<treasure> from, List<treasure> to)
    {
        treasure topCard = from[0];
        from.Remove(topCard);
        to.Add(topCard);
    }

}

[System.Serializable]
public class buildingStore : System.Object
{
    public List<buildingTile> halls = new List<buildingTile>();
    public bool kingStatue = true;
    public int warehouseNum;
    public int libraryNum;
    public int gemNum;
    public int arcanumNum;
    public int bedroom;
    public int barrack;
}

[System.Serializable]
public class specialistStore: System.Object
{
    public List<specialist> deck = new List<specialist>();
    public List<specialist> aDeck = new List<specialist>();

}

[System.Serializable]
public class resourceStore : System.Object
{
    public List<resource> rescList = new List<resource>();
}

[System.Serializable]
public class questDeck: System.Object
{
    public List<quest> deck = new List<quest>();
    public List<quest> aDeck = new List<quest>();
}

[System.Serializable]
public class treasureDeck : System.Object
{
    public List<treasure> redDeck = new List<treasure>();
    public List<treasure> yellowDeck = new List<treasure>();
    public List<treasure> blueDeck = new List<treasure>();

    public List<treasure> redDisc = new List<treasure>();
    public List<treasure> yellowDisc = new List<treasure>();
    public List<treasure> blueDisc = new List<treasure>();
}

[System.Serializable]
public class dashBoardList: System.Object
{
    public List<dashboard> playerBoards = new List<dashboard>();
}
