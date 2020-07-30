using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ObjectPoolingMng : MonoBehaviour
{

    private static ObjectPoolingMng instance = null;
    public static ObjectPoolingMng Data
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(ObjectPoolingMng)) as ObjectPoolingMng;
                if (instance == null)
                {
                    Debug.Log("no instance");
                }
            }
            return instance;
        }
    }


    [SerializeField] GameObject _MySoldier_Archer_Arrow;
    [SerializeField] GameObject _MySoldier_Archer_Arrow_Parent;
    public List<MySoldier_Archer_Arrow> _MySoldier_Archer_ArrowsList = new List<MySoldier_Archer_Arrow>();
    int _MySoldier_Archer_Arrow_NowCount = 0;
    int _MySoldier_Archer_Arrow_MaxCount;

    [SerializeField] GameObject _MySoldier_Hero_Queen_Magic;
    [SerializeField] GameObject _MySoldier_Hero_Queen_Magic_Parent;
    public List<MySoldier_Hero_Queen_Magic> _MySoldier_Hero_Queen_MagicsList = new List<MySoldier_Hero_Queen_Magic>();
    int _MySoldier_Hero_Queen_Magic_NowCount = 0;
    int _MySoldier_Hero_Queen_Magic_MaxCount;

    [SerializeField] GameObject _MySoldier_Seven_Magic;
    [SerializeField] GameObject _MySoldier_Seven_Magic_Parent;
    public List<MySoldier_Seven_Magic> _MySoldier_Seven_MagicsList = new List<MySoldier_Seven_Magic>();
    int _MySoldier_Seven_Magic_NowCount = 0;
    int _MySoldier_Seven_Magic_MaxCount;

    [SerializeField] GameObject _EnemySoldier_Rifle_Bullet;
    [SerializeField] GameObject _EnemySoldier_Rifle_Bullet_Parent;
    public List<EnemySoldier_Rifle_Bullet> _EnemySoldier_Rifle_BulletsList = new List<EnemySoldier_Rifle_Bullet>();
    int _EnemySoldier_Rifle_Bullet_NowCount = 0;
    int _EnemySoldier_Rifle_Bullet_MaxCount;


    [SerializeField] GameObject _DamageFontObj;
    [SerializeField] GameObject _DamageFontObj_Parent;
    public List<DamageFont> _DamageFontObjList = new List<DamageFont>();
    int _DamageFont_NowCount = 0;
    int _DamageFont_MaxCount;


    [SerializeField] SpriteAtlas _Boss5_NiddleAtlas;
    [SerializeField] GameObject _Boss_5_Niddle;
    [SerializeField] GameObject _Boss_5_Niddle_Parent;
    public List<Boss_5_Niddle> _Boss_5_NiddleList = new List<Boss_5_Niddle>();
    int _Boss_5_Niddle_NowCount = 0;
    int _Boss_5_Niddle_MaxCount;







    private void Start()
    {
        _MySoldier_Archer_Arrow_MaxCount = 100;
        _MySoldier_Hero_Queen_Magic_MaxCount = 50;
        _MySoldier_Seven_Magic_MaxCount = 50;
        _EnemySoldier_Rifle_Bullet_MaxCount = 100;
        _DamageFont_MaxCount = 250;
        _Boss_5_Niddle_MaxCount = 80;


        CreateObjs();
    }

    void CreateObjs()
    {
        for (int i = 0; i < _MySoldier_Archer_Arrow_MaxCount; i++)
        {
            GameObject obj = Instantiate(_MySoldier_Archer_Arrow, _MySoldier_Archer_Arrow_Parent.transform);
            _MySoldier_Archer_ArrowsList.Add(obj.GetComponent<MySoldier_Archer_Arrow>());
            obj.SetActive(false);
        }
        for (int i = 0; i < _MySoldier_Hero_Queen_Magic_MaxCount; i++)
        {
            GameObject obj = Instantiate(_MySoldier_Hero_Queen_Magic, _MySoldier_Hero_Queen_Magic_Parent.transform);
            _MySoldier_Hero_Queen_MagicsList.Add(obj.GetComponent<MySoldier_Hero_Queen_Magic>());
            obj.SetActive(false);
        }
        for (int i = 0; i < _MySoldier_Seven_Magic_MaxCount; i++)
        {
            GameObject obj = Instantiate(_MySoldier_Seven_Magic, _MySoldier_Seven_Magic_Parent.transform);
            _MySoldier_Seven_MagicsList.Add(obj.GetComponent<MySoldier_Seven_Magic>());
            obj.SetActive(false);
        }
        for (int i = 0; i < _EnemySoldier_Rifle_Bullet_MaxCount; i++)
        {
            GameObject obj = Instantiate(_EnemySoldier_Rifle_Bullet, _EnemySoldier_Rifle_Bullet_Parent.transform);
            _EnemySoldier_Rifle_BulletsList.Add(obj.GetComponent<EnemySoldier_Rifle_Bullet>());
            obj.SetActive(false);
        }

        for (int i = 0; i < _DamageFont_MaxCount; i++)
        {
            GameObject obj = Instantiate(_DamageFontObj, _DamageFontObj_Parent.transform);
            _DamageFontObjList.Add(obj.GetComponent<DamageFont>());
            obj.SetActive(false);
        }
        for (int i = 0; i < _Boss_5_Niddle_MaxCount; i++)
        {
            GameObject obj = Instantiate(_Boss_5_Niddle, _Boss_5_Niddle_Parent.transform);
            obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _Boss5_NiddleAtlas.GetSprite("niddle" + Random.Range(0, 3));
            _Boss_5_NiddleList.Add(obj.GetComponent< Boss_5_Niddle>());
            obj.SetActive(false);
        }
    }

    public int UseMySoldier_Archer_Arrow()
    {
        int t = _MySoldier_Archer_Arrow_NowCount++;
        if (_MySoldier_Archer_Arrow_NowCount >= _MySoldier_Archer_Arrow_MaxCount)
            _MySoldier_Archer_Arrow_NowCount = 0;
        return t;
    }
    
    public int UseMySoldier_Hero_Queen_Magic()
    {
        int t = _MySoldier_Hero_Queen_Magic_NowCount++;
        if (_MySoldier_Hero_Queen_Magic_NowCount >= _MySoldier_Hero_Queen_Magic_MaxCount)
            _MySoldier_Hero_Queen_Magic_NowCount = 0;
        return t;
    }
    public int UseMySoldier_Seven_Magic()
    {
        int t = _MySoldier_Seven_Magic_NowCount++;
        if (_MySoldier_Seven_Magic_NowCount >= _MySoldier_Seven_Magic_MaxCount)
            _MySoldier_Seven_Magic_NowCount = 0;
        return t;
    }

    public int UseEnemySoldier_Rifle_Bullet()
    {
        int t = _EnemySoldier_Rifle_Bullet_NowCount++;
        if (_EnemySoldier_Rifle_Bullet_NowCount >= _EnemySoldier_Rifle_Bullet_MaxCount)
            _EnemySoldier_Rifle_Bullet_NowCount = 0;
        return t;
    }

    public int UseDamageFont()
    {
        int t = _DamageFont_NowCount++;
        if (_DamageFont_NowCount >= _DamageFont_MaxCount)
            _DamageFont_NowCount = 0;
        return t;
    }

    public int UseBoss_5_Niddle()
    {
        int t = _Boss_5_Niddle_NowCount++;
        if (_Boss_5_Niddle_NowCount >= _Boss_5_Niddle_MaxCount)
            _Boss_5_Niddle_NowCount = 0;
        return t;
    }
    


    public void EndGame()
    {
        for (int i = 0; i < _MySoldier_Archer_Arrow_MaxCount; i++)
            _MySoldier_Archer_ArrowsList[i].gameObject.SetActive(false);

        for (int i = 0; i < _MySoldier_Hero_Queen_Magic_MaxCount; i++)
            _MySoldier_Hero_Queen_MagicsList[i].gameObject.SetActive(false);

        for (int i = 0; i < _MySoldier_Seven_Magic_MaxCount; i++)
            _MySoldier_Seven_MagicsList[i].gameObject.SetActive(false);

        for (int i = 0; i < _MySoldier_Archer_Arrow_MaxCount; i++)
            _EnemySoldier_Rifle_BulletsList[i].gameObject.SetActive(false);

        for (int i = 0; i < _DamageFont_MaxCount; i++)
            _DamageFontObjList[i].gameObject.SetActive(false);

        for (int i = 0; i < _Boss_5_Niddle_MaxCount; i++)
            _Boss_5_NiddleList[i].gameObject.SetActive(false);
    }
}
