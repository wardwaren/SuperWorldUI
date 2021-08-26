using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LogInWindow : UIWindow
{
    [SerializeField] private LogInSuccessWindow logInSuccessPanel;
    
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button forgotPasswordButton;
    [SerializeField] private Button signUpButton;
    [SerializeField] private Button signInButton;
    [SerializeField] private Button appleSignInButton;
    [SerializeField] private GameObject signInButtonsPanel;
    
    private UnityAction signIn;
    
    private void Start()
    {
        inputFields = new List<TMP_InputField> {emailInput, passwordInput};

        if(storageManager == null)
            storageManager = new StorageManager();
        
        signInButton.onClick.AddListener(SignIn);

        SignInDifferentPlatforms();
        
        userDatabase = (Dictionary<String,User>) storageManager.LoadData(DATABASE_NAME) ?? new Dictionary<string, User>();
    }

    public void GoToLogInSuccessWindow(UnityAction signIn)
    {
        this.signIn = signIn;
    }
    
    public void GoToSignUpWindow(UnityAction signUp)
    {
        signUpButton.onClick.AddListener(signUp);
    }
    
    public void GoToForgotPasswordWindow(UnityAction forgotPassword)
    {
        forgotPasswordButton.onClick.AddListener(forgotPassword);
    }

    private void SignInDifferentPlatforms()
    {
        #if UNITY_ANDROID
            signInButtonsPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 
                signInButtonsPanel.GetComponent<RectTransform>().sizeDelta.y * 2/3);
            appleSignInButton.gameObject.SetActive(false);
        #endif
    }
    
    private void SignIn()
    {
        if (CheckInfo())
        {
            logInSuccessPanel.user = userDatabase[emailInput.text];
            signIn.Invoke();
        }
        else
        {
            OnIncorrectInformation();
        }
    }

    private bool CheckInfo()
    {
        if (userDatabase.ContainsKey(emailInput.text))
        {
            if (userDatabase[emailInput.text].password == passwordInput.text)
            {
                return true;
            }
        }

        return false;
    }

    private void OnIncorrectInformation()
    {
        mistake = true;
        ColorInputFields(MY_RED, MY_RED);

        passwordInput.contentType = TMP_InputField.ContentType.Standard;
        passwordInput.text = "Incorrect password";
        emailInput.text = "Email not valid";
    }
    
    private void Update()
    {
        if (mistake && EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>() == null) return;

            passwordInput.contentType = TMP_InputField.ContentType.Password;
            mistake = false;
            ColorInputFields(Color.white, new Color(0.19f, 0.19f, 0.19f));
            CleanInputFields();
        }
    }
}
