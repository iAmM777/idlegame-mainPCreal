using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BreakInfinity;
[System.Serializable]

public class gameData
{
    public BigDouble Beans;

    public List<int> clickUpgradeLevel;
    public List<BigDouble> productionUpgradeLevel;
    public List<BigDouble> productionUpgradeGenerated;
    public List<int> generatorUpgradeLevel;


    public gameData() 
    {
        Beans = 0;
        clickUpgradeLevel = new int[4].ToList();
        productionUpgradeLevel = new BigDouble[4].ToList();
        productionUpgradeGenerated = new BigDouble[4].ToList();
        generatorUpgradeLevel = new int[4].ToList();
    } 
}
