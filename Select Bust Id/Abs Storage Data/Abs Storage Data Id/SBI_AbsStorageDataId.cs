using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SBI_AbsStorageDataId<Data>
{
    [SerializeField]
    private int _maxId;

    [SerializeField] 
    private List<AbsKeyData<int, Data>> _listData = new List<AbsKeyData<int, Data>>();
    private Dictionary<int, Data> _dictionaryData = new Dictionary<int, Data>();
    
    private bool _init = false;
    public bool Init => _init;
    public event Action OnInit;

    public void StartInit()
    {
        foreach (var VARIABLE in _listData)
        {
            _dictionaryData.Add(VARIABLE.Key, VARIABLE.Data);
        }

        _init = true;
        OnInit?.Invoke();
    }
        
    public void AutoCreate()
    {
        if (_listData.Count < _maxId + 1)
        {
            for (int j = _listData.Count - 1; j < _maxId; j++)
            {
                _listData.Add(new AbsKeyData<int, Data>(j, default));
            }
        }
        
        for (int i = 0; i < _listData.Count; i++)
        {
            if (_listData[i].Key != i)
            {
                _listData[i].Key = i;
            }
        }
    }

    public Data GetData(int id)
    {
        return _dictionaryData[id];
    }

    public List<int> GetAllId()
    {
        List<int> data = new List<int>();
        foreach (var VARIABLE in _dictionaryData.Keys)
        {
            data.Add(VARIABLE);
        }

        return data;
    }
}
