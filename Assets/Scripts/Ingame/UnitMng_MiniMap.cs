using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class UnitMng : MonoBehaviour
{

    //MiniMap
    const float _MINIMAP_MINUS_XPOS = 9.15f;
    const float _MINIMAP_WIDTH = 56.87f;
    const float _MINIMAP_PIXELWIDTH = 600;

    const float _MINIMAP_MINY = -43.5f;
    const float _MINIMAP_MAXY = -23.5f;

    const float _UnitSummonPosY_Min = 1.8f;
    const float _UnitSummonPosY_Max = 3.3f;

    [SerializeField] GameObject _MiniMap_Obj_Parent;

    [SerializeField] GameObject _MiniMap_MySoldier;
    int _MiniMap_MySoldier_MaxCount;

    [SerializeField] GameObject _MiniMap_MyHero;
    int _MiniMap_MyHero_MaxCount;

    [SerializeField] GameObject _MiniMap_EnemySoldier;
    int _MiniMap_EnemySoldier_MaxCount;

    [SerializeField] GameObject _MiniMap_Boss;
    int _MiniMap_Boss_MaxCount;

    List<GameObject> _Mini_MyUnitList = new List<GameObject>();
    List<GameObject> _Mini_MyHeroUnitList = new List<GameObject>();
    List<GameObject> _Mini_EnemyUnitList = new List<GameObject>();
    List<GameObject> _Mini_BossUnitList = new List<GameObject>();


    private void Start()
    {
        _MiniMap_MySoldier_MaxCount = 150;
        _MiniMap_MyHero_MaxCount = 15;
        _MiniMap_EnemySoldier_MaxCount = 150;
        _MiniMap_Boss_MaxCount = 100;


        for (int i = 0; i < _MiniMap_MySoldier_MaxCount; i++)
        {
            GameObject obj = Instantiate(_MiniMap_MySoldier, _MiniMap_Obj_Parent.transform);
            _Mini_MyUnitList.Add(obj);
            obj.SetActive(false);
        }
        for (int i = 0; i < _MiniMap_MyHero_MaxCount; i++)
        {
            GameObject obj = Instantiate(_MiniMap_MyHero, _MiniMap_Obj_Parent.transform);
            _Mini_MyHeroUnitList.Add(obj);
            obj.SetActive(false);
        }
        for (int i = 0; i < _MiniMap_EnemySoldier_MaxCount; i++)
        {
            GameObject obj = Instantiate(_MiniMap_EnemySoldier, _MiniMap_Obj_Parent.transform);
            _Mini_EnemyUnitList.Add(obj);
            obj.SetActive(false);
        }
        for (int i = 0; i < _MiniMap_Boss_MaxCount; i++)
        {
            GameObject obj = Instantiate(_MiniMap_Boss, _MiniMap_Obj_Parent.transform);
            _Mini_BossUnitList.Add(obj);
            obj.SetActive(false);
        }
    }


    void MiniMapUpdate()
    {
        for (int i = 0; i < _MiniMap_MySoldier_MaxCount; i++)
            _Mini_MyUnitList[i].SetActive(false);
        for (int i = 0; i < _MiniMap_MyHero_MaxCount; i++)
            _Mini_MyHeroUnitList[i].SetActive(false);
        for (int i = 0; i < _MiniMap_EnemySoldier_MaxCount; i++)
            _Mini_EnemyUnitList[i].SetActive(false);
        for (int i = 0; i < _MiniMap_Boss_MaxCount; i++)
            _Mini_BossUnitList[i].SetActive(false);

        for (int i = 1, u = 0, h = 0; i < _MySoldierList.Count; i++)
        {
            if(_MySoldierList[i].gameObject)
            {
                if (_MySoldierList[i].GetUnitName()[10] == 'h')
                {
                    _Mini_MyHeroUnitList[h].SetActive(true);
                    _Mini_MyHeroUnitList[h].transform.localPosition =
                        new Vector3((_MySoldierList[i].GetUnitPos().x + _MINIMAP_MINUS_XPOS) / _MINIMAP_WIDTH * _MINIMAP_PIXELWIDTH, MiniPosY(_MySoldierList[i].GetUnitPos().y), 0);
                    h++;
                }
                else
                {
                    _Mini_MyUnitList[u].SetActive(true);
                    _Mini_MyUnitList[u].transform.localPosition =
                        new Vector3((_MySoldierList[i].GetUnitPos().x + _MINIMAP_MINUS_XPOS) / _MINIMAP_WIDTH * _MINIMAP_PIXELWIDTH, MiniPosY(_MySoldierList[i].GetUnitPos().y), 0);
                    u++;
                }
            }
        }
        for (int i = 1, u = 0, b = 0; i < _EnemySoldierList.Count; i++)
        {
            if(_EnemySoldierList[i].gameObject)
            {
                if (_EnemySoldierList[i].GetUnitName()[0] == 'b')
                {
                    _Mini_BossUnitList[b].SetActive(true);
                    _Mini_BossUnitList[b].transform.localPosition =
                             new Vector3((_EnemySoldierList[i].GetUnitPos().x + _MINIMAP_MINUS_XPOS) / _MINIMAP_WIDTH * _MINIMAP_PIXELWIDTH, MiniPosY(_EnemySoldierList[i].GetUnitPos().y), 0);
                    b++;
                }
                else
                {
                    _Mini_EnemyUnitList[u].SetActive(true);
                    _Mini_EnemyUnitList[u].transform.localPosition =
                             new Vector3((_EnemySoldierList[i].GetUnitPos().x + _MINIMAP_MINUS_XPOS) / _MINIMAP_WIDTH * _MINIMAP_PIXELWIDTH, MiniPosY(_EnemySoldierList[i].GetUnitPos().y), 0);
                    u++;
                }
            }
        }
    }

    float MiniPosY(float y)
    {
        float bigrange = _UnitSummonPosY_Max - _UnitSummonPosY_Min;
        float minirange = _MINIMAP_MAXY - _MINIMAP_MINY;

        return (y - _UnitSummonPosY_Min) / bigrange * minirange + _MINIMAP_MINY;
    }
}
