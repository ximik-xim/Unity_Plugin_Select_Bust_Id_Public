using System;
using UnityEngine;

/// <summary>
/// обстракция для запуска покупки товара
/// </summary>
public abstract class SBI_AbsBuyProduct : MonoBehaviour
{
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }
    
    public abstract GetServerRequestData<BuyProductData> BuyProduct(KeyProductId key);
}
