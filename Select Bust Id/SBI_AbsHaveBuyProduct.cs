using System;
using UnityEngine;

/// <summary>
/// Абстракция для проверки, был ли куплен товар
/// </summary>
public abstract class SBI_AbsHaveBuyProduct : MonoBehaviour
{
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }
    
    public abstract GetServerRequestData<CheckIsBuyProductData> IsBuyProduct(KeyProductId key);
}
