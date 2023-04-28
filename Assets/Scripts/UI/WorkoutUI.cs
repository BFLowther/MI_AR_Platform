using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//using static UnityEditor.Progress;

[System.Serializable]
public class Exercise {
    public string name;
    public List<string> tasks;
    public string qr_code;
}

public class WorkoutUI : MonoBehaviour
{
    public List<ExerciseItem> exerciseItems = new List<ExerciseItem>();
    public List<Exercise> exercises = new List<Exercise>();

    [Header("More Info Page")]
    public GameObject moreInfoPageGO;
    public TMP_Text titleText;
    public TMP_Text infoText;
    public GameObject goalsGO;
    public GameObject goalsParentGO;
    private ExerciseItem curItem;
    

    private void Start()
    {
        SetUp();


    }

    private void SetUp()
    {
        Debug.Log("ttu"+ UnlockManager.Instance.tryingToUnlock);
        
        for(int i=0; i<exerciseItems.Count; i++)
        {
            Debug.Log("Exercise name tttt"+ exerciseItems[i].ExerciseName + UnlockManager.Instance.tryingToUnlock);
            if(UnlockManager.Instance.tryingToUnlock == exerciseItems[i].ExerciseName || exerciseItems[i].isUnlocked)
            {
                Debug.Log("Exercise name"+ exerciseItems[i].ExerciseName + UnlockManager.Instance.tryingToUnlock);
                exerciseItems[i].Unlock(this);
            }
        }
    }

    public void Refresh()
    {
        for(int i=0; i<exerciseItems.Count; i++)
        {
            if(UnlockManager.Instance.tryingToUnlock == exerciseItems[i].ExerciseName || exerciseItems[i].isUnlocked)
            {
                exerciseItems[i].Refresh();
            }
        }
    }

    public void QRScanner()
    {
        UnlockManager.Instance.CancelUnlock();
        SceneManager.LoadScene("QRCodeTest");
    }

    public void MoreInfoPage(string title, string info, ExerciseItem item)
    {
        titleText.text = title;
        infoText.text = info;

        for(int i = 0; i<goalsParentGO.transform.childCount; i++)
        {
            Destroy(goalsParentGO.transform.GetChild(i).gameObject);
        }

        GameObject gameObject;
        for(int i = 0; i<item.GoalSetups.Count; i++)
        {
            gameObject = Instantiate(goalsGO);
            gameObject.SetActive(false);
            gameObject.transform.parent = goalsParentGO.transform;

            gameObject.GetComponent<Goal>().workoutUI = this;

            if (item.GoalSetups[i].isCompleted)
            {
                gameObject.GetComponent<Goal>().exerciseItem = item;
                gameObject.GetComponent<Goal>().SetUp();
            }
            else
            {
                if (i == 0)
                {
                    item.GoalSetups[i].isReadyToComplete = true;
                    gameObject.GetComponent<Goal>().exerciseItem = item;
                    gameObject.GetComponent<Goal>().SetUp();
                }
                else
                {
                    if (item.GoalSetups[i - 1].isCompleted)
                    {
                        item.GoalSetups[i].isReadyToComplete = true;
                        gameObject.GetComponent<Goal>().exerciseItem = item;
                        gameObject.GetComponent<Goal>().SetUp();
                    }
                    else
                    {
                        item.GoalSetups[i].isReadyToComplete = false;
                        gameObject.GetComponent<Goal>().exerciseItem = item;
                        gameObject.GetComponent<Goal>().SetUp();
                    }
                }
            }

            gameObject.SetActive(true);
        }
        
        moreInfoPageGO.SetActive(true);
    }

    public void RefreshMorePage()
    {
        GameObject[] gameObjects = new GameObject[goalsParentGO.transform.childCount];

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i] = goalsParentGO.transform.GetChild(i).gameObject;

            if (gameObjects[i].GetComponent<Goal>().completedToggle.isOn)
            {
                gameObjects[i].GetComponent<Goal>().SetUp();
            }
            else
            {
                if (gameObjects[i - 1].GetComponent<Goal>().completedToggle.isOn)
                {
                    //gameObjects[i].GetComponent<Goal>().goalSetup.isReadyToComplete = true;
                    gameObjects[i].GetComponent<Goal>().SetUp();
                }
                else
                {
                    //gameObjects[i].GetComponent<Goal>().goalSetup.isReadyToComplete = false;
                    gameObjects[i].GetComponent<Goal>().SetUp();
                }
            }
        }
    }

    public void CloseMoreInfoPage()
    {
        Refresh();
        moreInfoPageGO.SetActive(false);
    }

    /*public void Complete()
    { 
        UnlockManager.Instance.ConfirmUnlock();
        curItem.SetToggle();
        moreInfoPageGO.SetActive(false);
    }*/
}
