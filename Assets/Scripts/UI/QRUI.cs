using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class QRUI : MonoBehaviour
{
    [Header("Scanned Pop")]
    public GameObject scannedPopUpGO;
    public TMP_Text whatYouScannedText;

    private float timer = 0;


    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = 0;
                ExitPopUp();
            }
        }

    }

    public void Exit()
    {
        SceneManager.LoadScene("Workout");
    }

    public void ScannedPopUp(string QRCode)
    {
        scannedPopUpGO.SetActive(true);
        whatYouScannedText.text = QRCode;
        timer = 10f;
    }

    private void ExitPopUp()
    {
        scannedPopUpGO.SetActive(false);
        SceneManager.LoadScene("Workout");
    }

}
