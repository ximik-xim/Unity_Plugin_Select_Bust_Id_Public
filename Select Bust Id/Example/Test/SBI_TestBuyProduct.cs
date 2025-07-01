using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class SBI_TestBuyProduct : SBI_AbsBuyProduct
{
    [SerializeField] 
    private bool _returnValue;

    public override event Action OnInit;
    public override bool IsInit => true;

    private void Awake()
    {
        OnInit?.Invoke();
    }
    
    public override GetServerRequestData<BuyProductData> BuyProduct(KeyProductId key)
    {
        var data = new ServerRequestDataWrapperBuyProductData(Random.Range(0, 10000));
        data.Data.StatusServer = StatusCallBackServer.Ok;
        data.Data.GetData = new BuyProductData(_returnValue);

        data.Data.IsGetDataCompleted = true;
        data.Data.Invoke();
        
        return data.DataGet;
    }
}
