using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshFireData : MonoBehaviour
{
    public void Run()
    {
        FireUser.instance.GetUserData();
    }
}
