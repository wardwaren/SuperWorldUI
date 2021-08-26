using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResetPasswordWindow : UIWindow
{
    [SerializeField] private TMP_InputField newPasswordInput;
    [SerializeField] private TMP_InputField passwordConfirmInput;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button updatePasswordButton;
    
    private UnityAction goToSuccessWindow;

    public User user { private get; set;}

    private void Start()
    {
        inputFields = new List<TMP_InputField> {newPasswordInput, passwordConfirmInput};

        if(storageManager == null)
            storageManager = new StorageManager();
        
        updatePasswordButton.onClick.AddListener(ResetPasswordInformation);
        
        userDatabase = (Dictionary<String,User>) storageManager.LoadData(DATABASE_NAME) ?? new Dictionary<string, User>();
    }
    
    

    public void GoToForgotWindow(UnityAction returnToForgotPassword)
    {
        closeButton.onClick.AddListener(returnToForgotPassword);
    }

    public void GoToSuccessWindow(UnityAction goToSuccessWindow)
    {
        this.goToSuccessWindow = goToSuccessWindow;
    }

    private void ResetPasswordInformation()
    {
        if (checkPasswordMatch() && checkPasswordValidity())
        {
            updatePassword();
            goToSuccessWindow.Invoke();
        }
        else if (checkPasswordMatch())
        {
            passwordNotValid();
        }
        else
        {
            passwordsDontMatch();
        }
    }

    private bool checkPasswordMatch()
    {
        return newPasswordInput.text == passwordConfirmInput.text;
    }

    private void updatePassword()
    {
        userDatabase[user.email].password = newPasswordInput.text;
        storageManager.SaveData(userDatabase, DATABASE_NAME);
    }

    private bool checkPasswordValidity()
    {
        return passwordConfirmInput.text.Length >= 8;
    }

    private void passwordNotValid()
    {
        mistake = true;
        ColorInputFields(MY_RED, MY_RED);
        
        passwordConfirmInput.text = "Password must include 8 characters";
        newPasswordInput.text = "Password must include 8 characters";
    }
    
    private void passwordsDontMatch()
    {
        mistake = true;
        ColorInputFields(MY_RED, MY_RED);

        passwordConfirmInput.text = "Passwords don't match";
        newPasswordInput.text = "Passwords don't match";
    }
}
