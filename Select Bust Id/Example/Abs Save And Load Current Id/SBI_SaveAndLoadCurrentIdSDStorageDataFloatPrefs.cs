using System;
using UnityEngine;

public class SBI_SaveAndLoadCurrentIdSDStorageDataFloatPrefs : SBI_AbsSaveAndLoadCurrentId
{
    public override bool IsInit => _storageSaveData.IsInit;

    public override event Action OnInit
    {
        add
        {
            _storageSaveData.OnInit += value;
        }
        remove
        {
            _storageSaveData.OnInit -= value;
        }
    }
    
    [SerializeField] 
    private SD_StorageDataFloatPrefs _storageSaveData;

    public override bool IsSaveKey(SD_KeyStorageFloatVariable key)
    {
        if (_storageSaveData.IsThereData(key) == true)
        {
            return true;
        }

        return false;
    }

    public override int GetSaveId(SD_KeyStorageFloatVariable key)
    {
        return (int)_storageSaveData.GetData(key);
    }

    public override void SetId(SD_KeyStorageFloatVariable key, int id)
    {
        _storageSaveData.SetData(key, id);
    }

    public override void SaveId()
    {
        _storageSaveData.SaveData(new TaskInfo("Save Select Bust Id"));
    }
}
