using System;
using UnityEngine;

public class SBI_ListTaskMinAndMaxId : MonoBehaviour
{
    [SerializeField] 
    private SelectBustId _selectBust;

    [SerializeField] 
    private SBI_TypeTaskMinAndMax _targetValue;

    [SerializeField] 
    private LogicListTaskDKO _listTaskDko;

    [SerializeField] 
    private DKOKeyAndTargetAction _dko;

    private void Awake()
    {
        if (_selectBust.IsInit == false)
        {
            _selectBust.OnInit += OnInitSelectBustId;
        }
        
        if (_listTaskDko.IsInit == false)
        {
            _listTaskDko.OnInit += OnInitListTask;
        }

        CheckInit();
    }

    private void OnInitListTask()
    {
        _listTaskDko.OnInit -= OnInitListTask;
        CheckInit();
    }

    private void OnInitSelectBustId()
    {
        _selectBust.OnInit -= OnInitSelectBustId;
        CheckInit();
    }

    private void CheckInit()
    {
        if (_selectBust.IsInit == true && _listTaskDko.IsInit == true)
        {
            Init();
        }
    }

    private void Init()
    {
        _selectBust.OnUpdateId += OnUpdateId;
        OnUpdateId();
    }
    
    private void OnUpdateId()
    {
        if (_targetValue == SBI_TypeTaskMinAndMax.Min)
        {
            if (_selectBust.CurrentId == -1)
            {
                _listTaskDko.StartAction(_dko);
            }
        }
        
        if (_targetValue == SBI_TypeTaskMinAndMax.Max)
        {
            if (_selectBust.CurrentId == _selectBust.MaxId)
            {
                _listTaskDko.StartAction(_dko);
            }
        }
    }

    private void OnDestroy()
    {
        _selectBust.OnUpdateId -= OnUpdateId;
    }
}

public enum SBI_TypeTaskMinAndMax
{
    Min,
    Max
}
