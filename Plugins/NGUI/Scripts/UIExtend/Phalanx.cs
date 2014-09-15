using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Phalanx : MonoBehaviour
{
    public int maxItemCount = 16;
    public int maxRows = 4;
    public int maxColumns = 4;
    public bool useUIIndex = false;

    public GameObject template;
    public bool temlateIsPrefab = true;
    private List<GameObject> _items = new List<GameObject>();
    public List<GameObject> items { get { return _items; } }
	public GameObject Get(int index)
	{
		return _items.Count > index ? _items[index] : null;
	}

	public T Get<T>(int index) where T : Component
	{
		GameObject go = Get(index);
		return go != null ? go.GetComponent<T>() : null;
	}

    public int spacing = 128;

    public int padding = 10;

    void Awake()
    {
        if (!temlateIsPrefab)
        {
			if(template != null){
				template.SetActive(false);
			}   
        }
        if (items.Count == 0)
        {
            CreateComponent();            
        }
    }

    public void Reset(int maxRows, int maxColumns, int maxItemCount)
    {
        this.maxRows = maxRows;
        this.maxColumns = maxColumns;
        this.maxItemCount = maxItemCount;
        _RefreshComponent();
    }

	public void RefreshComponent()
	{
        _RefreshComponent();
	}

    private void _RefreshComponent()
    {
        int max = Mathf.Min(maxColumns * maxRows, maxItemCount);
        int i = 0;
        if (_items.Count > max)
        {
            for (i = _items.Count - 1; i >= max; i--)
            {
                GameObject.Destroy(_items[i]);
                _items.RemoveAt(i);
            }
        }
        else if (_items.Count < max)
        {
            int beginRow = items.Count / maxColumns;
            int beginColumn = items.Count % maxColumns;
            CreateComponent(beginRow, beginColumn);
        }
    }

	private void CreateComponent(int beginRow = 0, int beginColumn = 0)
	{
		if (template != null)
		{
			//Bounds b = new Bounds();
			bool brFlag = false;
            for (int y = beginRow; y < maxRows; y++)
			{
				for (int x = beginColumn; x < maxColumns; x++)
				{
					GameObject go = NGUITools.AddChild(gameObject, template);
                    go.name = go.name + (y * maxColumns + x);
					go.SetActive(true);
					Transform t = go.transform;
					t.localPosition = new Vector3(padding + (x + 0.5f) * spacing, -padding - (y + 0.5f) * spacing, 0f);
					//b.Encapsulate(new Vector3(padding * 2f + (x + 1) * spacing, -padding * 2f - (y + 1) * spacing, 0f));
					if (useUIIndex) {
                        go.AddComponent<UIIndex>().index = y * maxColumns + x;
					}
					_items.Add(go);
                    
					if (y * maxColumns + x + 1 >= maxItemCount)
					{
						brFlag = true;
						break;
					}
				}
				if (brFlag) break;
			}
		}
	}
}
