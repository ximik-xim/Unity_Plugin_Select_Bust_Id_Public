using System;
using UnityEngine;

public class SBI_SelectBustIdAddAndRemoveBustFloat : MonoBehaviour
{
    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;
    
    [SerializeField] 
    private SelectBustId _selectBust;

    [SerializeField] 
    private StorageBustFloat _storageBust;

    [SerializeField]
    private GetDataSO_KeyCharacteristicFloat _keyCharacteristic;
    public KeyCharacteristicFloat KeyCharacteristic => _keyCharacteristic.GetData();

    [SerializeField] 
    private SBI_StorageDataIdKeySOBustDataFloat _storageDataBust;
    
    private void Awake()
    {
        if (_selectBust.IsInit == false)
        {
            _selectBust.OnInit += OnInitSelectBus;
        }

        if (_storageDataBust.IsInit == false)
        {
            _storageDataBust.OnInit += OnInitStorageDataBust;
        }
        
        CheckInit();
    }

    private void OnInitSelectBus()
    {
        _selectBust.OnInit -= OnInitSelectBus;
        
        CheckInit();
    }
    
    private void OnInitStorageDataBust()
    {
        _storageDataBust.OnInit -= OnInitStorageDataBust;
        CheckInit();
    }
    
    private void CheckInit()
    {
        if (_selectBust.IsInit == true && _storageDataBust.IsInit == true)  
        {
            Init();
        }   
    }

    private void Init()
    {
        _selectBust.OnUpdateId += OnUpdateId;
        OnUpdateId();

        _isInit = true;
        OnInit?.Invoke();
    }

    private void OnUpdateId()
    {
        //При каждом обновлении ключа тупо удаляю все старые бусты
        for (int i = 0; i <= _selectBust.MaxId; i++)
        {
            var key = _selectBust.KeyProduct;
            var storageKey = _storageDataBust.GetStorage();
            var storageId = storageKey.GetStorage(key);
            var dataBust = storageId.GetData(i);
        
        
            var bustStorage = _storageBust.GetBustData(_keyCharacteristic.GetData());
            if (bustStorage.GetBustLogic.IsKeyBust(dataBust.GetKeyBust) == true) 
            {
                bustStorage.GetBustLogic.RemoveBust(dataBust.GetKeyBust);    
            }
        }
        
        //И затем добавляю бусты до текущего значения(не очень эффективно, но зато точно работать будет)
        for (int i = 0; i <= _selectBust.CurrentId; i++)
        {
            var key = _selectBust.KeyProduct;
            var storageKey = _storageDataBust.GetStorage();
            var storageId = storageKey.GetStorage(key);
            var dataBust = storageId.GetData(i);
        
            var bustStorage = _storageBust.GetBustData(_keyCharacteristic.GetData());

            bustStorage.GetBustLogic.AddBust(dataBust.GetKeyBust, dataBust.BustData);
        }
    }

    private void OnDestroy()
    {
        _selectBust.OnUpdateId -= OnUpdateId;
    }
}