using System;
using UnityEngine;
using UnityEngine.UI;

public class SBI_ButtonBuyProduct : MonoBehaviour
{
    [SerializeField] 
    private SelectBustId _selectBust;
    
    [SerializeField] 
    private Button _button;

    [SerializeField] 
    private SBI_StorageDataIdKeySOPrice _storagePrice;
    
    private void Awake()
    {
        if (_selectBust.IsInit == false)
        {
            _selectBust.OnInit += OnInitSelectBus;
        }

        if (_storagePrice.IsInit == false)
        {
            _storagePrice.OnInit += OnInitStoragePrice;
        }
        
        CheckInit();
    }

    private void OnInitSelectBus()
    {
        _selectBust.OnInit -= OnInitSelectBus;
        
        CheckInit();
    }
    
    private void OnInitStoragePrice()
    {
        _storagePrice.OnInit -= OnInitStoragePrice;
        CheckInit();
    }

    
    private void CheckInit()
    {
        if (_selectBust.IsInit == true && _storagePrice.IsInit == true)  
        {
            Init();
        }   
    }

    private void Init()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (_selectBust.CurrentId >= -1 && _selectBust.CurrentId < _selectBust.MaxId)
        {
            _selectBust.BuyNextId(_selectBust.CurrentId + 1);
        }
    }

    private void OnDestroy()
    {
        _button.onClick.AddListener(OnClick);
    }
}
