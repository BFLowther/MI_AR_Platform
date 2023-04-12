using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Goal : MonoBehaviour
{
    [HideInInspector]
    public int step = 0;
    public TMP_Text titleText;
    public GameObject completeButton;
    public Toggle completedToggle;
    public WorkoutUI workoutUI;
    public ExerciseItem exerciseItem;

    public void SetUp()
    {
        titleText.text = exerciseItem.GoalSetups[step].titleName;
        if (exerciseItem.GoalSetups[step].isCompleted)
        {
            completeButton.SetActive(false);
            completedToggle.isOn = true;
        }
        else
        {
            if (exerciseItem.GoalSetups[step].isReadyToComplete)
            {
                completeButton.SetActive(true);
            }
            else
            {
                completeButton.SetActive(false);
            }
        }
        
    }
    public void Complete()
    {
        completedToggle.isOn = true;
        completeButton.SetActive(false);
        exerciseItem.GoalSetups[step].isCompleted = true;
        workoutUI.RefreshMorePage();
    }
}
