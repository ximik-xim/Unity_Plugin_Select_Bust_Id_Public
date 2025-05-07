using System;
using UnityEngine;
using UnityEngine.UI;

public class SBI_ButtonAddAndRemoveId : MonoBehaviour
{
    [SerializeField] 
    private Button _button;

    [SerializeField] 
    private SelectBustId _selectBustId;
    
    [SerializeField] 
    private SBI_TypeActionAddAnRemoveId _type;
    
    private void Awake()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (_type == SBI_TypeActionAddAnRemoveId.IdAdd)
        {
            if (_selectBustId.CurrentId < _selectBustId.MaxId)
            {
                _selectBustId.SetId(_selectBustId.CurrentId + 1);
            }
        }
        
        if (_type == SBI_TypeActionAddAnRemoveId.IdRemove)
        {
            if (_selectBustId.CurrentId > -1)
            {
                _selectBustId.SetId(_selectBustId.CurrentId - 1);
            }
        }
    }
    
    
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }
}

public enum SBI_TypeActionAddAnRemoveId
{
    IdAdd,
    IdRemove
}