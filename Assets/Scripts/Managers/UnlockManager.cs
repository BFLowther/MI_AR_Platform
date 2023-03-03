using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockManager : MonoBehaviour
{
    public static UnlockManager Instance;
    public List<string> todaysUnlocks;
    public List<string> validUnlocks;
    public string tryingToUnlock = "";

    void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Bye");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        validUnlocks.Add("Exercise #1");
        validUnlocks.Add("Exercise #2");
        validUnlocks.Add("Exercise #3");
        validUnlocks.Add("Exercise #4");
        validUnlocks.Add("Exercise #5");
    }

    public void ConfirmUnlock()
    {
        todaysUnlocks.Add(tryingToUnlock);
        tryingToUnlock = "";
    }

    public void CancelUnlock()
    {
        tryingToUnlock = "";
    }
}
