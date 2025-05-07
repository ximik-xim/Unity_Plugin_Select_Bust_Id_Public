using System;
using UnityEngine;
using UnityEngine.UI;

public class SBI_UITextCharacteristicFloat : MonoBehaviour
{
   [SerializeField] 
   private SBI_SelectBustIdAddAndRemoveBustFloat _addAndRemoveBust;
   
   [SerializeField] 
   private CharacteristicStorageFloat _characteristicStorage;

   [SerializeField] 
   private Text _text;

   [SerializeField] 
   private string _preText;
   
   [SerializeField] 
   private string _pastText;
   
   private void Awake()
   {
      if (_addAndRemoveBust.IsInit == false)
      {
         _addAndRemoveBust.OnInit += OnInit;
         return;
      }

      Init();
   }

   private void OnInit()
   {
      _addAndRemoveBust.OnInit -= OnInit;
      Init();
   }
   
   private void Init()
   {
      _characteristicStorage.GetCharacteristicData(_addAndRemoveBust.KeyCharacteristic).OnUpdateValue += OnUpdateValue;
      OnUpdateValue();
   }

   private void OnUpdateValue()
   {
      _text.text = _preText + _characteristicStorage.GetCharacteristicData(_addAndRemoveBust.KeyCharacteristic).GetValue() + _pastText;
   }

   private void OnDestroy()
   {
      _characteristicStorage.GetCharacteristicData(_addAndRemoveBust.KeyCharacteristic).OnUpdateValue -= OnUpdateValue;
   }
}
