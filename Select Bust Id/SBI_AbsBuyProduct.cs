using System;
using UnityEngine;

public abstract class SBI_AbsBuyProduct : MonoBehaviour
{
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }
    
    public abstract GetServerRequestData<BuyProductData> BuyProduct(KeyProductId key);
}
