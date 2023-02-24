using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WorkoutUI : MonoBehaviour
{
    public List<ExerciseItem> exerciseItems = new List<ExerciseItem>();

    [Header("More Info Page")]
    public GameObject moreInfoPageGO;
    public TMP_Text titleText;
    public TMP_Text infoText;
    private ExerciseItem curItem;
    

    private void Start()
    {
        if (UnlockManager.Instance.todaysUnlocks.Count > 0)
        {
            SetUp();
        }


    }

    private void SetUp()
    {
        for(int i=0; i<exerciseItems.Count; i++)
        {
            if(UnlockManager.Instance.tryingToUnlock == exerciseItems[i].ExerciseName)
            {
                MoreInfoPage(exerciseItems[i].ExerciseName, exerciseItems[i].infoString, exerciseItems[i]);
            }

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

    public void MoreInfoPage(string title, string info, ExerciseItem item)
    {

        titleText.text = title;
        infoText.text = info;
        curItem = item;
        moreInfoPageGO.SetActive(true);
    }

    public void Complete()
    {
        UnlockManager.Instance.ConfirmUnlock();
        curItem.SetToggle();
    }
}
