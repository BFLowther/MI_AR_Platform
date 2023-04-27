using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExerciseItem : MonoBehaviour
{
    public string ExerciseName;
    public string infoString;
    public List<GoalSetup> GoalSetups = new List<GoalSetup>();

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

    [HideInInspector]
    public bool isUnlocked = false;
    [HideInInspector]
    public bool isDone = false;

    private void Start()
    {
        titleText.text = ExerciseName;
    }

    public void Unlock(WorkoutUI workoutUIRef)
    {
        workoutUI = workoutUIRef;
        GameObject gameObject;
        goalsDoneCount = 0;
        for (int i = 0; i < GoalSetups.Count; i++)
        {
            gameObject = Instantiate(subtaskToggles);
            gameObject.SetActive(false);
            gameObject.transform.parent = subtaskParent.transform;

            if (GoalSetups[i].isCompleted)
            {
                gameObject.GetComponent<Toggle>().isOn = true;
                goalsDoneCount++;
            }

            gameObject.SetActive(true);
            subTaskGOs.Add(gameObject);

        }
        isUnlocked = true;

        if(goalsDoneCount  == GoalSetups.Count)
        {
            SetDoneToggle();
        }
    }

    public void SetUp(WorkoutUI workoutUIRef)
    {
        workoutUI = workoutUIRef;
        GameObject gameObject;
        goalsDoneCount = 0;
        for (int i = 0; i < GoalSetups.Count; i++)
        {
            gameObject = Instantiate(subtaskToggles);
            gameObject.SetActive(false);
            gameObject.transform.parent = subtaskParent.transform;

            if (GoalSetups[i].isCompleted)
            {
                gameObject.GetComponent<Toggle>().isOn = true;
                goalsDoneCount++;
            }

            gameObject.SetActive(true);
            subTaskGOs.Add(gameObject);

        }

        if(goalsDoneCount  == GoalSetups.Count)
        {
            SetDoneToggle();
        }
    }

    public void Refresh()
    {
        if(!isDone)
        {
            goalsDoneCount = 0;
            for(int i =0; i<subTaskGOs.Count; i++)
            {
            
                if (GoalSetups[i].isCompleted)
                {
                    subTaskGOs[i].GetComponent<Toggle>().isOn = true;
                    goalsDoneCount++;
                }
            }
            if(goalsDoneCount  == GoalSetups.Count)
            {
                SetDoneToggle();
            }
        }
    }
    public void SetDoneToggle()
    {
        isDone = true;
        isDoneToggle.isOn = true;
    }

    public void MoreInfo()
    {
        if (isUnlocked)
        {
            workoutUI.MoreInfoPage(ExerciseName, infoString, this);
        }
    }


}
