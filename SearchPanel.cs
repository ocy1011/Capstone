using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPanel : MonoBehaviour {

    [System.Serializable]
    public class SubPanel
    {
        public Transform panel;
        public Transform button;
        bool on = false;

        public void OnPanel()
        {
            on = true;
            panel.gameObject.SetActive(on);
            Transform buttonHead = button.GetChild(0);
            buttonHead.gameObject.SetActive(on);
        }

        public void OffPanel()
        {
            on = false;
            panel.gameObject.SetActive(on);
            Transform buttonHead = button.GetChild(0);
            buttonHead.gameObject.SetActive(on);
        }
    }

    public Transform productSearchPanel;
    public BarcodePanel barcodePanel;
    public Transform homePanel;
    public SubPanel keyword;
    public SubPanel barcode;

    // Use this for initialization
    void Start () {
        //OnKeywordPanel();
    }

    void OffAllPanels()
    {
        keyword.OffPanel();
        barcode.OffPanel();
    }

    public void OnKeywordPanel()
    {
        OffAllPanels();
        keyword.OnPanel();
        Panels.instance.barcodePanel.Stop();
    }

    public void OnBarcodePanel()
    {
        OffAllPanels();
        barcode.OnPanel();
        Panels.instance.barcodePanel.PanelOn();
    }

    public void OnProductSearchPanel()
    {
        productSearchPanel.gameObject.SetActive(true);
        homePanel.gameObject.SetActive(false);
        Panels.instance.productSearchPanel.PanelOn(homePanel.gameObject);
    }
}
