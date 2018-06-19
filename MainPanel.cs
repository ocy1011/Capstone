using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour {

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
        OnSearchPanel();
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
    }

    public void OnCategoryPanel()
    {
        OffAllPanels();
        category.OnPanel();
    }

    public void OnUserPanel()
    {

        OffAllPanels();
        user.OnPanel();
    }

    public void OnEtcPanel()
    {
        OffAllPanels();
        etc.OnPanel();
    }
}
