using UnityEngine;
using System.Collections;
//using JZWLEngine.Managers;

public class UIPrompt : MonoBehaviour {

    public string eventName;
    public UILabel label;
    private int promptNum;

    void Start()
    {
        updataLabel(label.text);
	}

    private void _OnEventHandler(params object[] args)
    {
        updataLabel(label.text);
    }

    private void updataLabel(string text)
    {
        if (label.text == "")
        {
            gameObject.SetActive(false);
            //EventManager.instance.RemoveEventListener(EventManager.instance, eventName, _OnEventHandler);
        }
        else
        {
            gameObject.SetActive(true);
            //EventManager.instance.AddEventListener(EventManager.instance, eventName, _OnEventHandler);
        }
    }

    void OnClick()
    {
 
    }

    public int PromptNum
    {
        set { promptNum = value; }
        get { return promptNum; }
    }
   
}
