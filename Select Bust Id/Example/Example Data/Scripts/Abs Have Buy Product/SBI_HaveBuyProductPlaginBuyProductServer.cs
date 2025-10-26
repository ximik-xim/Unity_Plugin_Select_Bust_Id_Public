using System;
using UnityEngine;

/// <summary>
/// (обертка) Проверяет был ли куплен товар через логику проверки купленного товара у плагина Buy Product Server
/// </summary>
public class SBI_HaveBuyProductPlaginBuyProductServer : SBI_AbsHaveBuyProduct
{
    [SerializeField] 
    private CheckIsBuyProductWrapper _checkIsBuyProduct;

    public override event Action OnInit
    {
        add
        {
            _checkIsBuyProduct.OnInit += value;
        }
        remove
        {
            _checkIsBuyProduct.OnInit -= value;
        }
        
    }
    public override bool IsInit => _checkIsBuyProduct.IsInit;
    
    public override GetServerRequestData<CheckIsBuyProductData> IsBuyProduct(KeyProductId key)
    {
        return _checkIsBuyProduct.CheckProductHaveBuy(key);
    }
}
