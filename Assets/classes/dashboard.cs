using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class dashboard : System.Object {
    public string name;
    public int pInd;
    public int coin;
    public int vp;
    public int starterType;
    public int rescLimit = 5;
    public List<treasure> hand = new List<treasure>();
    public List<specialist> spec = new List<specialist>();
    public List<resource> resc = new List<resource>();
    public List<quest> ques = new List<quest>();
    public List<buildingTile> buil = new List<buildingTile>();
    public List<Grid2> tplots = new List<Grid2>();
    public List<Grid2> aplots = new List<Grid2>();
    public plot plot;
    int he = 4;
    int wi = 4;
    int tot =16;
	
    public void updatePlot(int gridInd, plot nuPlot, int xGrid= 0)
    {
        int wiMod = (gridInd % wi) + 1;
        int heMod = (gridInd / he) + 1;
        Grid2 nuGrid = new Grid2();
        nuGrid.x = wiMod;
        nuGrid.y = heMod;
        tplots.Add(nuGrid);
        removePlot(nuGrid.x, nuGrid.y);
        if (xGrid > 0)
        {
            wiMod = (xGrid % wi) + 1;
            heMod = (xGrid / he) + 1;
            Grid2 nuGrid2 = new Grid2();
            nuGrid2.x = wiMod;
            nuGrid2.y = heMod;
            tplots.Add(nuGrid2);
            removePlot(nuGrid2.x, nuGrid2.y);
        }
        plot = nuPlot;
    }

    void removePlot(int x, int y)
    {
        int indToRemove = -1;
        for (int i= 0; i < aplots.Count; i++)
        {
            if(aplots[i].x == x && aplots[i].y == y)
            {
                indToRemove = i;
            }
        }
        aplots.Remove(aplots[indToRemove]);
    }

    public void initPlots(int plotNum)
    {
        for (int i=0; i< tot; i++){
            int wiMod = (i % wi) + 1;
            int heMod = (i / he) + 1;
            Grid2 nuGrid = new Grid2();
            nuGrid.x = wiMod;
            nuGrid.y = heMod;
            aplots.Add(nuGrid);
        }
        plot starterPlot = new plot();
        starterPlot.ind = plotNum;
        updatePlot(1, starterPlot, 2);
    }

    public int ResourceCount
    {
        get
        {
            return resc.Count;
        }
    }

    
    //public void Add(List<resource> resourceList)
    //{
    //    from r in resourceList select r f
    //    resc.Add()


  
    //}

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire"))
        {

        }
	}
}
