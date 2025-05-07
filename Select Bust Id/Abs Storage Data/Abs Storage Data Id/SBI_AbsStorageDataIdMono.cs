using System;
using UnityEngine;

public abstract class SBI_AbsStorageDataIdMono<Data> : MonoBehaviour
{
    [SerializeField] 
    private SBI_AbsStorageDataId<Data> _data = new SBI_AbsStorageDataId<Data>();
    
    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;

    private void Awake()
    {
        _data.StartInit();
        
        _isInit = true;
        OnInit?.Invoke();
    }

    private void OnValidate()
    {
        _data.AutoCreate();
    }
    
    public SBI_AbsStorageDataId<Data> GetStorage()
    {
        return _data;
    }
}
