using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryMng : MonoBehaviour
{
    List<string> _StoryList = new List<string>();

    [SerializeField] GameObject _TextTable;
    [SerializeField] Text _StoryText;
    [SerializeField] GameObject[] _StoryBookGray = new GameObject[12];

    private void Awake()
    {
        for(int i=0;i<12;i++)
        {
            _StoryList.Add(i.ToString());
        }
    }

    public void OpenStoryBook(int num)
    {
        _TextTable.SetActive(true);
        _StoryText.text = _StoryList[num - 1];
    }

    public void CloseStoryBook()
    {
        _TextTable.SetActive(false);
    }
}
