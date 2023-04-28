using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Goal : MonoBehaviour
{
    private string titleName = "";
    private bool isCompleted = false;
    private bool isReadyToComplete = false;
    private int step = 0;

    public TMP_Text titleText;
    public GameObject completeButton;
    public Toggle completedToggle;
    public WorkoutUI workoutUI;
    public ExerciseItem exerciseItem;

    public void SetUp(string name, bool isReady, bool complete, int stepNum)
    {
        titleText.text = name.Replace("_"," ");
        step = stepNum;
        isCompleted = complete;
        if (complete)
        {
            completeButton.SetActive(false);
            completedToggle.isOn = true;
        }
        else
        {
            if (isReady)
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
        isCompleted = true;
        //workoutUI.RefreshMorePage();
    }
}
