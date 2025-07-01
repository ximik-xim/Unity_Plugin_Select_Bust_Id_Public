using System;
using UnityEngine;

public class GetPriceProductIdSBI_StorageDataIdKeySOPrice : AbsGetPriceProductId
{
    [SerializeField] 
    private SBI_StorageDataIdKeySOPrice _idKeySoPrice;

    /// <summary>
    /// Если нет ценника в хранилеще с цениками, значит товар бесплатный(вернет стоймость = 0)
    /// </summary>
    [SerializeField]
    private bool _useFreeItemNoKey;
    
    public override event Action OnInit
    {
        add
        {
            _idKeySoPrice.OnInit += value;
        }
        
        remove
        {
            _idKeySoPrice.OnInit -= value;    
        }
    }
    
    public override bool IsInit => _idKeySoPrice.IsInit;
    
    public override float GetPriceProduct(KeyProductId key)
    {
        if (_useFreeItemNoKey == true) 
        {
            if (_idKeySoPrice.GetStorage().IsThereKey(key) == false)
            {
                Debug.Log($"ВНИМАНИЕ! Ключ товара {key.GetKey()} не был найден в хранилище с ценниками. А значит товар бесплатны(его цена = 0)");

                return 0;
            }
        }
        
        return _idKeySoPrice.GetStorage().GetData(key).Price;
    }
}
