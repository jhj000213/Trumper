using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UpgradeMng : MonoBehaviour
{

    private void Start()
    {
        //PlayerPrefs.SetInt("soldierlevel", 40);
        //PlayerPrefs.SetInt("summoncount_sword", 1);
        //PlayerPrefs.SetInt("summoncount_archer", 1);
        //PlayerPrefs.SetInt("summoncount_shield", 1);
        //PlayerPrefs.SetInt("summoncount_spear", 1);
        //
        //PlayerPrefs.SetInt("skill_sword", 1);
        //PlayerPrefs.SetInt("skill_archer", 1);
        //PlayerPrefs.SetInt("skill_shield", 1);
        //PlayerPrefs.SetInt("skill_spear", 1);
        //PlayerPrefs.SetInt("skill_seven", 1);
        //
        //PlayerPrefs.SetInt("skilllevel_hero_king", 8);
        //PlayerPrefs.SetInt("skilllevel_hero_ace", 8);
        //PlayerPrefs.SetInt("skilllevel_hero_jack", 8);
        //PlayerPrefs.SetInt("skilllevel_hero_queen", 8);
        //
        //PlayerPrefs.SetInt("shapelevel_spade", 7);
        //PlayerPrefs.SetInt("shapelevel_heart", 7);
        //PlayerPrefs.SetInt("shapelevel_diamond", 7);
        //PlayerPrefs.SetInt("shapelevel_club", 7);

        PlayerPrefsStart();
        TextUpdate();
        SelectTab(0);
        HeroSelectTab(PlayerPrefs.GetInt("heronumber", 1) - 1);
        ShapeSelectTab(0);

        
    }

    public void SelectTab(int n)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.UI_Click, 0);
        for (int i = 0; i < 5; i++)
        {
            _SoldierUpgradeTabs[i].SetActive(false);
            _UpgradeTabsChoices[i].SetActive(false);
        }
        _SoldierUpgradeTabs[n].SetActive(true);
        _UpgradeTabsChoices[n].SetActive(true);
    }
    public void HeroSelectTab(int n)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.UI_Click, 0);
        for (int i = 0; i < 4; i++)
        {
            _HeroUpgradeTabs[i].SetActive(false);
            _HeroTabsChoice[i].SetActive(false);
        }
        _HeroUpgradeTabs[n].SetActive(true);
        _HeroTabsChoice[n].SetActive(true);
    }
    public void ShapeSelectTab(int n)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.UI_Click, 0);
        for (int i = 0; i < 4; i++)
        {
            _ShapeUpgradeTabs[i].SetActive(false);
            _ShapeTabsChoice[i].SetActive(false);
        }
        _ShapeUpgradeTabs[n].SetActive(true);
        _ShapeTabsChoice[n].SetActive(true);
    }

    void PlayerPrefsStart()
    {

        _Coin = PlayerPrefs.GetInt("coin", 0);
        _ShapeUpgradeCoin = PlayerPrefs.GetInt("shape_upgradecoin", 0);
        _HeroUpgradeCoin = PlayerPrefs.GetInt("hero_upgradecoin", 0);

        _SkillCoin = PlayerPrefs.GetInt("skillcoin", 0);

        _SummonCount_Soldier_Sword = PlayerPrefs.GetInt("summoncount_sword", 1);
        _SummonCount_Soldier_Archer = PlayerPrefs.GetInt("summoncount_archer", 1);
        _SummonCount_Soldier_Shield = PlayerPrefs.GetInt("summoncount_shield", 1);
        _SummonCount_Soldier_Spear = PlayerPrefs.GetInt("summoncount_spear", 1);
        _SkillLevel_Soldier_Seven = PlayerPrefs.GetInt("skill_seven", 0);

        _Skill_Level_Soldier_Hero_King = PlayerPrefs.GetInt("skilllevel_hero_king", 1);
        _Skill_Level_Soldier_Hero_Ace = PlayerPrefs.GetInt("skilllevel_hero_ace", 1);
        _Skill_Level_Soldier_Hero_Jack = PlayerPrefs.GetInt("skilllevel_hero_jack", 1);
        _Skill_Level_Soldier_Hero_Queen = PlayerPrefs.GetInt("skilllevel_hero_queen", 1);

        _SoldierLevel = PlayerPrefs.GetInt("soldierlevel", 1);
        _SkillLevel_Soldier_Sword = PlayerPrefs.GetInt("skill_sword", 0);
        _SkillLevel_Soldier_Archer = PlayerPrefs.GetInt("skill_archer", 0);
        _SkillLevel_Soldier_Shield = PlayerPrefs.GetInt("skill_shield", 0);
        _SkillLevel_Soldier_Spear = PlayerPrefs.GetInt("skill_spear", 0);

        _Level_Shape_Spade = PlayerPrefs.GetInt("shapelevel_spade", 1);
        _Level_Shape_Heart = PlayerPrefs.GetInt("shapelevel_heart", 1);
        _Level_Shape_Diamond = PlayerPrefs.GetInt("shapelevel_diamond", 1);
        _Level_Shape_Club = PlayerPrefs.GetInt("shapelevel_club", 1);

        _MainGroundSoldierMng.SetOnCount();
    }

    public void CoinUp(int n)
    {
        _Coin += n;
        TextUpdate();
        SetPlayerPref_Upgrade();
    }
    public void HeroCoinUp(int n)
    {
        _HeroUpgradeCoin += n;
        TextUpdate();
        SetPlayerPref_Upgrade();
    }
    public void ShapeCoinUp(int n)
    {
        _ShapeUpgradeCoin += n;
        TextUpdate();
        SetPlayerPref_Upgrade();
    }
    public void SkillCoinUp(int n)
    {
        _SkillCoin += n;
        TextUpdate();
        SetPlayerPref_Upgrade();
    }

    void TextUpdate()
    {
        _CoinText.text = string.Format("{0:#,0}", _Coin);
        _HeroUpgradeCoinText.text = string.Format("{0:#,0}", _HeroUpgradeCoin);
        _ShapeUpgradeCoinText.text = string.Format("{0:#,0}", _ShapeUpgradeCoin);

        _SoldierSkillCoinText.text = _SkillCoin.ToString();

        //Soldier
        if (_SoldierLevel >= 60)
            _SoldierLevelPriceText.text = "Max";
        else
            _SoldierLevelPriceText.text = string.Format("{0:#,0}", MyUnitInfoMng.Data._SoldierLevelPrice[_SoldierLevel - 1].ToString());
        _SoldierLevelText.text = _SoldierLevel.ToString();

        _SkillLevelText_Sword.text = _SkillLevel_Soldier_Sword + " / " + _SKILLLEVEL_SOLDIER_SWORD_MAX;
        _SkillLevelText_Archer.text = _SkillLevel_Soldier_Archer + " / " + _SKILLLEVEL_SOLDIER_ARCHER_MAX;
        _SkillLevelText_Shield.text = _SkillLevel_Soldier_Shield + " / " + _SKILLLEVEL_SOLDIER_SHIELD_MAX;
        _SkillLevelText_Spear.text = _SkillLevel_Soldier_Spear + " / " + _SKILLLEVEL_SOLDIER_SPEAR_MAX;
        _SkillLevelText_Seven.text = _SkillLevel_Soldier_Seven + " / " + _SKILLLEVEL_SOLDIER_SEVEN_MAX;

        _SummonCountText_Sword.text = (_SummonCount_Soldier_Sword - 1) + " / " + (_SUMMONCOUNT_SOLDIER_SWORD_MAX - 1);
        _SummonCountText_Archer.text = (_SummonCount_Soldier_Archer - 1) + " / " + (_SUMMONCOUNT_SOLDIER_ARCHER_MAX - 1);
        _SummonCountText_Shield.text = (_SummonCount_Soldier_Shield - 1) + " / " + (_SUMMONCOUNT_SOLDIER_SHIELD_MAX - 1);
        _SummonCountText_Spear.text = (_SummonCount_Soldier_Spear - 1) + " / " + (_SUMMONCOUNT_SOLDIER_SPEAR_MAX - 1);

        _SkillCostText_Sword.text = _SkillLevel_Soldier_Sword>=_SKILLLEVEL_SOLDIER_SWORD_MAX?"Max":_SkillLevelUpCost_Sword[_SkillLevel_Soldier_Sword].ToString();
        _SkillCostText_Archer.text = _SkillLevel_Soldier_Archer >= _SKILLLEVEL_SOLDIER_ARCHER_MAX ? "Max" : _SkillLevelUpCost_Archer[_SkillLevel_Soldier_Archer].ToString();
        _SkillCostText_Shield.text = _SkillLevel_Soldier_Shield >= _SKILLLEVEL_SOLDIER_SHIELD_MAX ? "Max" : _SkillLevelUpCost_Shield[_SkillLevel_Soldier_Shield].ToString();
        _SkillCostText_Spear.text = _SkillLevel_Soldier_Spear >= _SKILLLEVEL_SOLDIER_SPEAR_MAX ? "Max" : _SkillLevelUpCost_Spear[_SkillLevel_Soldier_Spear].ToString();
        _SkillCostText_Seven.text = _SkillLevel_Soldier_Seven >= _SKILLLEVEL_SOLDIER_SEVEN_MAX ? "Max" : _SkillLevelUpCost_Seven[_SkillLevel_Soldier_Seven].ToString();

        _SummonCountCostText_Sword.text = _SummonCount_Soldier_Sword>=_SUMMONCOUNT_SOLDIER_SWORD_MAX?"Max":_SummonCountUpCost_Sword[_SummonCount_Soldier_Sword-1].ToString();
        _SummonCountCostText_Archer.text = _SummonCount_Soldier_Archer >= _SUMMONCOUNT_SOLDIER_ARCHER_MAX ? "Max" : _SummonCountUpCost_Archer[_SummonCount_Soldier_Archer - 1].ToString();
        _SummonCountCostText_Shield.text = _SummonCount_Soldier_Shield >= _SUMMONCOUNT_SOLDIER_SHIELD_MAX ? "Max" : _SummonCountUpCost_Shield[_SummonCount_Soldier_Shield - 1].ToString();
        _SummonCountCostText_Spear.text = _SummonCount_Soldier_Spear >= _SUMMONCOUNT_SOLDIER_SPEAR_MAX ? "Max" : _SummonCountUpCost_Spear[_SummonCount_Soldier_Spear - 1].ToString();

        //Hero
        _Skill_LevelText_Hero_Ace.text = _Skill_Level_Soldier_Hero_Ace.ToString();/*+ " / " + _SKILL_LEVEL_SOLDIER_HERO_JACK_MAX.ToString();*/
        _Skill_LevelText_Hero_Jack.text = _Skill_Level_Soldier_Hero_Jack.ToString();/* + " / " + _SKILL_LEVEL_SOLDIER_HERO_QUEEN_MAX.ToString();*/
        _Skill_LevelText_Hero_Queen.text = _Skill_Level_Soldier_Hero_Queen.ToString();/* + " / " + _SKILL_LEVEL_SOLDIER_HERO_KING_MAX.ToString();*/
        _Skill_LevelText_Hero_King.text = _Skill_Level_Soldier_Hero_King.ToString();/* + " / "+_SKILL_LEVEL_SOLDIER_HERO_ACE_MAX.ToString();*/

        if (_Skill_Level_Soldier_Hero_Ace >= _SKILL_LEVEL_SOLDIER_HERO_ACE_MAX)
            _SkillCostHero_Ace.text = "Max";
        else
            _SkillCostHero_Ace.text = MyUnitInfoMng.Data._Ace_LevelPrice[_Skill_Level_Soldier_Hero_Ace - 1].ToString();

        if (_Skill_Level_Soldier_Hero_Jack >= _SKILL_LEVEL_SOLDIER_HERO_JACK_MAX)
            _SkillCostHero_Jack.text = "Max";
        else
            _SkillCostHero_Jack.text = MyUnitInfoMng.Data._Jack_LevelPrice[_Skill_Level_Soldier_Hero_Jack - 1].ToString();

        if (_Skill_Level_Soldier_Hero_Queen >= _SKILL_LEVEL_SOLDIER_HERO_QUEEN_MAX)
            _SkillCostHero_Queen.text = "Max";
        else
            _SkillCostHero_Queen.text = MyUnitInfoMng.Data._Queen_LevelPrice[_Skill_Level_Soldier_Hero_Queen - 1].ToString();

        if (_Skill_Level_Soldier_Hero_King >= _SKILL_LEVEL_SOLDIER_HERO_KING_MAX)
            _SkillCostHero_King.text = "Max";
        else
            _SkillCostHero_King.text = MyUnitInfoMng.Data._King_LevelPrice[_Skill_Level_Soldier_Hero_King - 1].ToString();

        if(_Skill_Level_Soldier_Hero_Ace<_SKILL_LEVEL_SOLDIER_HERO_ACE_MAX)
        {
            _SkillDmgText_Ace.text = ((int)(MyUnitInfoMng.Data._Ace_UpDmg[_Skill_Level_Soldier_Hero_Ace - 1] * 100)) + "% → "+ ((int)(MyUnitInfoMng.Data._Ace_UpDmg[_Skill_Level_Soldier_Hero_Ace] * 100))+"%";
            _SkillAttackSpeedText_Ace.text = ((int)(100 - MyUnitInfoMng.Data._Ace_UpDelay[_Skill_Level_Soldier_Hero_Ace - 1] * 100)) + "% → " + ((int)(100 - MyUnitInfoMng.Data._Ace_UpDelay[_Skill_Level_Soldier_Hero_Ace] * 100)) + "%";
        }
        else
        {
            _SkillDmgText_Ace.text = ((int)(MyUnitInfoMng.Data._Ace_UpDmg[_Skill_Level_Soldier_Hero_Ace - 1] * 100)).ToString() + "%";
            _SkillAttackSpeedText_Ace.text = ((int)(100 - MyUnitInfoMng.Data._Ace_UpDelay[_Skill_Level_Soldier_Hero_Ace - 1] * 100)).ToString() + "%";
        }

        if (_Skill_Level_Soldier_Hero_Jack < _SKILL_LEVEL_SOLDIER_HERO_JACK_MAX)
        {
            _SkillChance_Jack.text = MyUnitInfoMng.Data._Jack_JumpRandom[_Skill_Level_Soldier_Hero_Jack - 1] + "% → " + MyUnitInfoMng.Data._Jack_JumpRandom[_Skill_Level_Soldier_Hero_Jack] + "%";
            _SkillTime_Jack.text = MyUnitInfoMng.Data._Jack_JumpDelay[_Skill_Level_Soldier_Hero_Jack - 1] + "초 → " + MyUnitInfoMng.Data._Jack_JumpDelay[_Skill_Level_Soldier_Hero_Jack] + "초";
        }
        else
        {
            _SkillChance_Jack.text = MyUnitInfoMng.Data._Jack_JumpRandom[_Skill_Level_Soldier_Hero_Jack - 1].ToString() + "%";
            _SkillTime_Jack.text = MyUnitInfoMng.Data._Jack_JumpDelay[_Skill_Level_Soldier_Hero_Jack - 1].ToString() + "초";
        }

        if (_Skill_Level_Soldier_Hero_Queen < _SKILL_LEVEL_SOLDIER_HERO_QUEEN_MAX)
        {
            _SkillChance_Queen.text = MyUnitInfoMng.Data._Queen_SkillChance[_Skill_Level_Soldier_Hero_Queen - 1] + "% → " + MyUnitInfoMng.Data._Queen_SkillChance[_Skill_Level_Soldier_Hero_Queen] + "%";
            _SkillMaxEnemy_Queen.text = MyUnitInfoMng.Data._Queen_SkillCount[_Skill_Level_Soldier_Hero_Queen - 1] + "개 → " + MyUnitInfoMng.Data._Queen_SkillCount[_Skill_Level_Soldier_Hero_Queen] + "개";
        }
        else
        {
            _SkillChance_Queen.text = MyUnitInfoMng.Data._Queen_SkillChance[_Skill_Level_Soldier_Hero_Queen - 1].ToString() + "%";
            _SkillMaxEnemy_Queen.text = MyUnitInfoMng.Data._Queen_SkillCount[_Skill_Level_Soldier_Hero_Queen - 1].ToString() + "개";
        }

        if (_Skill_Level_Soldier_Hero_King < _SKILL_LEVEL_SOLDIER_HERO_KING_MAX)
            _SkillChance_King.text = MyUnitInfoMng.Data._King_DoublePercent[_Skill_Level_Soldier_Hero_King - 1] + "% → " + MyUnitInfoMng.Data._King_DoublePercent[_Skill_Level_Soldier_Hero_King] + "%";
        else
            _SkillChance_King.text = MyUnitInfoMng.Data._King_DoublePercent[_Skill_Level_Soldier_Hero_King - 1].ToString() + "%";

        //Shape
        _ShapeLevelText_Spade.text = _Level_Shape_Spade.ToString();
        _ShapeLevelText_Heart.text = _Level_Shape_Heart.ToString();
        _ShapeLevelText_Diamond.text = _Level_Shape_Diamond.ToString();
        _ShapeLevelText_Club.text = _Level_Shape_Club.ToString();

        if (_Level_Shape_Spade >= _LEVEL_SHAPE_MAX)
            _ShapeCostText_Spade.text = "Max";
        else
            _ShapeCostText_Spade.text = _ShapePrice[_Level_Shape_Spade - 1].ToString();

        if (_Level_Shape_Heart >= _LEVEL_SHAPE_MAX)
            _ShapeCostText_Heart.text = "Max";
        else
            _ShapeCostText_Heart.text = _ShapePrice[_Level_Shape_Heart - 1].ToString();

        if (_Level_Shape_Diamond >= _LEVEL_SHAPE_MAX)
            _ShapeCostText_Diamond.text = "Max";
        else
            _ShapeCostText_Diamond.text = _ShapePrice[_Level_Shape_Diamond - 1].ToString();

        if (_Level_Shape_Club >= _LEVEL_SHAPE_MAX)
            _ShapeCostText_Club.text = "Max";
        else
            _ShapeCostText_Club.text = _ShapePrice[_Level_Shape_Club - 1].ToString();


        if (_Level_Shape_Spade < _LEVEL_SHAPE_MAX)
            _ShapeDataText_Spade.text = SoldierShapeBuffMng.Data._SpadeData[_Level_Shape_Spade - 1] + "% → " + SoldierShapeBuffMng.Data._SpadeData[_Level_Shape_Spade] + "%";
        else
            _ShapeDataText_Spade.text = SoldierShapeBuffMng.Data._SpadeData[_Level_Shape_Spade - 1] + "%";

        if (_Level_Shape_Heart < _LEVEL_SHAPE_MAX)
            _ShapeDataText_Heart.text = "전체 체력의\n"+SoldierShapeBuffMng.Data._HeartData[_Level_Shape_Heart - 1] + "% → " + SoldierShapeBuffMng.Data._HeartData[_Level_Shape_Heart] + "%";
        else
            _ShapeDataText_Heart.text = "전체 체력의\n" + SoldierShapeBuffMng.Data._HeartData[_Level_Shape_Heart - 1] + "%";

        if (_Level_Shape_Diamond < _LEVEL_SHAPE_MAX)
            _ShapeDataText_Diamond.text = SoldierShapeBuffMng.Data._DiamondData[_Level_Shape_Diamond - 1] + " → " + SoldierShapeBuffMng.Data._DiamondData[_Level_Shape_Diamond];
        else
            _ShapeDataText_Diamond.text = SoldierShapeBuffMng.Data._DiamondData[_Level_Shape_Diamond - 1].ToString();

        if (_Level_Shape_Club < _LEVEL_SHAPE_MAX)
            _ShapeDataText_Club.text = SoldierShapeBuffMng.Data._ClubData[_Level_Shape_Club - 1] + "% → " + SoldierShapeBuffMng.Data._ClubData[_Level_Shape_Club] + "%";
        else
            _ShapeDataText_Club.text = SoldierShapeBuffMng.Data._ClubData[_Level_Shape_Club - 1] + "%";



        _MainGroundSoldierMng.SetOnCount();
    }

    public void UpgradeSoldierLevel()
    {
        if (_Coin >= MyUnitInfoMng.Data._SoldierLevelPrice[_SoldierLevel - 1] && _SoldierLevel < _SOLDEIRLEVELMAX)
        {
            SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
            _Coin -= MyUnitInfoMng.Data._SoldierLevelPrice[_SoldierLevel - 1];
            _SoldierLevel += 1;
            SetPlayerPref_Upgrade();
            for (int i = 0; i < 12; i++)
                AchievementMng.Data.AddAchievementCount(i);
        }
    }

    //Sword

    public void UpgradeSoldierSummonCount_Sword()
    {
        if (_SkillCoin >= _SummonCountUpCost_Sword[_SummonCount_Soldier_Sword - 1] && _SummonCount_Soldier_Sword < _SUMMONCOUNT_SOLDIER_SWORD_MAX)
        {
            SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
            _SkillCoin -= _SummonCountUpCost_Sword[_SummonCount_Soldier_Sword - 1];
            _SummonCount_Soldier_Sword += 1;
            SetPlayerPref_Upgrade();
        }
    }
    public void UpgradeSoldierSkill_Sword()
    {
        if (_SkillCoin >= _SkillLevelUpCost_Sword[_SkillLevel_Soldier_Sword] && _SkillLevel_Soldier_Sword < _SKILLLEVEL_SOLDIER_SWORD_MAX)
        {
            SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
            _SkillCoin -= _SkillLevelUpCost_Sword[_SkillLevel_Soldier_Sword];
            _SkillLevel_Soldier_Sword += 1;
            SetPlayerPref_Upgrade();
        }
    }

    //Archer

    public void UpgradeSoldierSummonCount_Archer()
    {
        if (_SkillCoin >= _SummonCountUpCost_Archer[_SummonCount_Soldier_Archer - 1] && _SummonCount_Soldier_Archer < _SUMMONCOUNT_SOLDIER_ARCHER_MAX)
        {
            SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
            _SkillCoin -= _SummonCountUpCost_Archer[_SummonCount_Soldier_Archer - 1];
            _SummonCount_Soldier_Archer += 1;
            SetPlayerPref_Upgrade();
        }
    }
    public void UpgradeSoldierSkill_Archer()
    {
        if (_SkillCoin >= _SkillLevelUpCost_Archer[_SkillLevel_Soldier_Archer] && _SkillLevel_Soldier_Archer < _SKILLLEVEL_SOLDIER_ARCHER_MAX)
        {
            SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
            _SkillCoin -= _SkillLevelUpCost_Archer[_SkillLevel_Soldier_Archer];
            _SkillLevel_Soldier_Archer += 1;
            SetPlayerPref_Upgrade();
        }
    }
    //Shield

    public void UpgradeSoldierSummonCount_Shield()
    {
        if (_SkillCoin >= _SummonCountUpCost_Shield[_SummonCount_Soldier_Shield - 1] && _SummonCount_Soldier_Shield < _SUMMONCOUNT_SOLDIER_SHIELD_MAX)
        {
            SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
            _SkillCoin -= _SummonCountUpCost_Shield[_SummonCount_Soldier_Shield - 1];
            _SummonCount_Soldier_Shield += 1;
            SetPlayerPref_Upgrade();
        }
    }
    public void UpgradeSoldierSkill_Shield()
    {
        if (_SkillCoin >= _SkillLevelUpCost_Shield[_SkillLevel_Soldier_Shield] && _SkillLevel_Soldier_Shield < _SKILLLEVEL_SOLDIER_SHIELD_MAX)
        {
            SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
            _SkillCoin -= _SkillLevelUpCost_Shield[_SkillLevel_Soldier_Shield];
            _SkillLevel_Soldier_Shield += 1;
            SetPlayerPref_Upgrade();
        }
    }
    //Spear

    public void UpgradeSoldierSummonCount_Spear()
    {
        if (_SkillCoin >= _SummonCountUpCost_Spear[_SummonCount_Soldier_Spear - 1] && _SummonCount_Soldier_Spear < _SUMMONCOUNT_SOLDIER_SPEAR_MAX)
        {
            SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
            _SkillCoin -= _SummonCountUpCost_Spear[_SummonCount_Soldier_Spear - 1];
            _SummonCount_Soldier_Spear += 1;
            SetPlayerPref_Upgrade();
        }
    }
    public void UpgradeSoldierSkill_Spear()
    {
        if (_SkillCoin >= _SkillLevelUpCost_Spear[_SkillLevel_Soldier_Spear] && _SkillLevel_Soldier_Spear < _SKILLLEVEL_SOLDIER_SPEAR_MAX)
        {
            SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
            _SkillCoin -= _SkillLevelUpCost_Spear[_SkillLevel_Soldier_Spear];
            _SkillLevel_Soldier_Spear += 1;
            SetPlayerPref_Upgrade();
        }
    }
    //Seven
    public void UpgradeSoldierSkill_Seven()
    {
        if (_SkillCoin >= _SkillLevelUpCost_Seven[_SkillLevel_Soldier_Seven] && _SkillLevel_Soldier_Seven < _SKILLLEVEL_SOLDIER_SEVEN_MAX)
        {
            SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
            _SkillCoin -= _SkillLevelUpCost_Seven[_SkillLevel_Soldier_Seven];
            _SkillLevel_Soldier_Seven += 1;
            SetPlayerPref_Upgrade();
        }
    }



    //<===Hero===>


    //Ace
    public void UpgradeSoldierLevel_Hero_Ace()
    {
        if (_HeroUpgradeCoin >= MyUnitInfoMng.Data._Ace_LevelPrice[_Skill_Level_Soldier_Hero_Ace - 1] && _Skill_Level_Soldier_Hero_Ace < _SKILL_LEVEL_SOLDIER_HERO_ACE_MAX)
        {
            SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
            _HeroUpgradeCoin -= MyUnitInfoMng.Data._Ace_LevelPrice[_Skill_Level_Soldier_Hero_Ace - 1];
            _Skill_Level_Soldier_Hero_Ace += 1;
            SetPlayerPref_Upgrade();
        }
    }
    //Jack
    public void UpgradeSoldierLevel_Hero_Jack()
    {
        if (_HeroUpgradeCoin >= MyUnitInfoMng.Data._Jack_LevelPrice[_Skill_Level_Soldier_Hero_Jack - 1] && _Skill_Level_Soldier_Hero_Jack < _SKILL_LEVEL_SOLDIER_HERO_JACK_MAX)
        {
            SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
            _HeroUpgradeCoin -= MyUnitInfoMng.Data._Jack_LevelPrice[_Skill_Level_Soldier_Hero_Jack - 1];
            _Skill_Level_Soldier_Hero_Jack += 1;
            SetPlayerPref_Upgrade();
        }
    }
    //Queen
    public void UpgradeSoldierLevel_Hero_Queen()
    {
        if (_HeroUpgradeCoin >= MyUnitInfoMng.Data._Queen_LevelPrice[_Skill_Level_Soldier_Hero_Queen - 1] && _Skill_Level_Soldier_Hero_Queen < _SKILL_LEVEL_SOLDIER_HERO_QUEEN_MAX)
        {
            SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
            _HeroUpgradeCoin -= MyUnitInfoMng.Data._Queen_LevelPrice[_Skill_Level_Soldier_Hero_Queen - 1];
            _Skill_Level_Soldier_Hero_Queen += 1;
            SetPlayerPref_Upgrade();
        }
    }
    //King
    public void UpgradeSoldierLevel_Hero_King()
    {
        if (_HeroUpgradeCoin >= MyUnitInfoMng.Data._King_LevelPrice[_Skill_Level_Soldier_Hero_King - 1] && _Skill_Level_Soldier_Hero_King < _SKILL_LEVEL_SOLDIER_HERO_KING_MAX)
        {
            SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
            _HeroUpgradeCoin -= MyUnitInfoMng.Data._King_LevelPrice[_Skill_Level_Soldier_Hero_King - 1];
            _Skill_Level_Soldier_Hero_King += 1;
            SetPlayerPref_Upgrade();
        }
    }


    //<===Shape===>
    public void UpgradeShapeLevel(int num)
    {
        if (_ShapeUpgradeCoin > 0)
        {
            if (num == 0 && _Level_Shape_Spade < _LEVEL_SHAPE_MAX && _ShapePrice[_Level_Shape_Spade-1]<=_ShapeUpgradeCoin)
            {
                SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
                _ShapeUpgradeCoin -= _ShapePrice[_Level_Shape_Spade - 1];
                _Level_Shape_Spade++;
                AchievementMng.Data.AddAchievementCount(16);
            }
            else if (num == 1 && _Level_Shape_Heart < _LEVEL_SHAPE_MAX && _ShapePrice[_Level_Shape_Heart - 1] <= _ShapeUpgradeCoin)
            {
                SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
                _ShapeUpgradeCoin -= _ShapePrice[_Level_Shape_Heart - 1];
                _Level_Shape_Heart++;
                AchievementMng.Data.AddAchievementCount(17);
            }
            else if (num == 2 && _Level_Shape_Diamond < _LEVEL_SHAPE_MAX && _ShapePrice[_Level_Shape_Diamond - 1] <= _ShapeUpgradeCoin)
            {
                SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
                _ShapeUpgradeCoin -= _ShapePrice[_Level_Shape_Diamond - 1];
                _Level_Shape_Diamond++;
                AchievementMng.Data.AddAchievementCount(18);
            }
            else if (num == 3 && _Level_Shape_Club < _LEVEL_SHAPE_MAX && _ShapePrice[_Level_Shape_Club - 1] <= _ShapeUpgradeCoin)
            {
                SoundMng.Data.PlayEffectSound(EffectSound.Upgrade_Click, 0);
                _ShapeUpgradeCoin -= _ShapePrice[_Level_Shape_Club - 1];
                _Level_Shape_Club++;
                AchievementMng.Data.AddAchievementCount(19);
            }
            SetPlayerPref_Upgrade();
        }
    }

    void SetPlayerPref_Upgrade()
    {
        PlayerPrefs.SetInt("coin", _Coin);
        PlayerPrefs.SetInt("shape_upgradecoin", _ShapeUpgradeCoin);
        PlayerPrefs.SetInt("hero_upgradecoin", _HeroUpgradeCoin);
        PlayerPrefs.SetInt("skillcoin", _SkillCoin);

        PlayerPrefs.SetInt("soldierlevel", _SoldierLevel);
        PlayerPrefs.SetInt("summoncount_sword", _SummonCount_Soldier_Sword);
        PlayerPrefs.SetInt("summoncount_archer", _SummonCount_Soldier_Archer);
        PlayerPrefs.SetInt("summoncount_shield", _SummonCount_Soldier_Shield);
        PlayerPrefs.SetInt("summoncount_spear", _SummonCount_Soldier_Spear);

        PlayerPrefs.SetInt("skill_sword", _SkillLevel_Soldier_Sword);
        PlayerPrefs.SetInt("skill_archer", _SkillLevel_Soldier_Archer);
        PlayerPrefs.SetInt("skill_shield", _SkillLevel_Soldier_Shield);
        PlayerPrefs.SetInt("skill_spear", _SkillLevel_Soldier_Spear);
        PlayerPrefs.SetInt("skill_seven", _SkillLevel_Soldier_Seven);

        PlayerPrefs.SetInt("skilllevel_hero_king", _Skill_Level_Soldier_Hero_King);
        PlayerPrefs.SetInt("skilllevel_hero_ace", _Skill_Level_Soldier_Hero_Ace);
        PlayerPrefs.SetInt("skilllevel_hero_jack", _Skill_Level_Soldier_Hero_Jack);
        PlayerPrefs.SetInt("skilllevel_hero_queen", _Skill_Level_Soldier_Hero_Queen);

        PlayerPrefs.SetInt("shapelevel_spade", _Level_Shape_Spade);
        PlayerPrefs.SetInt("shapelevel_heart", _Level_Shape_Heart);
        PlayerPrefs.SetInt("shapelevel_diamond", _Level_Shape_Diamond);
        PlayerPrefs.SetInt("shapelevel_club", _Level_Shape_Club);
        
        TextUpdate();
    }

    public void Temp_GetCoin()
    {
        _Coin += 100000000;
        _HeroUpgradeCoin += 1000;
        _ShapeUpgradeCoin += 1000;
        _SkillCoin += 1000;
        PlayerPrefs.SetInt("coin", _Coin);
        PlayerPrefs.SetInt("shape_upgradecoin", _ShapeUpgradeCoin);
        PlayerPrefs.SetInt("hero_upgradecoin", _HeroUpgradeCoin);
        PlayerPrefs.SetInt("skillcoin", _SkillCoin);
        TextUpdate();
    }
    public void Temp_PlayerPrefReset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefsStart();
        SetPlayerPref_Upgrade();
    }

}
