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


    private void Start()
    {
        titleText.text = exerciseConfig.ExerciseName;
    }

    public void Unlock(WorkoutUI workoutUIRef)
    {
        workoutUI = workoutUIRef;
        GameObject gameObject;
        for (int i = 0; i < exerciseConfig.GoalSetups.Count; i++)
        {
            gameObject = Instantiate(subtaskToggles);
            gameObject.SetActive(false);
            gameObject.transform.parent = subtaskParent.transform;

            if (exerciseConfig.GoalSetups[i].isCompleted)
            {
                gameObject.GetComponent<Toggle>().isOn = true;
            }

            gameObject.SetActive(true);

        }
        exerciseConfig.isUnlocked = true;
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
