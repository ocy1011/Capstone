using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TBEasyWebCam;

public class BarcodePanel : MonoBehaviour
{
    public QRCodeDecodeController e_qrController;

    public Text UiText;

    public GameObject resetBtn;

    public GameObject scanLineObj;
    #if UNITY_ANDROID && !UNITY_EDITOR
	bool isTorchOn = false;
    #endif
    public Sprite torchOnSprite;
    public Sprite torchOffSprite;
    public Image torchImage;
    public GameObject isNullPanel;
    /// <summary>
    /// when you set the var is true,if the result of the decode is web url,it will open with browser.
    /// </summary>
    public bool isOpenBrowserIfUrl;

    void Start()
    {
        if (this.e_qrController != null)
        {
            this.e_qrController.onQRScanFinished += new QRCodeDecodeController.QRScanFinished(this.qrScanFinished);
        }
        Play();
    }

    public void PanelOn()
    {
        if (this.e_qrController != null)
        {
            this.e_qrController.onQRScanFinished += new QRCodeDecodeController.QRScanFinished(this.qrScanFinished);
        }
        Play();
    }

    public void PanelOff()
    {
        Stop();
    }

    private void qrScanFinished(string dataText)
    {
        if (isOpenBrowserIfUrl)
        {
            if (Utility.CheckIsUrlFormat(dataText))
            {
                if (!dataText.Contains("http://") && !dataText.Contains("https://"))
                {
                    dataText = "http://" + dataText;
                }
                Application.OpenURL(dataText);
            }
        }
        if (this.UiText != null)
        {
            this.UiText.text = dataText;
        }
        if (this.resetBtn != null)
        {
            this.resetBtn.SetActive(true);
        }
        if (this.scanLineObj != null)
        {
            this.scanLineObj.SetActive(false);
        }
        if(dataText.Length==13)
        {
            string sql = "SELECT * FROM product WHERE barcode='" + dataText + "';";
            MySql.Data.MySqlClient.MySqlDataReader reader = DbConnecter.instance.Reader(sql);
            if (reader.Read())
            {
                Product product = new Product();
                product.id = reader.GetInt32(0);
                product.name = reader.GetString(1);
                product.barcode = reader.GetString(2);
                product.company = reader.GetString(3);
                product.category = product.koreanToCategory(reader.GetString(4));
                product.ingredients = reader.GetString(5);
                product.imagePath = reader.GetString(6);

                reader.Close();
                DbConnecter.instance.CloseConnection();
                Stop();
                Panels.instance.productInfoPanel.gameObject.SetActive(true);
                Panels.instance.productInfoPanel.PanelOn(product, Panels.instance.homePanel.gameObject);

            }
            else if (this.isNullPanel != null)
            {
                isNullPanel.SetActive(true);
                DbConnecter.instance.CloseConnection();
            }
        }
        else if (this.isNullPanel != null)
        {
            isNullPanel.SetActive(true);
        }
    }

    public void Reset()
    {
        if (this.e_qrController != null)
        {
            this.e_qrController.Reset();
        }
        if (this.UiText != null)
        {
            this.UiText.text = string.Empty;
        }
        if (this.resetBtn != null)
        {
            this.resetBtn.SetActive(false);
        }
        if (this.scanLineObj != null)
        {
            this.scanLineObj.SetActive(true);
        }
        if(this.isNullPanel!=null)
            isNullPanel.SetActive(false);
    }

    public void Play()
    {
        Reset();
        if (this.e_qrController != null)
        {
            this.e_qrController.StartWork();
        }
    }

    public void Stop()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
		        if (EasyWebCam.isActive) {
			        if (isTorchOn) {
				        torchImage.sprite = torchOffSprite;
				        EasyWebCam.setTorchMode (TBEasyWebCam.Setting.TorchMode.Off);
                        isTorchOn = !isTorchOn;
			        } 
		        }
        #endif
        if (this.e_qrController != null)
        {
            this.e_qrController.StopWork();
        }

        if (this.resetBtn != null)
        {
            this.resetBtn.SetActive(false);
        }
        if (this.scanLineObj != null)
        {
            this.scanLineObj.SetActive(false);
        }
    }

    public void GotoNextScene(string scenename)
    {
        if (this.e_qrController != null)
        {
            this.e_qrController.StopWork();
        }
        //Application.LoadLevel(scenename);
        SceneManager.LoadScene(scenename);
    }

    /// <summary>
    /// Toggles the torch by click the ui button
    /// note: support the feature by using the EasyWebCam Component 
    /// </summary>
    public void toggleTorch()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
		if (EasyWebCam.isActive) {
			if (isTorchOn) {
				torchImage.sprite = torchOffSprite;
				EasyWebCam.setTorchMode (TBEasyWebCam.Setting.TorchMode.Off);
			} else {
				torchImage.sprite = torchOnSprite;
				EasyWebCam.setTorchMode (TBEasyWebCam.Setting.TorchMode.On);
			}
			isTorchOn = !isTorchOn;
		}
#endif
    }

}
