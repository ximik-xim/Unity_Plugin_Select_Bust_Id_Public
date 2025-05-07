using System.Collections.Generic;
using UnityEngine;

public class SBI_UIVisualIdSelect : MonoBehaviour
{
    [SerializeField] 
    private List<AbsKeyData<int, GameObject>> _IdListTask;
    private Dictionary<int, GameObject> _data = new Dictionary<int, GameObject>();
    
    [SerializeField] 
    private SelectBustId _selectBust;
    
    [SerializeField]
    private int _maxId;
        
    private void Awake()
    {
        foreach (var VARIABLE in _IdListTask)
        {
            _data.Add(VARIABLE.Key, VARIABLE.Data);
        }
        
        if (_selectBust.IsInit == false)
        {
            _selectBust.OnInit += OnInit;
            return;
        }

        Init();
    }

    private void OnInit()
    {
        _selectBust.OnInit -= OnInit;
        Init();
    }

    private void Init()
    {
        _selectBust.OnUpdateId += OnUpdateId;
        OnUpdateId();
    }

    private void OnUpdateId()
    {
        for (int i = 0; i <= _selectBust.CurrentId; i++)
        {
            _data[i].SetActive(true);
        }

        for (int i = _selectBust.CurrentId + 1; i <= _selectBust.MaxId; i++) 
        {
            _data[i].SetActive(false);
        }
    }
    
    private void OnValidate()
    {
        if (_IdListTask.Count < _maxId + 1)
        {
            for (int j = _IdListTask.Count - 1; j < _maxId; j++)
            {
                _IdListTask.Add(new AbsKeyData<int, GameObject>(j, default));
            }
        }
        
        for (int i = 0; i < _IdListTask.Count; i++)
        {
            if (_IdListTask[i].Key != i)
            {
                _IdListTask[i].Key = i;
            }
        }
        
    }
}
