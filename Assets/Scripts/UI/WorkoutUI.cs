using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorkoutUI : MonoBehaviour
{
    public List<ExerciseItem> exerciseItems = new List<ExerciseItem>();

    private void Start()
    {
        if (UnlockManager.Instance.todaysUnlocks.Count > 0)
        {
            SetExerciseItems();
        }
    }

    private void SetExerciseItems()
    {
        for(int i=0; i<exerciseItems.Count; i++)
        {
            if (UnlockManager.Instance.todaysUnlocks.Contains(exerciseItems[i].ExerciseName))
            {
                exerciseItems[i].SetToggle();
            }
        }
    }

    public void QRScanner()
    {
        SceneManager.LoadScene("QRCodeTest");
    }
}
