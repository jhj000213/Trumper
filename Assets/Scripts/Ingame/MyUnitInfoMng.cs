using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUnitInfoMng : MonoBehaviour
{
    private static MyUnitInfoMng instance = null;
    public static MyUnitInfoMng Data
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(MyUnitInfoMng)) as MyUnitInfoMng;
                if (instance == null)
                {
                    Debug.Log("no instance");
                }
            }
            return instance;
        }
    }

    public List<int> _SoldierLevelPrice = new List<int>();

    //Sword
    public List<float> _Sword_HP = new List<float>();
    public List<float> _Sword_Dmg = new List<float>();
    public List<int> _Sword_SkillChance = new List<int>();
    public List<float> _Sword_SkillTime = new List<float>();
    //Archer
    public List<float> _Archer_HP = new List<float>();
    public List<float> _Archer_Dmg = new List<float>();
    public List<int> _Archer_SkillChance = new List<int>();
    public List<float> _Archer_SkillDmg = new List<float>();
    //Shield
    public List<float> _Shield_HP = new List<float>();
    public List<float> _Shield_Dmg = new List<float>();
    public List<float> _Shield_SkillSpeed = new List<float>();
    //Spear
    public List<float> _Spear_HP = new List<float>();
    public List<float> _Spear_Dmg = new List<float>();
    public List<int> _Spear_SkillChance = new List<int>();
    public List<float> _Spear_SkillDmg = new List<float>();
    //Seven
    public List<float> _Seven_HP = new List<float>();
    public List<float> _Seven_Dmg = new List<float>();
    //Hero_Ace
    public List<float> _Ace_HP = new List<float>();
    public List<float> _Ace_Dmg = new List<float>();
    public List<int> _Ace_LevelPrice = new List<int>();
    public List<float> _Ace_UpDmg = new List<float>();
    public List<float> _Ace_UpDelay = new List<float>();
    //Hero_Jack
    public List<float> _Jack_HP = new List<float>();
    public List<float> _Jack_Dmg = new List<float>();
    public List<int> _Jack_LevelPrice = new List<int>();
    public List<int> _Jack_JumpRandom = new List<int>();
    public List<float> _Jack_JumpDelay = new List<float>();
    //Hero_Queen
    public List<float> _Queen_HP = new List<float>();
    public List<float> _Queen_Dmg = new List<float>();
    public List<int> _Queen_LevelPrice = new List<int>();
    public List<int> _Queen_SkillChance = new List<int>();
    public List<int> _Queen_SkillCount = new List<int>();
    //public List<float> _Queen_SkillDelay = new List<float>();
    //Hero_King
    public List<float> _King_HP = new List<float>();
    public List<float> _King_Dmg = new List<float>();
    public List<int> _King_LevelPrice = new List<int>();
    public List<int> _King_DoublePercent = new List<int>();




    //shape
    private void Awake()
    {
        MyUnitInit();
    }

    void MyUnitInit()
    {
        Soldier_LevelPrice();
        Soldier_Sword();
        Soldier_Archer();
        Soldier_Shield();
        Soldier_Spear();
        Soldier_Seven();
        Hero_Ace();
        Hero_Jack();
        Hero_Queen();
        Hero_King();
    }

    void Soldier_LevelPrice()
    {
        List<Dictionary<string, object>> unit = CSVReader.Read("SoldierLevelUpPrice");
        int max = (int)unit[0]["max"];
        for (int i = 0; i < max; i++)
        {
            _SoldierLevelPrice.Add((int)unit[i]["levelprice"]);
        }
        UpgradeMng.Data._SOLDEIRLEVELMAX = max;
    }

    void Soldier_Sword()
    {
        List<Dictionary<string, object>> unit = CSVReader.Read("Unit_Sword");
        int max = (int)unit[0]["max"];
        int countmax = (int)unit[0]["countmax"];

        for(int i=0;i<max;i++)
        {
            _Sword_HP.Add((float)unit[i]["hp"]);
            _Sword_Dmg.Add((float)unit[i]["dmg"]);
            _Sword_SkillChance.Add((int)unit[i]["skillchance"]);
            _Sword_SkillTime.Add((float)unit[i]["skilltime"]);

            UpgradeMng.Data._SummonCountUpCost_Sword.Add((int)unit[i]["countcost"]);
            UpgradeMng.Data._SkillLevelUpCost_Sword.Add((int)unit[i]["skillcost"]);
        }
        UpgradeMng.Data._SKILLLEVEL_SOLDIER_SWORD_MAX = (int)unit[0]["skillmax"];
        UpgradeMng.Data._SUMMONCOUNT_SOLDIER_SWORD_MAX = countmax;
    }
    void Soldier_Archer()
    {
        List<Dictionary<string, object>> unit = CSVReader.Read("Unit_Archer");
        int max = (int)unit[0]["max"];
        int countmax = (int)unit[0]["countmax"];

        for (int i = 0; i < max; i++)
        {
            _Archer_HP.Add((float)unit[i]["hp"]);
            _Archer_Dmg.Add((float)unit[i]["dmg"]);
            _Archer_SkillChance.Add((int)unit[i]["skillchance"]);
            _Archer_SkillDmg.Add((float)unit[i]["skilldmg"]);
            UpgradeMng.Data._SummonCountUpCost_Archer.Add((int)unit[i]["countcost"]);
            UpgradeMng.Data._SkillLevelUpCost_Archer.Add((int)unit[i]["skillcost"]);
        }
        UpgradeMng.Data._SKILLLEVEL_SOLDIER_ARCHER_MAX = (int)unit[0]["skillmax"];
        UpgradeMng.Data._SUMMONCOUNT_SOLDIER_ARCHER_MAX = countmax;
    }
    void Soldier_Shield()
    {
        List<Dictionary<string, object>> unit = CSVReader.Read("Unit_Shield");
        int max = (int)unit[0]["max"];
        int countmax = (int)unit[0]["countmax"];

        for (int i = 0; i < max; i++)
        {
            _Shield_HP.Add((float)unit[i]["hp"]);
            _Shield_Dmg.Add((float)unit[i]["dmg"]);
            _Shield_SkillSpeed.Add((float)unit[i]["skillspeed"]);
            UpgradeMng.Data._SummonCountUpCost_Shield.Add((int)unit[i]["countcost"]);
            UpgradeMng.Data._SkillLevelUpCost_Shield.Add((int)unit[i]["skillcost"]);
        }
        UpgradeMng.Data._SKILLLEVEL_SOLDIER_SHIELD_MAX = (int)unit[0]["skillmax"];
        UpgradeMng.Data._SUMMONCOUNT_SOLDIER_SHIELD_MAX = countmax;
    }
    void Soldier_Spear()
    {
        List<Dictionary<string, object>> unit = CSVReader.Read("Unit_Spear");
        int max = (int)unit[0]["max"];
        int countmax = (int)unit[0]["countmax"];

        for (int i = 0; i < max; i++)
        {
            _Spear_HP.Add((float)unit[i]["hp"]);
            _Spear_Dmg.Add((float)unit[i]["dmg"]);
            _Spear_SkillChance.Add((int)unit[i]["skillchance"]);
            _Spear_SkillDmg.Add((float)unit[i]["skilldmg"]);
            UpgradeMng.Data._SummonCountUpCost_Spear.Add((int)unit[i]["countcost"]);
            UpgradeMng.Data._SkillLevelUpCost_Spear.Add((int)unit[i]["skillcost"]);
        }
        UpgradeMng.Data._SKILLLEVEL_SOLDIER_SPEAR_MAX = (int)unit[0]["skillmax"];
        UpgradeMng.Data._SUMMONCOUNT_SOLDIER_SPEAR_MAX = countmax;
    }
    void Soldier_Seven()
    {
        List<Dictionary<string, object>> unit = CSVReader.Read("Unit_Seven");
        int max = (int)unit[0]["max"];

        for (int i = 0; i < max; i++)
        {
            _Seven_HP.Add((float)unit[i]["hp"]);
            _Seven_Dmg.Add((float)unit[i]["dmg"]);
            UpgradeMng.Data._SkillLevelUpCost_Seven.Add((int)unit[i]["skillcost"]);
        }
        UpgradeMng.Data._SKILLLEVEL_SOLDIER_SEVEN_MAX = (int)unit[0]["skillmax"];
    }

    void Hero_Ace()
    {
        List<Dictionary<string, object>> unit = CSVReader.Read("Unit_Ace");
        int max = (int)unit[0]["max"];
        int skillmax = (int)unit[0]["skillmax"];

        for (int i = 0; i < max; i++)
        {
            _Ace_HP.Add((float)unit[i]["hp"]);
            _Ace_Dmg.Add((float)unit[i]["dmg"]);
            _Ace_LevelPrice.Add((int)unit[i]["levelprice"]);
            _Ace_UpDmg.Add((float)unit[i]["acedmg"]);
            _Ace_UpDelay.Add((float)unit[i]["acedelay"]);
        }
        UpgradeMng.Data._SKILL_LEVEL_SOLDIER_HERO_ACE_MAX = skillmax;
    }
    void Hero_Jack()
    {
        List<Dictionary<string, object>> unit = CSVReader.Read("Unit_Jack");
        int max = (int)unit[0]["max"];
        int skillmax = (int)unit[0]["skillmax"];

        for (int i = 0; i < max; i++)
        {
            _Jack_HP.Add((float)unit[i]["hp"]);
            _Jack_Dmg.Add((float)unit[i]["dmg"]);
            _Jack_LevelPrice.Add((int)unit[i]["levelprice"]);
            _Jack_JumpRandom.Add((int)unit[i]["jumprand"]);
            _Jack_JumpDelay.Add((int)unit[i]["jumpdelay"]);
        }
        UpgradeMng.Data._SKILL_LEVEL_SOLDIER_HERO_JACK_MAX = skillmax;
    }
    void Hero_Queen()
    {
        List<Dictionary<string, object>> unit = CSVReader.Read("Unit_Queen");
        int max = (int)unit[0]["max"];
        int skillmax = (int)unit[0]["skillmax"];

        for (int i = 0; i < max; i++)
        {
            _Queen_HP.Add((float)unit[i]["hp"]);
            _Queen_Dmg.Add((float)unit[i]["dmg"]);
            _Queen_LevelPrice.Add((int)unit[i]["levelprice"]);
            _Queen_SkillChance.Add((int)unit[i]["skillchance"]);
            _Queen_SkillCount.Add((int)unit[i]["skillcount"]);
        }
        UpgradeMng.Data._SKILL_LEVEL_SOLDIER_HERO_QUEEN_MAX = skillmax;
    }
    void Hero_King()
    {
        List<Dictionary<string, object>> unit = CSVReader.Read("Unit_King");
        int max = (int)unit[0]["max"];
        int skillmax = (int)unit[0]["skillmax"];

        for (int i = 0; i < max; i++)
        {
            _King_HP.Add((float)unit[i]["hp"]);
            _King_Dmg.Add((float)unit[i]["dmg"]);
            _King_LevelPrice.Add((int)unit[i]["levelprice"]);
            _King_DoublePercent.Add((int)unit[i]["kingdouble"]);
        }
        UpgradeMng.Data._SKILL_LEVEL_SOLDIER_HERO_KING_MAX = skillmax;
    }
}
