using BreakInfinity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{
    public List<Upgrades> Upgrades;
    public Upgrades UpgradePrefab;
    public ScrollRect UpgradesScroll;
    public Transform UpgradesPanel;
    public string[] UpgradeNames;

    public BigDouble[] UpgradeBaseCost;
    public BigDouble[] UpgradeCostMult;
    public BigDouble[] UpgradeBasePower;
    public BigDouble[] UpgradesUnlock;

}
