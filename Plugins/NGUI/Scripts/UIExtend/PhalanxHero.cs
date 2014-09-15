using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PhalanxHero : MonoBehaviour
{
    public int width = 128;
    public int height = 10;
    public GameObject Parent;

    public GameObject template;

    private List<GameObject> _items = new List<GameObject>();
    public List<GameObject> items { get { return _items; } }
    void Awake()
    {

        if (template != null)
        {
            template.SetActive(false);
        }
        CreateComponent();
    }
    public GameObject Get(int index)
    {
        return _items.Count > index ? _items[index] : null;
    }

	public T Get<T>(int index) where T : Component
	{
		GameObject go = Get(index);
		return go != null ? go.GetComponent<T>() : null;
	}
    private bool siAdd=false;
    private void CreateComponent(int BeginRow = 0, int maxColumns=0,int total=0)
    {
        if (template != null)
        {
            for (int y = BeginRow; y < total; y++)
            {
                for (int x = 0; x < maxColumns; x++)
                {
                    GameObject go = NGUITools.AddChild(Parent, template);
                    go.SetActive(true);
                    
                    if (siAdd)
                    {
                        go.transform.localPosition = new Vector3( 1 * (width * 2), -y * (height * 2), 0f);
                        siAdd = false;
                    }
                    else
                    {
                        go.transform.localPosition = new Vector3(x * (width * 2), -y * (height * 2), 0f);
                    }
                    _items.Add(go);
                } 
            }
        }
    }
    private int maxItemCount=0;
    public void RefreshComponent(int maxItemCount, int maxColumns)
    {
        int i = 0;
        if (_items.Count > maxItemCount)
        {
            if (maxItemCount%maxColumns==0)
            {
                 for (i = _items.Count - 1; i >maxItemCount-1; i--)
                 {
                     GameObject.Destroy(_items[i]);
                     _items.RemoveAt(i);
                 }
            }
            else
            {
                for (i = _items.Count - 1; i > maxItemCount; i--)
                {
                    GameObject.Destroy(_items[i]);
                    _items.RemoveAt(i);
                }
            }
        }
        else if (_items.Count < maxItemCount)
        {
            int Row = _items.Count / maxColumns;
            if (_items.Count % maxColumns != 0)
            {
                siAdd = true;
            }
            CreateComponent(Row, maxColumns, maxItemCount % maxColumns == 0 ? maxItemCount / maxColumns : maxItemCount / maxColumns + 1);
        }
    }
}
