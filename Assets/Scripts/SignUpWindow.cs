using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SignUpWindow : UIWindow
{
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button signUpButton;

    private UnityAction goToLogIn;
    
    private enum incorrectInfoTypes {EmailProblem, PasswordProblem, UsernameTaken, InvalidName}
    
    private void Start()
    {
        incorrectFields = new List<GameObject>();
        inputFields = new List<TMP_InputField> {emailInput, passwordInput, usernameInput, nameInput};
        
        if(storageManager == null)
            storageManager = new StorageManager();
        
        signUpButton.onClick.AddListener(OnSignUpButtonCall);
        closeButton.onClick.AddListener(goToLogIn);

        userDatabase = (Dictionary<String,User>) storageManager.LoadData(DATABASE_NAME) ?? new Dictionary<string, User>();
    }
    
    public void GoToLogInWindow(UnityAction returnToLogIn)
    {
        this.goToLogIn = returnToLogIn;
    }
    
    private void OnSignUpButtonCall()
    {
        if (SignUpFilled() && !userDatabase.ContainsKey(emailInput.text) && !oneFieldMistake)
        {
            SaveSignUpInformation();
            goToLogIn.Invoke();
        }
    }
        
    private bool SignUpFilled()
    {
        if (passwordInput.text.Length < 8 ) MissingInformation(incorrectInfoTypes.PasswordProblem);
        
        if(nameInput.text.Length == 0) MissingInformation(incorrectInfoTypes.InvalidName);
        
        try
        {
            MailAddress m = new MailAddress(emailInput.text);
        }
        catch
        {
            MissingInformation(incorrectInfoTypes.EmailProblem);
        }

        if (userDatabase.Any(user => user.Value.username == usernameInput.text) || usernameInput.text.Length == 0)
        {
            MissingInformation(incorrectInfoTypes.UsernameTaken);
        }
        
        return !oneFieldMistake;
    }

    private void SaveSignUpInformation()
    {
        userDatabase.Add(emailInput.text, new User(emailInput.text, usernameInput.text, passwordInput.text, nameInput.text));
        
        storageManager.SaveData(userDatabase, DATABASE_NAME);
    }
    
    private void MissingInformation(incorrectInfoTypes type)
    {
        oneFieldMistake = true;

        if (type == incorrectInfoTypes.EmailProblem)
        {
            incorrectFields.Add(emailInput.gameObject);
            emailInput.text = "Email must be valid";
            ColorInputField(MY_RED, MY_RED, emailInput);
        }
        else if (type == incorrectInfoTypes.InvalidName)
        {
            incorrectFields.Add(nameInput.gameObject);
            nameInput.text = "Full Name";
            ColorInputField(MY_RED, MY_RED, nameInput);
        }
        else if (type == incorrectInfoTypes.PasswordProblem)
        {
            incorrectFields.Add(passwordInput.gameObject);
            passwordInput.contentType = TMP_InputField.ContentType.Standard;
            passwordInput.text = "Password must include 8 characters";
            ColorInputField(MY_RED, MY_RED, passwordInput);
        }
        else if (type == incorrectInfoTypes.UsernameTaken)
        {
            incorrectFields.Add(usernameInput.gameObject);
            usernameInput.text = "Username is already taken";
            ColorInputField(MY_RED, MY_RED, usernameInput);
        }
    }
    
    private void Update()
    {
        if (oneFieldMistake && incorrectFields.Contains(EventSystem.current.currentSelectedGameObject))
        {
            foreach (var field in incorrectFields)
            {
                if (field != EventSystem.current.currentSelectedGameObject) continue;
                    
                TMP_InputField inputField = field.GetComponent<TMP_InputField>();
                    
                if(field == passwordInput.gameObject) 
                    passwordInput.contentType = TMP_InputField.ContentType.Password;
                    
                incorrectFields.Remove(field);
                    
                if (incorrectFields.Count == 0) 
                    oneFieldMistake = false;
                    
                ColorInputField(Color.white, new Color(0.19f, 0.19f, 0.19f), inputField);
                    
                inputField.text = "";
                    
                return;
            }
        }
    }
}
