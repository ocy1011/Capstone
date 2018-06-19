using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : MonoBehaviour {

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

    public SubPanel search;
    public SubPanel category;
    public SubPanel user;
    public SubPanel etc;
    

	// Use this for initialization
	void Start () {
        OffAllPanels();
        search.OnPanel();
    }
	
    void OffAllPanels()
    {
        search.OffPanel();
        category.OffPanel();
        user.OffPanel();
        etc.OffPanel();
    }

    public void OnSearchPanel()
    {
        OffAllPanels();
        search.OnPanel();
        search.panel.GetComponent<SearchPanel>().OnBarcodePanel();
    }

    public void OnCategoryPanel()
    {
        OffAllPanels();
        category.OnPanel();
        Panels.instance.barcodePanel.Stop();
    }

    public void OnUserPanel()
    {

        OffAllPanels();
        user.OnPanel();
        Panels.instance.userPanel.CheckLog();
        Panels.instance.barcodePanel.Stop();
    }

    public void OnEtcPanel()
    {
        OffAllPanels();
        etc.OnPanel();
        Panels.instance.barcodePanel.Stop();
    }

    



}
