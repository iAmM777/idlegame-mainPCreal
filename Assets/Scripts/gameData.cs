using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BreakInfinity;
[System.Serializable]

public class gameData
{
    public BigDouble Beans;
    public List<int> clickUpgradeLevel;
    public List<int> productionUpgradeLevel;

    public gameData()
    {
        Beans = 0;
        clickUpgradeLevel = new int[4].ToList();
        productionUpgradeLevel = new int[4].ToList();
    } 
}
