using UnityEngine;

[System.Serializable]
public class SBI_StorageDataIdKeySOBustDataFloatData
{
    [SerializeField] 
    private GetDataSO_KeyBustData _keyBust;
    public KeyBustData GetKeyBust => _keyBust.GetData();
   
    [SerializeField]
    private BustDataFloat _bustData;
    public BustDataFloat BustData => _bustData;
}
