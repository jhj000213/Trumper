using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelpTableMng : MonoBehaviour
{
    [SerializeField] GameObject _TargetTable;

    public void HelpTable_Open()
    {
        _TargetTable.SetActive(true);
    }

    public void HelpTable_Close()
    {
        _TargetTable.SetActive(false);
    }
}
