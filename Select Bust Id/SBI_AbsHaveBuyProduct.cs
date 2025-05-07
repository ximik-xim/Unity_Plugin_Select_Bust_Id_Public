using System;
using UnityEngine;

public abstract class SBI_AbsHaveBuyProduct : MonoBehaviour
{
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }
    
    public abstract GetServerRequestData<CheckIsBuyProductData> IsBuyProduct(KeyProductId key);
}
