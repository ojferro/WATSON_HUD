using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Text;

public class InterfaceControl : MonoBehaviour
{
    public Text timeDisplay;
    public Text audioInText;
    public static string data = null;

    void Start(){}

    void Update()
    {
        String time_hr = DateTime.Now.ToString("HH:mm");
        timeDisplay.text = time_hr;
        audioInText.text = RosSharp.RosBridgeClient.UserTextInSubscriber.text;
    }
}
