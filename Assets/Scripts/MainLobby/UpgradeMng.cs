using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class UpgradeMng : MonoBehaviour
{
    private static UpgradeMng instance = null;
    public static UpgradeMng Data
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(UpgradeMng)) as UpgradeMng;
                if (instance == null)
                {
                    Debug.Log("no instance");
                }
            }
            return instance;
        }
    }

    [SerializeField] MainGroundSoldierMng _MainGroundSoldierMng;

    [SerializeField] Text _CoinText;
    [SerializeField] Text _HeroUpgradeCoinText;
    [SerializeField] Text _ShapeUpgradeCoinText;
    [SerializeField] Text _SoldierSkillCoinText;

    int _Coin;
    int _ShapeUpgradeCoin;
    int _HeroUpgradeCoin;
    int _SkillCoin;
    
    [SerializeField] GameObject[] _SoldierUpgradeTabs = new GameObject[5];//검,궁수,창,방패,7
    [SerializeField] GameObject[] _UpgradeTabsChoices = new GameObject[5];
    [SerializeField] Text _SoldierLevelPriceText;
    [SerializeField] Text _SoldierLevelText;
    public int _SoldierLevel;
    public int _SOLDEIRLEVELMAX;

    /// == Soldier ==
    readonly int _SkillLevelUpCost = 1;
    readonly int _SummonCountUpCost = 2;

    //<--Sword-->
    [SerializeField] Text _SkillLevelText_Sword;
    [SerializeField] Text _SummonCountText_Sword;
    [SerializeField] Text _SkillCostText_Sword;
    [SerializeField] Text _SummonCountCostText_Sword;
    public List<int> _SummonCountUpCost_Sword = new List<int>();
    public List<int> _SkillLevelUpCost_Sword = new List<int>();
    public int _SummonCount_Soldier_Sword;
    public int _SUMMONCOUNT_SOLDIER_SWORD_MAX;
    public int _SkillLevel_Soldier_Sword;
    public int _SKILLLEVEL_SOLDIER_SWORD_MAX;

    //<--Archer-->
    [SerializeField] Text _SkillLevelText_Archer;
    [SerializeField] Text _SummonCountText_Archer;
    [SerializeField] Text _SkillCostText_Archer;
    [SerializeField] Text _SummonCountCostText_Archer;
    public List<int> _SummonCountUpCost_Archer = new List<int>();
    public List<int> _SkillLevelUpCost_Archer = new List<int>();
    public int _SummonCount_Soldier_Archer;
    public int _SUMMONCOUNT_SOLDIER_ARCHER_MAX;
    public int _SkillLevel_Soldier_Archer;
    public int _SKILLLEVEL_SOLDIER_ARCHER_MAX;

    //<--Shield-->
    [SerializeField] Text _SkillLevelText_Shield;
    [SerializeField] Text _SummonCountText_Shield;
    [SerializeField] Text _SkillCostText_Shield;
    [SerializeField] Text _SummonCountCostText_Shield;
    public List<int> _SummonCountUpCost_Shield = new List<int>();
    public List<int> _SkillLevelUpCost_Shield = new List<int>();
    public int _SummonCount_Soldier_Shield;
    public int _SUMMONCOUNT_SOLDIER_SHIELD_MAX;
    public int _SkillLevel_Soldier_Shield;
    public int _SKILLLEVEL_SOLDIER_SHIELD_MAX;

    //<--Spear-->
    [SerializeField] Text _SkillLevelText_Spear;
    [SerializeField] Text _SummonCountText_Spear;
    [SerializeField] Text _SkillCostText_Spear;
    [SerializeField] Text _SummonCountCostText_Spear;
    public List<int> _SummonCountUpCost_Spear = new List<int>();
    public List<int> _SkillLevelUpCost_Spear = new List<int>();
    public int _SummonCount_Soldier_Spear;
    public int _SUMMONCOUNT_SOLDIER_SPEAR_MAX;
    public int _SkillLevel_Soldier_Spear;
    public int _SKILLLEVEL_SOLDIER_SPEAR_MAX;


    //<--Seven-->
    [SerializeField] Text _SkillLevelText_Seven;
    [SerializeField] Text _SkillCostText_Seven;
    public List<int> _SkillLevelUpCost_Seven = new List<int>();
    public int _SkillLevel_Soldier_Seven;
    public int _SKILLLEVEL_SOLDIER_SEVEN_MAX;


    //<--Hero-->
    [SerializeField] GameObject[] _HeroUpgradeTabs = new GameObject[4];
    public GameObject[] _CheckNowHero = new GameObject[5];
    [SerializeField] GameObject[] _HeroTabsChoice = new GameObject[4];

    //Ace
    [SerializeField] Text _SkillCostHero_Ace;
    [SerializeField] Text _Skill_LevelText_Hero_Ace;
    [SerializeField] Text _SkillDmgText_Ace;
    [SerializeField] Text _SkillAttackSpeedText_Ace;
    public int _Skill_Level_Soldier_Hero_Ace;
    public int _SKILL_LEVEL_SOLDIER_HERO_ACE_MAX;

    //Jack
    [SerializeField] Text _SkillCostHero_Jack;
    [SerializeField] Text _Skill_LevelText_Hero_Jack;
    [SerializeField] Text _SkillChance_Jack;
    [SerializeField] Text _SkillTime_Jack;
    public int _Skill_Level_Soldier_Hero_Jack;
    public int _SKILL_LEVEL_SOLDIER_HERO_JACK_MAX;

    //Queen
    [SerializeField] Text _SkillCostHero_Queen;
    [SerializeField] Text _Skill_LevelText_Hero_Queen;
    [SerializeField] Text _SkillChance_Queen;
    [SerializeField] Text _SkillMaxEnemy_Queen;
    public int _Skill_Level_Soldier_Hero_Queen;
    public int _SKILL_LEVEL_SOLDIER_HERO_QUEEN_MAX;

    //King
    [SerializeField] Text _SkillCostHero_King;
    [SerializeField] Text _Skill_LevelText_Hero_King;
    [SerializeField] Text _SkillChance_King;
    public int _Skill_Level_Soldier_Hero_King;
    public int _SKILL_LEVEL_SOLDIER_HERO_KING_MAX;



    //<--Shape-->
    public int _LEVEL_SHAPE_MAX;
    public List<int> _ShapePrice = new List<int>();
    [SerializeField] GameObject[] _ShapeUpgradeTabs = new GameObject[4];
    [SerializeField] GameObject[] _ShapeTabsChoice = new GameObject[4];

    //Spade
    [SerializeField] Text _ShapeCostText_Spade;
    [SerializeField] Text _ShapeLevelText_Spade;
    [SerializeField] Text _ShapeDataText_Spade;
    public int _Level_Shape_Spade;

    //Heart
    [SerializeField] Text _ShapeCostText_Heart;
    [SerializeField] Text _ShapeLevelText_Heart;
    [SerializeField] Text _ShapeDataText_Heart;
    public int _Level_Shape_Heart;

    //Diamond
    [SerializeField] Text _ShapeCostText_Diamond;
    [SerializeField] Text _ShapeLevelText_Diamond;
    [SerializeField] Text _ShapeDataText_Diamond;
    public int _Level_Shape_Diamond;

    //Club
    [SerializeField] Text _ShapeCostText_Club;
    [SerializeField] Text _ShapeLevelText_Club;
    [SerializeField] Text _ShapeDataText_Club;
    public int _Level_Shape_Club;

}
