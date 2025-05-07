using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SBI_AbsStorageDataIdKey<Data>
{
    [SerializeField] 
    private List<AbsKeyData<GetDataSODataProductId, SBI_AbsStorageDataId<Data>>> _listData = new List<AbsKeyData<GetDataSODataProductId, SBI_AbsStorageDataId<Data>>>();
    private Dictionary<string, SBI_AbsStorageDataId<Data>> _dictionaryData = new Dictionary<string, SBI_AbsStorageDataId<Data>>();
    
    private Dictionary<string, Data> _dictionaryData2 = new Dictionary<string, Data>();
    
    private bool _init = false;
    public bool Init => _init;
    public event Action OnInit;
    
    public void StartInit()
    {
        foreach (var VARIABLE in _listData)
        {
            VARIABLE.Data.StartInit();
            _dictionaryData.Add(VARIABLE.Key.GetData().GetKey(), VARIABLE.Data);

            foreach (var VARIABLE2 in VARIABLE.Data.GetAllId())
            {
                _dictionaryData2.Add(GetKey(VARIABLE.Key.GetData(), VARIABLE2), VARIABLE.Data.GetData(VARIABLE2));
            }
            
        }       

        _init = true;
        OnInit?.Invoke();
    }
        
    public void AutoCreate()
    {
        foreach (var VARIABLE in _listData)
        {
            VARIABLE.Data.AutoCreate();
        }
    }

    public SBI_AbsStorageDataId<Data> GetStorage(KeyProductId key)
    {
        return _dictionaryData[key.GetKey()];
    }

    public Data GetData(KeyProductId key)
    {
        return _dictionaryData2[key.GetKey()];
    }

    private string GetKey(KeyProductId key, int id)
    {
        return key.GetKey() + id;
    }
}
