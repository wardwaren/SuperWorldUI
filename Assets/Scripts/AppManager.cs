 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.Events;

 public class AppManager : MonoBehaviour
{
    [SerializeField] private LogInWindow logInPanel;
    [SerializeField] private SignUpWindow signUpPanel;
    [SerializeField] private ForgotPasswordWindow forgotPasswordPanel;
    [SerializeField] private ResetPasswordWindow resetPasswordPanel;
    [SerializeField] private ResetSuccessWindow resetSuccessPanel;
    [SerializeField] private LogInSuccessWindow logInSuccessPanel;
    
    private UIWindow currentWindow;

    private void Start()
    {
        currentWindow = logInPanel;
        SetupWindowTransitions();
    }

    private void SetupWindowTransitions()
    {
        logInPanel.GoToSignUpWindow(() => OnPanelClick(signUpPanel));
        logInPanel.GoToForgotPasswordWindow(() => OnPanelClick(forgotPasswordPanel));
        logInPanel.GoToLogInSuccessWindow(() => OnPanelClick(logInSuccessPanel));
        signUpPanel.GoToLogInWindow(() => OnPanelClick(logInPanel));
        forgotPasswordPanel.GoToLogInWindow(() => OnPanelClick(logInPanel));
        forgotPasswordPanel.GoToResetPasswordWindow(() => OnPanelClick(resetPasswordPanel));
        resetPasswordPanel.GoToForgotWindow(() => OnPanelClick(forgotPasswordPanel));
        resetPasswordPanel.GoToSuccessWindow(() => OnPanelClick(resetSuccessPanel));
        resetSuccessPanel.GoToSignInWindow((() => OnPanelClick(logInPanel)));
        logInSuccessPanel.GoToLogInWindow(() => OnPanelClick(logInPanel));
    }
    
    private void OnPanelClick(UIWindow newWindow)
    {
        SwitchPanel(currentWindow, newWindow);
        currentWindow = newWindow;
    }
    
    private void SwitchPanel(UIWindow panelOne, UIWindow panelTwo)
    {
        panelOne.ShowWindow(false);
        panelTwo.ShowWindow(true);
    }
}
