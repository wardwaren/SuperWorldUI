using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIWindow : MonoBehaviour
{
    protected List<TMP_InputField> inputFields;
    
    protected Color MY_RED = new Color(0.69f, 0.17f, 0.17f);
    
    protected const string DATABASE_NAME = "users";
    
    protected StorageManager storageManager;

    protected Dictionary<String, User> userDatabase;

    protected bool mistake = false;

    protected bool oneFieldMistake = false;

    protected List<GameObject> incorrectFields; 
    
    public void ShowWindow(bool show)
    {
        if (!show && inputFields != null)
        {
            ColorInputFields(Color.white, new Color(0.19f, 0.19f, 0.19f));
            CleanInputFields();
        }
        
        gameObject.SetActive(show);
    }

    protected void OnEnable()
    {
        if(storageManager == null)
            storageManager = new StorageManager();
        
        userDatabase = (Dictionary<String,User>) storageManager.LoadData(DATABASE_NAME) ?? new Dictionary<string, User>();
    }
    
    protected void CleanInputFields()
    {
        foreach (var inputField in inputFields)
        {
            inputField.text = "";
        }
    }
    
    protected void ColorInputFields(Color inputColor, Color textColor)
    {
        foreach (var inputField in inputFields)
        {
            inputField.gameObject.GetComponent<Image>().color = inputColor;
            inputField.textComponent.color = textColor;
        }
    }
    
    protected void ColorInputField(Color inputColor, Color textColor, TMP_InputField inputField)
    {
        inputField.gameObject.GetComponent<Image>().color = inputColor;
        inputField.textComponent.color = textColor;
    }
    
    protected void Update()
    {
        if (mistake && EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>() == null) return;
            
            mistake = false;
            ColorInputFields(Color.white, new Color(0.19f, 0.19f, 0.19f));
            CleanInputFields();
        }
    }
}
