using System;
using UnityEngine;

public abstract class SBI_AbsSaveAndLoadCurrentId : MonoBehaviour
{
   public abstract event Action OnInit;
   public abstract bool IsInit { get; }
   
   public abstract bool IsSaveKey(SD_KeyStorageFloatVariable key);
   public abstract int GetSaveId(SD_KeyStorageFloatVariable key);

   public abstract void SetId(SD_KeyStorageFloatVariable key, int id);
   public abstract void SaveId();
}
