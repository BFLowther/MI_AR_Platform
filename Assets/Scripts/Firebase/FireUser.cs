using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_ANDROID || UNITY_IOS
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
#endif

public class FireUser : MonoBehaviour
{
    #if UNITY_ANDROID || UNITY_IOS
    public static FireUser instance { get; private set; }
    public List<string> unlocks = new List<string>();
    public List<Exercise> exercises = new List<Exercise>();
    private FirebaseFirestore m_database;
    private FirebaseUser m_firebaseUser;
    private FirebaseAuth m_firebaseAuth;

    System.DateTime timeStamp;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_firebaseAuth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        m_database = FirebaseFirestore.DefaultInstance;
        m_firebaseAuth.StateChanged += AuthStateChanged;
    }

    /// <summary>
    /// Fires when the FirebaseAuth StateChaged event goes off.
    /// Then send the SignedOut or SignedIn event.
    /// </summary>
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        Debug.Log("Auth Changed");
        if (m_firebaseAuth.CurrentUser != m_firebaseUser)
        {
            bool signedIn = m_firebaseUser != m_firebaseAuth.CurrentUser && m_firebaseAuth.CurrentUser != null;
            if (!signedIn && m_firebaseUser != null)
            {

            }
            m_firebaseUser = m_firebaseAuth.CurrentUser;
            if (signedIn)
            {
                timeStamp = System.DateTime.UtcNow;
                UpdateUser();
            }
        }
    }

    public void UpdateUser()
    {
        GetUserData();
    }

    public void GetUserData()
    {
        Debug.Log("Get Data");
        DocumentReference userRef = m_database.Collection("users").Document(m_firebaseUser.UserId);
        userRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result.Exists)
            {
                DocumentSnapshot documentSnapshot = task.Result;
                Dictionary<string, object> documentDictionary = documentSnapshot.ToDictionary();

                Debug.Log(string.Format("User: {0}", documentSnapshot.Id));

                /*foreach(string key in documentDictionary.Keys)
                {
                    Debug.Log(string.Format("{0}: {1}", key,documentDictionary[key]));
                }*/
                //Debug.Log("Before");
                if (documentDictionary.ContainsKey("completed"))
                {
                    //Debug.Log("completed: " + (((IEnumerable<object>)documentDictionary["completed"]).Cast<List<string>>().ToList())[0]);
                    //Debug.Log("completed: " + ((documentDictionary["completed"] as List<object>)[0] as string));
                    //unlocks = new List<string>((List<string>)documentDictionary["completed"]);
                    unlocks.Clear();
                    for(int i = 0; i < (documentDictionary["completed"] as List<object>).Count; i++)
                    {
                        unlocks.Add(((documentDictionary["completed"] as List<object>)[i] as string));
                    }
                }
                
                foreach(string s in unlocks)
                    Debug.Log("completed: " + s);
            }
            else
            {
                Debug.Log("Set Default Data");
                SetDefaultUserData();
            }
        });
    }

    private void SetDefaultUserData()
    {
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "last_login", timeStamp },
        };

        SetUserData(user);
    }

    public void SaveData()
    {
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "completed", unlocks },
            { "last_login", timeStamp },
        };

        SetUserData(user);
    }

    private void SetUserData(Dictionary<string, object> newUserData)
    {
        Debug.Log("Set Data");
        DocumentReference docRef = m_database.Collection("users").Document(m_firebaseUser.UserId);
        docRef.SetAsync(newUserData).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Added data to the " + m_firebaseUser.UserId + " document in the users collection.");
        });
    }
    #endif
}
