using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleInfo : MonoBehaviour, IPointerClickHandler {

    public ToggleGroupInfo toggleGroup;
    public bool isOn = false;
    public int id;

    void Start()
    {
        Check();
        toggleGroup.AddToggle(this);
    }

	public void OnPointerClick(PointerEventData data)
    {
        toggleGroup.SelectToggle(id);
    }

    public void Check()
    {
        Image checkMark = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        if(isOn==false)
            checkMark.enabled = false;
        else
            checkMark.enabled = true;
    }
}
