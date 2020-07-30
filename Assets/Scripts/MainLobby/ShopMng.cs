using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UpgradeMng : MonoBehaviour
{
    public void HeroCoinToChip()
    {
        if (_HeroUpgradeCoin < 10)
            return;

        _HeroUpgradeCoin -= 10;
        GambleMng.Data.AddChip(1);
        SetPlayerPref_Upgrade();
    }
    public void ShapeCoinToChip()
    {
        if (_ShapeUpgradeCoin < 10)
            return;

        _ShapeUpgradeCoin -= 10;
        GambleMng.Data.AddChip(1);
        SetPlayerPref_Upgrade();
    }
}
