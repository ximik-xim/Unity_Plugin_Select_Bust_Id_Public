using System;
using UnityEngine;

public class GetPriceProductIdSBI_StorageDataIdKeySOPrice : AbsGetPriceProductId
{
    [SerializeField] 
    private SBI_StorageDataIdKeySOPrice _idKeySoPrice;

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
        return _idKeySoPrice.GetStorage().GetData(key).Price;
    }
}
