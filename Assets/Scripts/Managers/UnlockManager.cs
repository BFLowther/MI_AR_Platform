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

    public void ConfirmUnlock()
    {
        todaysUnlocks.Add(tryingToUnlock);
        tryingToUnlock = "";
    }
}
