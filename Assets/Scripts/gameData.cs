using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
[System.Serializable]

public class gameData
{
    public BigDouble Beans;
    public List<BigDouble> clickUpgradeLevel;

    public gameData()
    {
        Beans = 0;
        clickUpgradeLevel = Methods.CreateList<BigDouble>(3);
    }
}
