using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using UnityEngine.SceneManagement;

public class QRScanner : MonoBehaviour
{
    [Range(0.1f,3.0f)]
    public float delay = 0.5f;
    WebCamTexture webcamTexture;
    string QrCode = string.Empty;

    void Start()
    {
        var renderer = GetComponent<RawImage>();
        int max = Mathf.Max(Screen.width, Screen.height);
        var rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(max,max);

        if (PlayerPrefs.GetString("default_camera", "") == "")
            webcamTexture = new WebCamTexture((int)(max/2.0f), (int)(max/2.0f));
        else
            webcamTexture = new WebCamTexture(PlayerPrefs.GetString("default_camera", ""),(int)(max/2.0f), (int)(max/2.0f));

        PlayerPrefs.SetString("default_camera", webcamTexture.deviceName);

        Debug.Log(webcamTexture.deviceName);
        Debug.Log("Number of cameras: " + WebCamTexture.devices.Length);
        renderer.texture = webcamTexture;
        webcamTexture.filterMode = FilterMode.Trilinear;
        //renderer.material.mainTexture = webcamTexture;
        StartCoroutine(GetQRCode());
    }

    IEnumerator GetQRCode()
    {
        IBarcodeReader barCodeReader = new BarcodeReader();
        webcamTexture.Play();
        var snap = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.ARGB32, false);
        while (string.IsNullOrEmpty(QrCode))
        {
            try
            {
                snap.SetPixels32(webcamTexture.GetPixels32());
                var Result = barCodeReader.Decode(snap.GetRawTextureData(), webcamTexture.width, webcamTexture.height, RGBLuminanceSource.BitmapFormat.ARGB32);
                if (Result != null)
                {
                    QrCode = Result.Text;
                    if (!string.IsNullOrEmpty(QrCode))
                    {
                        if (UnlockManager.Instance.validUnlocks.Contains(QrCode)) //&&
                            //!UnlockManager.Instance.todaysUnlocks.Contains(QrCode))
                        {
                            //UnlockManager.Instance.todaysUnlocks.Add(QrCode);
                            UnlockManager.Instance.tryingToUnlock = QrCode;
                            Debug.Log("DECODED TEXT FROM QR: " + QrCode);
                            // Melisa add scene change here

                            SceneManager.LoadScene("Workout");
                            ///////////////////////////////
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
            yield return new WaitForSeconds(0.5f);
        }
        webcamTexture.Stop();
    }

    public void NextCamera()
    {
        int index = 0;
        for(int i = 0; i < WebCamTexture.devices.Length; i++)
        {
            Debug.Log(i + " : " + webcamTexture.deviceName);
            if (WebCamTexture.devices[i].name == webcamTexture.deviceName)
            {
                index = i;
            }
        }

        if (index >= WebCamTexture.devices.Length)
        {
            index = 0;
        }

        webcamTexture.deviceName = WebCamTexture.devices[index].name;
        PlayerPrefs.SetString("default_camera", webcamTexture.deviceName);
        Debug.Log(webcamTexture.deviceName);
    }
    
    private void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 50;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        string text =QrCode;
        GUI.Label(rect, text, style);
    }
}
