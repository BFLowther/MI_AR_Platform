using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExerciseItem : MonoBehaviour
{
    
    public string ExerciseName;
    public string infoString;

    [SerializeField]
    private Toggle isDoneToggle;

    [SerializeField]
    private TMP_Text titleText;


    private void Start()
    {
        titleText.text = ExerciseName;
    }

    public void SetToggle()
    {
        isDoneToggle.isOn = true;
    }


}
