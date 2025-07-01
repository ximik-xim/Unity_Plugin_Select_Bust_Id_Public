using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectBustId : MonoBehaviour
{
   public event Action OnInit;
   public  bool IsInit => _isInit;
   private bool _isInit = false;
   
   [SerializeField]
   private GetDataSODataProductId _keyProduct;
   public KeyProductId KeyProduct => _keyProduct.GetData();

   [SerializeField]
   private GetDataSO_SD_KeyStorageFloatVariable _keySaveId;
   /// <summary>
   /// -1 это начальное значение(это учитывать)
   /// </summary>
   public event Action OnUpdateId;
   private int _currentId = -1;
   public int CurrentId => _currentId;
   
   [SerializeField]
   private int _maxId;
   public int MaxId => _maxId;

   [SerializeField]
   private SBI_AbsSaveAndLoadCurrentId _saveKey;
   [SerializeField]
   private SBI_AbsHaveBuyProduct _isBuyProduct;
   GetServerRequestData<CheckIsBuyProductData> _isBuyProductData;
   [SerializeField]
   private SBI_AbsBuyProduct _buyProduct;
   private GetServerRequestData<BuyProductData> _dataBuy;
   private int _targetSetId;
   private KeyProductId _targetKeyData;

   [SerializeField]
   private LogicTaskGroupDKO _logicTask;
   [SerializeField]
   private DKOKeyAndTargetAction _dko;

   [SerializeField] 
   private SelectBustIdTypeSave _typeSaveData = SelectBustIdTypeSave.SetValueAndSave;

   public bool IsBlock => _isBlock;
   private bool _isBlock = false;
   public event Action OnRemoveBlock;
   
//#if UNITY_EDITOR

   [SerializeField]
   private bool _isDebug;
//#endif
   
   private void Awake()
   {
      _isBlock = true;
      
      if (_saveKey.IsInit == false)
      {
         //#if UNITY_EDITOR
         if (_isDebug == true)
         {
            Debug.Log(gameObject.name + " SBI 10");
         }
//#endif
         _saveKey.OnInit += OnInitSaveKey;
      }
      
      if (_isBuyProduct.IsInit == false)
      {
         //#if UNITY_EDITOR
         if (_isDebug == true)
         {
            Debug.Log(gameObject.name + " SBI 20");
         }
//#endif
         _isBuyProduct.OnInit += OnInitIsBuyProduct;
      }
      
      if (_buyProduct.IsInit == false)
      {
         //#if UNITY_EDITOR
         if (_isDebug == true)
         {
            Debug.Log(gameObject.name + " SBI 30");
         }
//#endif
         _buyProduct.OnInit += OnInitBuyProduct;
      }

      if (_logicTask.IsInit == false)
      {
         //#if UNITY_EDITOR
         if (_isDebug == true)
         {
            Debug.Log(gameObject.name + " SBI 40");
         }
//#endif
         _logicTask.OnInit += OnInitTaskGroup;
      }
      
      CheckInit();
   }

   private void OnInitSaveKey()
   {
      //#if UNITY_EDITOR
      if (_isDebug == true)
      {
         Debug.Log(gameObject.name + " SBI 50");
      }
//#endif
      _saveKey.OnInit -= OnInitSaveKey;
      CheckInit();
   }

   private void OnInitIsBuyProduct()
   {
      //#if UNITY_EDITOR
      if (_isDebug == true)
      {
         Debug.Log(gameObject.name + " SBI 60");
      }
//#endif
      _isBuyProduct.OnInit -= OnInitIsBuyProduct;
      CheckInit();
   }
   
   private void OnInitBuyProduct()
   {
      //#if UNITY_EDITOR
      if (_isDebug == true)
      {
         Debug.Log(gameObject.name + " SBI 70");
      }
//#endif
      _buyProduct.OnInit -= OnInitBuyProduct;
      CheckInit();
   }
   
   private void OnInitTaskGroup()
   {
      //#if UNITY_EDITOR
      if (_isDebug == true)
      {
         Debug.Log(gameObject.name + " SBI 80");
      }
//#endif
      _logicTask.OnInit -= OnInitTaskGroup;
      CheckInit();
   }
   
   private void CheckInit()
   {
      if (_isInit == false)
      {
         if (_saveKey.IsInit == true && _isBuyProduct.IsInit == true && _buyProduct.IsInit == true && _logicTask.IsInit == true)   
         {
//#if UNITY_EDITOR
            if (_isDebug == true)
            {
               Debug.Log(gameObject.name + " SBI 100 Start Init");
            }
//#endif
            Init();
         }   
      }
      
   }

   private void Init()
   {
      if (_saveKey.IsSaveKey(_keySaveId.GetData()) == false)
      {
//#if UNITY_EDITOR
         if (_isDebug == true)
         {
            Debug.Log(gameObject.name + " SBI 200 Init");
         }
//#endif
         _currentId = -1;
         
         _isBlock = false;
         OnRemoveBlock?.Invoke();
            
         _logicTask.StartAction(_dko);
         OnUpdateId?.Invoke();

         _isInit = true;
         OnInit?.Invoke();   
      }
      else
      {
//#if UNITY_EDITOR
         if (_isDebug == true)
         {
            Debug.Log(gameObject.name + " SBI 250");
         }
//#endif
         _currentId = _saveKey.GetSaveId(_keySaveId.GetData());
         
         if (_currentId != -1)
         {
            
//#if UNITY_EDITOR
            if (_isDebug == true)
            {
               Debug.Log(gameObject.name + " SBI 300");
            }
//#endif
            
            string key = GetKeyId(_saveKey.GetSaveId(_keySaveId.GetData()));
            KeyProductId keyData = new KeyProductId(key);

            _isBuyProductData = _isBuyProduct.IsBuyProduct(keyData);

            if (_isBuyProductData.IsGetDataCompleted == false)
            {
               _isBuyProductData.OnGetDataCompleted += OnGetDataCompletedIsBuyProductInit;
               return;
            }
            else
            {
               GetDataCompletedIsBuyProductInit();
               return;
            }
         }
         else
         {
//#if UNITY_EDITOR
            if (_isDebug == true)
            {
               Debug.Log(gameObject.name + " SBI 350");
            }
//#endif
            
            _currentId = -1;
         
            _isBlock = false;
            OnRemoveBlock?.Invoke();
            
            _logicTask.StartAction(_dko);
            OnUpdateId?.Invoke();

            _isInit = true;
            OnInit?.Invoke();   
         }
      }
      
   }
   
   
   
   private void OnGetDataCompletedIsBuyProductInit()
   {
      _isBuyProductData.OnGetDataCompleted -= OnGetDataCompletedIsBuyProductInit;
      GetDataCompletedIsBuyProductInit();
   }

   private void GetDataCompletedIsBuyProductInit()
   {     
      if (_isBuyProductData.StatusServer == StatusCallBackServer.Ok)
      {
         if (_isBuyProductData.GetData.IsBuyProduct == true)
         {
             //#if UNITY_EDITOR
                   if (_isDebug == true)
                   {
                      Debug.Log(gameObject.name + " SBI 400");
                   }
             //#endif
             
            _currentId = _saveKey.GetSaveId(_keySaveId.GetData());

            _isBlock = false;
            OnRemoveBlock?.Invoke();
            
            _logicTask.StartAction(_dko);
            OnUpdateId?.Invoke();

            _isInit = true;
            OnInit?.Invoke();
         }
         else
         {
             //#if UNITY_EDITOR
                   if (_isDebug == true)
                   {
                      Debug.Log(gameObject.name + " SBI 500");
                   }
             //#endif
             
            StartLogic();
            
            int currentId = 0;
            
            void StartLogic()
            {
                
               currentId = 0;
               CounterNext();
            }

            void OnCheck()
            {
               _isBuyProductData.OnGetDataCompleted -= OnCheck;
               Check();
            }

            void Check()
            {
               if (_isBuyProductData.StatusServer == StatusCallBackServer.Ok)
               {
                  if (_isBuyProductData.GetData.IsBuyProduct == true) 
                  {
//#if UNITY_EDITOR
                     if (_isDebug == true)
                     {
                        Debug.Log(gameObject.name + " SBI 700");
                     }
//#endif
                     
                     _currentId = _saveKey.GetSaveId(_keySaveId.GetData());
                     currentId++;
                     CounterNext();
                  }
                  else
                  {
//#if UNITY_EDITOR
                     if (_isDebug == true)
                     {
                        Debug.Log(gameObject.name + " SBI 800");
                     }
//#endif
                     Complited();
                  }   
               }
               else
               {
//#if UNITY_EDITOR
                  if (_isDebug == true)
                  {
                     Debug.Log(gameObject.name + " SBI 900");
                  }
//#endif
                  //Если сервер не ответил на запрос
                  //!!!!!!Возможно в другой ситуации тут нужно вызвать Error или еще что... Но пока пусть будет так
                  Complited();
               }
            }

            void CounterNext()
            {
//#if UNITY_EDITOR
               if (_isDebug == true)
               {
                  Debug.Log(gameObject.name + " SBI 600");
               }
//#endif
               
               for (int i = currentId; i < _maxId; i++)
               {
                  currentId = i;
                  
                  string key2 = GetKeyId(_saveKey.GetSaveId(_keySaveId.GetData()));
                  KeyProductId keyData2 = new KeyProductId(key2);

                  _isBuyProductData = _isBuyProduct.IsBuyProduct(keyData2);

                  if (_isBuyProductData.IsGetDataCompleted == false)
                  {
                     _isBuyProductData.OnGetDataCompleted += OnCheck;
                     return;

                  }
                  else
                  {
                     Check();
                  }
               }

               Complited();
            }

            void Complited()
            {
//#if UNITY_EDITOR
               if (_isDebug == true)
               {
                  Debug.Log(gameObject.name + " SBI 1000");
               }
//#endif
               
               if (_isInit == false)
               {
//#if UNITY_EDITOR
                  if (_isDebug == true)
                  {
                     Debug.Log(gameObject.name + " SBI 1100 INIT");
                  }
//#endif
                  
                  _isBlock = false;
                  OnRemoveBlock?.Invoke();
            
                  _logicTask.StartAction(_dko);
                  OnUpdateId?.Invoke();

                  _isInit = true;
                  OnInit?.Invoke();   
               }
            }
         }
      }
   }
   
   /// /////////////////////////////////////BuyNextId/////////////////////////////////////////////////////////////////////
   
   public void BuyNextId(int setId)
   {
//#if UNITY_EDITOR
      if (_isDebug == true)
      {
         Debug.Log(gameObject.name + " SBI 2000");
      }
//#endif
      
      if (_isBlock == false)
      {
//#if UNITY_EDITOR
         if (_isDebug == true)
         {
            Debug.Log(gameObject.name + " SBI 2100");
         }
//#endif
         _isBlock = true;

         if (setId <= _maxId)
         {
            string key = GetKeyId(setId);
            _targetKeyData = new KeyProductId(key);

            _isBuyProductData = _isBuyProduct.IsBuyProduct(_targetKeyData);
            _targetSetId = setId;
               
            if (_isBuyProductData.IsGetDataCompleted == false)
            {
               //#if UNITY_EDITOR
               if (_isDebug == true)
               {
                  Debug.Log(gameObject.name + " SBI 2110");
               }
//#endif
               
               _isBuyProductData.OnGetDataCompleted += OnGetDataCompletedIsBuyProduct;
               return;
            }
            else
            {
               //#if UNITY_EDITOR
               if (_isDebug == true)
               {
                  Debug.Log(gameObject.name + " SBI 2120");
               }
//#endif
               GetDataCompletedIsBuyProduct();
               return;
            }
         }
      }
   }

   private void OnGetDataCompletedIsBuyProduct()
   {
      //#if UNITY_EDITOR
      if (_isDebug == true)
      {
         Debug.Log(gameObject.name + " SBI 2150");
      }
//#endif
      _isBuyProductData.OnGetDataCompleted -= OnGetDataCompletedIsBuyProduct;
      GetDataCompletedIsBuyProduct();
   }

   private void GetDataCompletedIsBuyProduct()
   {
//#if UNITY_EDITOR
      if (_isDebug == true)
      {
         Debug.Log(gameObject.name + " SBI 2200");
      }
//#endif
      
      if (_isBuyProductData.StatusServer == StatusCallBackServer.Ok)
      {
         if (_isBuyProductData.GetData.IsBuyProduct == false)
         {
//#if UNITY_EDITOR
            if (_isDebug == true)
            {
               Debug.Log(gameObject.name + " SBI 2300");
            }
//#endif
            
            _dataBuy = _buyProduct.BuyProduct(_targetKeyData);

            if (_dataBuy.IsGetDataCompleted == false)
            {
               _dataBuy.OnGetDataCompleted += OnGetDataCompletedBuyProduct;
               return;
            }
            else
            {
               GetDataCompletedBuyProduct();
               return;
            }
         }
         else
         {
            
            //#if UNITY_EDITOR
            if (_isDebug == true)
            {
               Debug.Log(gameObject.name + " SBI 2350");
            }
//#endif
            //Если оказалось что уже куплен
            ComplitedBuy();
         }
      }
      
      _isBlock = false;
      OnRemoveBlock?.Invoke();
   }

   private void OnGetDataCompletedBuyProduct()
   {
      _dataBuy.OnGetDataCompleted -= OnGetDataCompletedBuyProduct;
      GetDataCompletedBuyProduct();
   }

   private void GetDataCompletedBuyProduct()
   {
//#if UNITY_EDITOR
      if (_isDebug == true)
      {
         Debug.Log(gameObject.name + " SBI 2400");
      }
//#endif
      
      if (_dataBuy.StatusServer == StatusCallBackServer.Ok)
      {
         if (_dataBuy.GetData.ProductHaveBuy == true)
         {
//#if UNITY_EDITOR
            if (_isDebug == true)
            {
               Debug.Log(gameObject.name + " SBI 2500");
            }
//#endif
            
            ComplitedBuy();
         }
      }
      
      _isBlock = false;
      OnRemoveBlock?.Invoke();
   }


   private void ComplitedBuy()
   {
//#if UNITY_EDITOR
      if (_isDebug == true)
      {
         Debug.Log(gameObject.name + " SBI 2600 COMPLETED");
      }
//#endif
      
      _currentId = _targetSetId;
               
      _logicTask.StartAction(_dko);

      switch (_typeSaveData)
      {
         case SelectBustIdTypeSave.SetValue:
         {
//#if UNITY_EDITOR
            if (_isDebug == true)
            {
               Debug.Log(gameObject.name + " SBI 2650");
            }
//#endif
            _saveKey.SetId(_keySaveId.GetData(),_currentId);
         } break;
               
         case SelectBustIdTypeSave.SetValueAndSave:
         {
//#if UNITY_EDITOR
            if (_isDebug == true)
            {
               Debug.Log(gameObject.name + " SBI 2700");
            }
//#endif
            _saveKey.SetId(_keySaveId.GetData(), _currentId);
            _saveKey.SaveId();
         } break;
      }
            
      OnUpdateId?.Invoke();
   }
   /// /////////////////////////////////////BuyNextId/////////////////////////////////////////////////////////////////////
    
   /// /////////////////////////////////////SetId/////////////////////////////////////////////////////////////////////
   public void SetId(int setId)
   {
//#if UNITY_EDITOR
      if (_isDebug == true)
      {
         Debug.Log(gameObject.name + " SBI 3000");
      }
//#endif
      
      if (_isBlock == false)
      {
//#if UNITY_EDITOR
         if (_isDebug == true)
         {
            Debug.Log(gameObject.name + " SBI 3100");
         }
//#endif
         
         _isBlock = true;
         if (setId >= 0 && setId <= _maxId)
         {
//#if UNITY_EDITOR
            if (_isDebug == true)
            {
               Debug.Log(gameObject.name + " SBI 3200");
            }
//#endif
            
            string key = GetKeyId(setId);
            KeyProductId keyData = new KeyProductId(key);

            _isBuyProductData = _isBuyProduct.IsBuyProduct(keyData);
            _targetSetId = setId;

            if (_isBuyProductData.IsGetDataCompleted == false)
            {
               _isBuyProductData.OnGetDataCompleted += OnGetDataCompletedIsBuyProductSetId;
               return;
            }
            else
            {
               GetDataCompletedIsBuyProductSetId();
               return;
            }
         }
         else
         {
            //Эта логика отдельно, т.к стартовое значение не будет внесено в список покупок, т.к оно не покупаеться
            if (setId == -1)
            {
               _currentId = -1;
               _logicTask.StartAction(_dko);
                  
               switch (_typeSaveData)
               {
                  case SelectBustIdTypeSave.SetValue:
                  {
//#if UNITY_EDITOR
                     if (_isDebug == true)
                     {
                        Debug.Log(gameObject.name + " SBI 3220");
                     }
//#endif
                  
                     _saveKey.SetId(_keySaveId.GetData(),_currentId);
                  } break;
               
                  case SelectBustIdTypeSave.SetValueAndSave:
                  {
//#if UNITY_EDITOR
                     if (_isDebug == true)
                     {
                        Debug.Log(gameObject.name + " SBI 3250");
                     }
//#endif
                     _saveKey.SetId(_keySaveId.GetData(), _currentId);
                     _saveKey.SaveId();
                  } break;
               }
               
               OnUpdateId?.Invoke();
               
               _isBlock = false;
               OnRemoveBlock?.Invoke();
            }
         }
      }
   }

   private void OnGetDataCompletedIsBuyProductSetId()
   {
      _dataBuy.OnGetDataCompleted -= OnGetDataCompletedIsBuyProductSetId;
      GetDataCompletedIsBuyProductSetId();
   }
   
   private void GetDataCompletedIsBuyProductSetId()
   {
      
//#if UNITY_EDITOR
      if (_isDebug == true)
      {
         Debug.Log(gameObject.name + " SBI 3300");
      }
//#endif
      if (_isBuyProductData.StatusServer == StatusCallBackServer.Ok)
      {
         if (_isBuyProductData.GetData.IsBuyProduct == true)
         {
//#if UNITY_EDITOR
            if (_isDebug == true)
            {
               Debug.Log(gameObject.name + " SBI 3400 COMPLETED");
            }
//#endif
            
            _currentId = _targetSetId;
            _logicTask.StartAction(_dko);
            
            switch (_typeSaveData)
            {
               case SelectBustIdTypeSave.SetValue:
               {
//#if UNITY_EDITOR
                  if (_isDebug == true)
                  {
                     Debug.Log(gameObject.name + " SBI 3450");
                  }
//#endif
                  
                  _saveKey.SetId(_keySaveId.GetData(),_currentId);
               } break;
               
               case SelectBustIdTypeSave.SetValueAndSave:
               {
//#if UNITY_EDITOR
                  if (_isDebug == true)
                  {
                     Debug.Log(gameObject.name + " SBI 3500");
                  }
//#endif
                  _saveKey.SetId(_keySaveId.GetData(), _currentId);
                  _saveKey.SaveId();
               } break;
            }
            
            OnUpdateId?.Invoke();
         }
      }
      
      _isBlock = false;
      OnRemoveBlock?.Invoke();
   }

   /// /////////////////////////////////////SetId/////////////////////////////////////////////////////////////////////
   
   public string GetKeyId(int id)
   {
      return _keyProduct.GetData().GetKey() + id;
   }
   
}



public enum SelectBustIdTypeSave
{
   None,
   SetValue,
   SetValueAndSave
}
