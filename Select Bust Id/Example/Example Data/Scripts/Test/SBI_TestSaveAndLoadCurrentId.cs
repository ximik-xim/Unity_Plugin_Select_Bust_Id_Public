using UnityEngine;
using System;

public class SBI_TestSaveAndLoadCurrentId : SBI_AbsSaveAndLoadCurrentId
{
    [SerializeField] 
    private bool _returnValueIsSave;

    [SerializeField] 
    private int _idSave = 1;
    
    public override event Action OnInit;
    public override bool IsInit => true;

    private void Awake()
    {
        OnInit?.Invoke();
    }
    
    public override bool IsSaveKey(SD_KeyStorageFloatVariable key)
    {
        return _returnValueIsSave;
    }

    public override int GetSaveId(SD_KeyStorageFloatVariable key)
    {
        return _idSave;
    }

    public override void SetId(SD_KeyStorageFloatVariable key, int id)
    {
       
    }

    public override void SaveId()
    {
       
    }
}
