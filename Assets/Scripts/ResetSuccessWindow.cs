using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResetSuccessWindow : UIWindow
{
    [SerializeField] private Button signInButton;
    
    public void GoToSignInWindow(UnityAction signInWindow)
    {
        signInButton.onClick.AddListener(signInWindow);
    }
}
