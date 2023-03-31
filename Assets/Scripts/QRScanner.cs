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
    private float countDown = 0.0f;

    public QRUI QRUI;

    void Start()
    {
        var renderer = GetComponent<RawImage>();
        int max = Mathf.Max(Screen.width, Screen.height);
        var rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(max,max);

        webcamTexture = new WebCamTexture((int)(max/2.0f), (int)(max/2.0f), 24);
        webcamTexture.deviceName = WebCamTexture.devices[PlayerPrefs.GetInt("default_camera", 0)].name;

        Debug.Log(webcamTexture.deviceName);
        Debug.Log("Number of cameras: " + WebCamTexture.devices.Length);
        renderer.texture = webcamTexture;
        webcamTexture.filterMode = FilterMode.Trilinear;
        //renderer.material.mainTexture = webcamTexture;
        StartCoroutine(GetQRCode());
    }

    void Update()
    {
        countDown -= Time.deltaTime;
    }

    IEnumerator GetQRCode()
    {
        IBarcodeReader barCodeReader = new BarcodeReader();
        webcamTexture.Play();
        ZXing.Result result;
        var snap = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGB24, false);
        while (string.IsNullOrEmpty(QrCode))
        {
            try
            {
                snap.SetPixels32(webcamTexture.GetPixels32());
                if (countDown < 0.0f)
                {
                    countDown = delay;
                    GetComponent<RectTransform>().rotation = Quaternion.Euler(0.0f,0.0f,-webcamTexture.videoRotationAngle);
                    result = barCodeReader.Decode(snap.GetRawTextureData(), webcamTexture.width, webcamTexture.height, RGBLuminanceSource.BitmapFormat.RGB24);
                    if (result != null)
                    {
                        QrCode = result.Text;
                        if (!string.IsNullOrEmpty(QrCode))
                        {
                            if (UnlockManager.Instance.validUnlocks.Contains(QrCode)) //&&
                                //!UnlockManager.Instance.todaysUnlocks.Contains(QrCode))
                            {
                                //UnlockManager.Instance.todaysUnlocks.Add(QrCode);
                                UnlockManager.Instance.tryingToUnlock = QrCode;
                                Debug.Log("DECODED TEXT FROM QR: " + QrCode);
                                // Melisa add scene change here

                                QRUI.ScannedPopUp(QrCode);
                                ///////////////////////////////
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
            yield return null;//new WaitForSeconds(delay);
        }
        webcamTexture.Stop();
    }

    public void NextCamera()
    {
        int index = PlayerPrefs.GetInt("default_camera", 0);

        index++;

        if (index >= WebCamTexture.devices.Length)
        {
            index = 0;
        }

        Debug.Log("Index : " + index);

        webcamTexture.Stop();
        webcamTexture.deviceName = WebCamTexture.devices[index].name;
        PlayerPrefs.SetInt("default_camera", index);
        Debug.Log(webcamTexture.deviceName);
        webcamTexture.Play();
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
