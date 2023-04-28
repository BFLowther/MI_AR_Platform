using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID || UNITY_IOS
using Firebase;
using Firebase.Auth;
using Firebase.Analytics;
using Firebase.Firestore;
using Firebase.Extensions;
#endif


public class LoginHelper : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent userSignedIn;
    public UnityEngine.Events.UnityEvent userSignedOut;

    #if UNITY_ANDROID || UNITY_IOS
    
    FirebaseUser m_user;
    FirebaseAuth m_auth;
    FirebaseFirestore m_database;

    void Start()
    {
        m_database = FirebaseFirestore.DefaultInstance;
        //SignIn("yourgeekeric@gmail.com", "password12345!");
    }

    void OnDestroy()
    {
        if (m_auth != null)
            m_auth.StateChanged -= AuthStateChanged;
        
        m_auth = null;
    }

    void InitializeFirebaseAuth()
    {
        Debug.Log("Initialize Firebase Auth");
        m_auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        m_auth.StateChanged += AuthStateChanged;
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (m_auth.CurrentUser != m_user)
        {
            bool signedIn = m_user != m_auth.CurrentUser && m_auth.CurrentUser != null;

            if (!signedIn && m_user != null)
            {
                Debug.Log("Signed out " + m_user.UserId);
                userSignedOut.Invoke();
            }

            m_user = m_auth.CurrentUser;

            if (signedIn)
            {
                Debug.Log("Signed in " + m_user.UserId);
                userSignedIn.Invoke();
            }
        }
        else
        {

        }
    }

    void CheckUser()
    {
        /*if (FirebaseManager.Instance.MyUser.MyFirebaseUser != null)
        {
            m_auth = FirebaseManager.Instance.MyUser.MyFirebaseAuth;
            m_user = FirebaseManager.Instance.MyUser.MyFirebaseUser;
            Debug.Log("User already signed in: " + m_user.UserId);
            userSignedIn.Invoke();
            return;
        }*/
        if (m_auth == null)
            InitializeFirebaseAuth();
    }

    public void SignIn(string _email, string _password)
    {
        CheckUser();

        m_auth.SignInWithEmailAndPasswordAsync(_email, _password).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    public void SignUp(string _email, string _password)
    {
        CheckUser();

        m_auth.CreateUserWithEmailAndPasswordAsync(_email, _password).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }
    #endif
}
