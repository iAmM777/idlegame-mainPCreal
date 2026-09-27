using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BreakInfinity;
[System.Serializable]

public class gameData
{
    public BigDouble Beans;
    public List<BigDouble> clickUpgradeLevel;

    public gameData()
    {
        Beans = 0;
        clickUpgradeLevel = new BigDouble[4].ToList();
    }
}
