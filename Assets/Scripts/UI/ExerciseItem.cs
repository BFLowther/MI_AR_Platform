using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExerciseItem : MonoBehaviour
{
    public ExerciseConfig exerciseConfig;

    [SerializeField]
    private Toggle isDoneToggle;

    [SerializeField]
    private TMP_Text titleText;

    [SerializeField]
    private GameObject subtaskParent;

    [SerializeField]
    private GameObject subtaskToggles;

    private WorkoutUI workoutUI;

    private List<GameObject> subTaskGOs = new List<GameObject>();

    private int goalsDoneCount = 0;


    private void Start()
    {
        titleText.text = exerciseConfig.ExerciseName;
    }

    public void Unlock(WorkoutUI workoutUIRef)
    {
        workoutUI = workoutUIRef;
        GameObject gameObject;
        goalsDoneCount = 0;
        for (int i = 0; i < exerciseConfig.GoalSetups.Count; i++)
        {
            gameObject = Instantiate(subtaskToggles);
            gameObject.SetActive(false);
            gameObject.transform.parent = subtaskParent.transform;

            if (exerciseConfig.GoalSetups[i].isCompleted)
            {
                gameObject.GetComponent<Toggle>().isOn = true;
                goalsDoneCount++;
            }

            gameObject.SetActive(true);
            subTaskGOs.Add(gameObject);

        }
        exerciseConfig.isUnlocked = true;

        if(goalsDoneCount  == exerciseConfig.GoalSetups.Count)
        {
            SetDoneToggle();
        }
    }

    public void SetUp(WorkoutUI workoutUIRef)
    {
        workoutUI = workoutUIRef;
        GameObject gameObject;
        goalsDoneCount = 0;
        for (int i = 0; i < exerciseConfig.GoalSetups.Count; i++)
        {
            gameObject = Instantiate(subtaskToggles);
            gameObject.SetActive(false);
            gameObject.transform.parent = subtaskParent.transform;

            if (exerciseConfig.GoalSetups[i].isCompleted)
            {
                gameObject.GetComponent<Toggle>().isOn = true;
                goalsDoneCount++;
            }

            gameObject.SetActive(true);
            subTaskGOs.Add(gameObject);

        }

        if(goalsDoneCount  == exerciseConfig.GoalSetups.Count)
        {
            SetDoneToggle();
        }
    }

    public void Refresh()
    {
        if(!exerciseConfig.isDone)
        {
            goalsDoneCount = 0;
            for(int i =0; i<subTaskGOs.Count; i++)
            {
            
                if (exerciseConfig.GoalSetups[i].isCompleted)
                {
                    subTaskGOs[i].GetComponent<Toggle>().isOn = true;
                    goalsDoneCount++;
                }
            }
            if(goalsDoneCount  == exerciseConfig.GoalSetups.Count)
            {
                SetDoneToggle();
            }
        }
    }
    public void SetDoneToggle()
    {
        exerciseConfig.isDone = true;
        isDoneToggle.isOn = true;
    }

    public void MoreInfo()
    {
        if (exerciseConfig.isUnlocked)
        {
            workoutUI.MoreInfoPage(exerciseConfig.ExerciseName, exerciseConfig.infoString, this);
        }
    }


}
