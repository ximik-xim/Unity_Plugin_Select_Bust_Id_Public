using System;
using UnityEngine;

/// <summary>
/// Перенаправляет на логику покупок из плагину
/// Buy Product Server
/// </summary>
public class SBI_BuyProductPlaginBuyProductServer : SBI_AbsBuyProduct
{
    [SerializeField] 
    private BuyProductWrapper _buyProduct;

    public override event Action OnInit
    {
        add
        {
            _buyProduct.OnInit += value;
        }
        remove
        {
            _buyProduct.OnInit -= value;
        }
        
    }
    public override bool IsInit => _buyProduct.IsInit;
    public override GetServerRequestData<BuyProductData> BuyProduct(KeyProductId key)
    {
        return _buyProduct.BuyProduct(key);
    }
}
