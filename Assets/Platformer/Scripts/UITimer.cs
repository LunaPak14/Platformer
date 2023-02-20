using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    // Update is called once per frame
    void Update()
    {
        int wholeSecond = (int)Mathf.Floor(Time.realtimeSinceStartup);
        timerText.text = "Time \n" + (375 - wholeSecond);
    }
}
