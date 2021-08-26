using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ForgotPasswordWindow : UIWindow
{
    [SerializeField] private ResetPasswordWindow resetPasswordPanel;
    
    [SerializeField] private Button closeButton;
    [SerializeField] private Button sendInfoButton;
    [SerializeField] private TMP_InputField emailOrUsernameInput;
    
    private UnityAction resetPassword;

    private void Start()
    {
        inputFields = new List<TMP_InputField> {emailOrUsernameInput};

        storageManager = new StorageManager();
        
        sendInfoButton.onClick.AddListener(SendResetInfo);
        
        userDatabase = (Dictionary<String,User>) storageManager.LoadData(DATABASE_NAME) ?? new Dictionary<string, User>();
    }

    public void GoToLogInWindow(UnityAction returnToLogIn)
    {
        closeButton.onClick.AddListener(returnToLogIn);
    }

    public void GoToResetPasswordWindow(UnityAction resetPassword)
    {
        this.resetPassword = resetPassword;
    }

    private void SendResetInfo()
    {
        if(UserExists())
            resetPassword.Invoke();
        else
        {
            NoUserFound();
        }
    }
    
    private bool UserExists()
    {
        foreach (var user in userDatabase)
        {
            if (emailOrUsernameInput.text == user.Value.username ||
                emailOrUsernameInput.text == user.Value.email)
            {
                resetPasswordPanel.user = user.Value;
                return true;
            }
        }

        return false;
    }

    private void NoUserFound()
    {
        mistake = true;
        ColorInputFields(MY_RED, MY_RED);

        emailOrUsernameInput.text = "User does not exist";
    }
}
