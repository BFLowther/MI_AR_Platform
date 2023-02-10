using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseItem : MonoBehaviour
{
    
    public string ExerciseName;

    [SerializeField]
    private Toggle isDoneToggle;


    public void SetToggle()
    {
        isDoneToggle.isOn = true;
    }


}
