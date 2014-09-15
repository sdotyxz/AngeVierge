using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PhalanxTeam : MonoBehaviour
{
    public int maxItemCount = 16;
    public int maxRows = 4;
    public int maxColumns = 4;
    public bool useUIIndex = false;

    public GameObject template;
    public bool temlateIsPrefab = true;
    private List<GameObject> _items = new List<GameObject>();
    public List<GameObject> items { get { return _items; } }

    public Vector2 spacing=new Vector2(100,100);

    public GameObject Get(int index)
    {
        return _items.Count > index ? _items[index] : null;
    }

    public T Get<T>(int index) where T : Component
    {
        GameObject go = Get(index);
        return go != null ? go.GetComponent<T>() : null;
    }

    

    void Awake()
    {
        if (!temlateIsPrefab)
        {
            if (template != null)
            {
                template.SetActive(false);
            }
        }
        //CreateComponent();
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
        int max =maxItemCount;
        int i = 0;
        if (_items.Count > max)
        {
            for (i = _items.Count - 1; i >= max; i--)
            {
                GameObject.Destroy(_items[i]);
                _items.RemoveAt(i);
            }
            ResetPosition();
        }
        else if (_items.Count < max)
        {
            for (int x = _items.Count; x < max; x++)
            {
                GameObject go = NGUITools.AddChild(gameObject, template);
                go.name = go.name + (x);
                go.SetActive(true);

                _items.Add(go);
            }
            ResetPosition();
        }
        else
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        int maxNum = _items.Count;
        bool brFlag = false;
        for (int i = 0; i < maxRows; i++)
        {
            for (int j = 0; j < maxColumns; j++)
            {
                _items[i * maxColumns + j].transform.localPosition = new Vector3(template.transform.localPosition.x + j * spacing.x, template.transform.localPosition.y -i * spacing.y, 0f);
                if (i * maxColumns + j + 1 >= maxItemCount)
                {
                    brFlag = true;
                    break;
                }
            }
            if (brFlag) break;
        }
    }
}

