using System;
using UnityEngine;
using UnityEngine.UI;


public class SBI_ButtonBuyProductInterectableLogic : MonoBehaviour
{
    [SerializeField] 
    private SelectBustId _selectBust;
    
    [SerializeField] 
    private Button _button;

    [SerializeField] 
    private Text _text;
    
    [SerializeField] 
    private SBI_StorageDataIdKeySOPrice _storagePrice;

    [SerializeField] 
    private GetDKOPatch _patchStorageMoney;
    private StorageMoney _storageMoney;
    
    //Возможно потом как то по другому буду определять куплен ли след. продукт(т.к не во всех случаеях смогу так в тупую получить список купленных товаров)
    [SerializeField] 
    private SD_StorageDataStringPrefs _storageSaveData;
    [SerializeField] 
    private SD_GetClassKeyDataGetDKOString _keySaveData;

    private void Awake()
    {
        _button.interactable = false;
        _text.text = "-";

        if (_selectBust.IsInit == false)
        {
            _selectBust.OnInit += OnInitSelectBust;
        }

        if (_storagePrice.IsInit == false)
        {
            _storagePrice.OnInit += OnInitStoragePrice;
        }
        
        if (_patchStorageMoney.Init == false)
        {
            _patchStorageMoney.OnInit += OnInitPatchStorageMoney;
        }
        
        if (_storageSaveData.IsInit == false)
        {
            _storageSaveData.OnInit += OnInitStorageSaveData;
        }

        CheckInit();
    }

    private void OnInitSelectBust()
    {
        _selectBust.OnInit -= OnInitSelectBust;
        CheckInit();
    }
    
    private void OnInitStoragePrice()
    {
        _storagePrice.OnInit -= OnInitStoragePrice;
        CheckInit();
    }

    private void OnInitPatchStorageMoney()
    {
        _patchStorageMoney.OnInit -= OnInitPatchStorageMoney;
        CheckInit();
    }

    private void OnInitStorageSaveData()
    {
        _storageSaveData.OnInit -= OnInitStorageSaveData;
        CheckInit();
    }



    private void CheckInit()
    {
        if (_selectBust.IsInit == true && _storagePrice.IsInit == true && _patchStorageMoney.Init == true && _storageSaveData.IsInit == true) 
        {
            var DKOData = (DKODataInfoT<StorageMoney>)_patchStorageMoney.GetDKO();
            _storageMoney = DKOData.Data;
            
            Init();
        }
    }

    private void Init()
    {
        _selectBust.OnRemoveBlock += OnRemoveBlock;
        _selectBust.OnUpdateId += OnUpdateId;

        _storageMoney.OnUpdateCount += OnUpdateCountMoney;

        _storageSaveData.OnUpdateData += OnUpdateDataStorageSave;
        _storageSaveData.OnUpdateValue += OnUpdateValueStorageSave;
        
        Check();
    }
    
    private void OnUpdateCountMoney()
    {
        Check();
    }
    
    private void OnUpdateId()
    {
        Check();
    }

    private void OnRemoveBlock()
    {
        Check();
    }
    
    private void OnUpdateDataStorageSave()
    {
        Check();
    }
    
    private void OnUpdateValueStorageSave(SD_KeyStorageStringVariable obj)
    {
        Check();
    }

    private void Check()
    {
        if (IsBlockBuy() == false)
        {
            if (IsMaxValue() == false)
            {
                if (IsBuyNextProduct() == false)
                {
                    if (IsBlockCountMoney() == false)
                    {
                        
                    }
                }
            }

        }
    }
    
    private bool IsBlockBuy()
    {
        if (_selectBust.IsBlock == true)
        {
            _button.interactable = false;
            _text.text = "-";
            
            return true;
        }

        return false;
    }

    private bool IsMaxValue()
    {
        if (_selectBust.CurrentId >= -1 && _selectBust.CurrentId < _selectBust.MaxId)
        {
            return false;
        }
        
        _button.interactable = false;
        _text.text = "Макс";
        
        return true;
    }

    private bool IsBuyNextProduct()
    {
        string jsData = _storageSaveData.GetData(_keySaveData.GetKey());
        ListProductBuy keyProductId = JsonUtility.FromJson<ListProductBuy>(jsData);

        if (keyProductId == null)
        {
            keyProductId = new ListProductBuy();
        }
    
        string key = _selectBust.KeyProduct.GetKey() + (_selectBust.CurrentId + 1);
                 
        if (keyProductId.KeyProductId.Contains(key) == true)
        {
            _button.interactable = false;
            _text.text = "-";

            return true;
        }

        return false;
    }
    
    private bool IsBlockCountMoney()
    {
        if (_selectBust.CurrentId >= -1 && _selectBust.CurrentId < _selectBust.MaxId)
        {
            float priceNext = _storagePrice.GetStorage().GetStorage(_selectBust.KeyProduct).GetData(_selectBust.CurrentId + 1).Price;

            if (_storageMoney.GetCountMoney() >= priceNext)
            {
                _button.interactable = true;
                _text.text = priceNext.ToString();

                return false;
            }

            _button.interactable = false;
            _text.text = priceNext.ToString();

            return true;
        }
        
        return false;
    }
   
    private void OnDestroy()
    {
        _selectBust.OnRemoveBlock -= OnRemoveBlock;
        _selectBust.OnUpdateId -= OnUpdateId;

        _storageMoney.OnUpdateCount -= OnUpdateCountMoney;

        _storageSaveData.OnUpdateData -= OnUpdateDataStorageSave;
        _storageSaveData.OnUpdateValue -= OnUpdateValueStorageSave;
    }
}
