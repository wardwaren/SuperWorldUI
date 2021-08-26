using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LogInSuccessWindow : UIWindow
{
    [SerializeField] private TextMeshProUGUI welcomeText;
    [SerializeField] private Button logoutButton;
    
    public User user {private get; set;}

    private void Start()
    {
        if(storageManager == null)
            storageManager = new StorageManager();
        welcomeText.text += user.username;
    }

    public void GoToLogInWindow(UnityAction logout)
    {
        logoutButton.onClick.AddListener(logout);
    }
}
