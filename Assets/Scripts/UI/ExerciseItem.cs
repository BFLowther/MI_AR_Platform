using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExerciseItem : MonoBehaviour
{
    public string ExerciseName;
    public List<string> subtasks = new List<string>();
    public string QRcode;

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


  /*  public void Unlock(WorkoutUI workoutUIRef)
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
  */
    public void SetUp(WorkoutUI workoutUIRef, string name, string code, List<string> tasks, bool unlocked)
    {
        workoutUI = workoutUIRef;
        ExerciseName = name.Replace("_", " ");
        QRcode = code;
        subtasks = tasks;

        titleText.text = ExerciseName;
        isUnlocked = unlocked;

        if (unlocked)
        {
            GameObject gameObject;
            goalsDoneCount = 0;
            for (int i = 0; i < subtasks.Count; i++)
            {
                gameObject = Instantiate(subtaskToggles);
                gameObject.SetActive(false);
                gameObject.transform.parent = subtaskParent.transform;

                if (FireUser.instance.unlocks.Contains(name + "." + subtasks[i]))
                {
                    gameObject.GetComponent<Toggle>().isOn = true;
                    goalsDoneCount++;
                }

                gameObject.SetActive(true);
                subTaskGOs.Add(gameObject);
            }
        }

        if (goalsDoneCount == subtasks.Count)
        {
            SetDoneToggle();
        }
    }

   /* public void Refresh()
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
    }*/
    public void SetDoneToggle()
    {
        isDone = true;
        isDoneToggle.isOn = true;
    }

    public void MoreInfo()
    {
        if (isUnlocked)
        {
           workoutUI.MoreInfoPage(ExerciseName, QRcode, this);
        }
    }


}
