using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//[CreateAssetMenu(menuName = "TTTTT/TTTTT2")]
public abstract class SBI_AbsStorageDataIdKeySO<Data> : ScriptableObject, IInitScripObj
{
    [SerializeField] 
    private SBI_AbsStorageDataIdKey<Data> _data = new SBI_AbsStorageDataIdKey<Data>();
    
    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;

    private void Awake()
    {
        if (_isInit == false)
        {
            _data.StartInit();
            
            _isInit = true;
            OnInit?.Invoke();
        }
    }

    private void OnValidate()
    {
        _data.AutoCreate();
    }
    
    public SBI_AbsStorageDataIdKey<Data> GetStorage()
    {
        return _data;
    }
    
    
    public void InitScripObj()
    {
#if UNITY_EDITOR
        
        EditorApplication.playModeStateChanged -= OnUpdateStatusPlayMode;
        EditorApplication.playModeStateChanged += OnUpdateStatusPlayMode;

        //На случай если event playModeStateChanged не отработает при входе в режим PlayModeStateChange.EnteredPlayMode (такое может быть, и как минимум по этому нужна пер. bool _init)
        if (EditorApplication.isPlaying == true)
        {
            if (_isInit == false)
            {
                Awake();
            }
        }
#endif
    }
        
#if UNITY_EDITOR
    private void OnUpdateStatusPlayMode(PlayModeStateChange obj)
    {
        //При выходе из Play Mode произвожу очистку данных(тем самым эмулирую что при след. запуске(вхождение в Play Mode) данные будут обнулены)
        if (obj == PlayModeStateChange.ExitingPlayMode)
        {
            if (_isInit == true)
            {
                _isInit = false;
            }
        }
        
        // При запуске игры эмулирую иниц. SO(По идеи не совсем верно, т.к Awake должен произойти немного раньше, но пофиг)(как показала практика метод может не сработать)
        if (obj == PlayModeStateChange.EnteredPlayMode)
        {
            if (_isInit == false)
            {
                Awake();
            }
            
        }
    }
#endif

    
    private void OnDestroy()
    {
#if UNITY_EDITOR
        EditorApplication.playModeStateChanged -= OnUpdateStatusPlayMode;
#endif
    }
}
