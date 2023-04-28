using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public LoginHelper loginHelper;

    private void Start()
    {
        passwordInput.contentType = TMP_InputField.ContentType.Password;
    }

    public void SignUp()
    {
        loginHelper.SignUp(usernameInput.text, passwordInput.text);
    }
    public void SignIn()
    {
        loginHelper.SignIn(usernameInput.text, passwordInput.text);
    }

    public void LoadMapScene()
    {
        SceneManager.LoadScene("Map");
    }
}
