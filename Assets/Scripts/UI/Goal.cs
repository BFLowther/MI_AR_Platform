using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Goal : MonoBehaviour
{
    public GoalSetup goalSetup;
    public TMP_Text titleText;
    public GameObject completeButton;
    public Toggle completedToggle;
    public WorkoutUI workoutUI;
    //public ExerciseConfig ExerciseConfig;

    public void SetUp()
    {
        titleText.text = goalSetup.titleName;
        if (goalSetup.isCompleted)
        {
            completeButton.SetActive(false);
            completedToggle.isOn = true;
        }
        else
        {
            if (goalSetup.isReadyToComplete)
            {
                completeButton.SetActive(true);
            }
        }
        
    }


    public void Complete()
    {
        goalSetup.isCompleted = true;

        workoutUI.RefreshMorePage();
    }
}
