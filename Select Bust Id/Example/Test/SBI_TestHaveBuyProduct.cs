using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SBI_TestHaveBuyProduct : SBI_AbsHaveBuyProduct
{
    [SerializeField] 
    private bool _returnValue;

    public override event Action OnInit;
    public override bool IsInit => true;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override GetServerRequestData<CheckIsBuyProductData> IsBuyProduct(KeyProductId key)
    {
        var data = new ServerRequestDataWrapperCheckIsBuyProductData(Random.Range(0, 10000));
        data.Data.StatusServer = StatusCallBackServer.Ok;
        data.Data.GetData = new CheckIsBuyProductData(_returnValue);

        return data.DataGet;
    }
}
