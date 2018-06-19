using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ToggleGroupInfo : MonoBehaviour {

    List<ToggleInfo> toggleList = new List<ToggleInfo>();

    public void AddToggle(ToggleInfo toggleInfo)
    {
        toggleList.Add(toggleInfo);
        toggleList.OrderBy(a => a.id);
        SelectToggle(0);
    }

    public void SelectToggle(int id)
    {
        for(int i=0; i<toggleList.Count; i++)
        {
            ToggleInfo nowToggle = toggleList[i];
            if (nowToggle.id == id)
                nowToggle.isOn = true;
            else
                nowToggle.isOn = false;
            nowToggle.Check();
        }
    }

    public int ActiveToggle()
    {
        ToggleInfo toggle = toggleList.Find(a => a.isOn == true);
        if (toggle == null)
        {
            SelectToggle(0);
            return 0;
        }
        else
            return toggleList.Find(a => a.isOn == true).id;
    }
}
