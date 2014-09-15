using System;
using System.Collections.Generic;
using UnityEngine;

public class UIConfig : MonoBehaviour
{
    public int battleWidth = 20;
    public Vector2 uiSize = new Vector2(960, 640);
    public int frameRate = 24;
    public string ptUrl = "http://127.0.0.1:8080/protocol";

    private static UIConfig _instance;
    public static UIConfig instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
