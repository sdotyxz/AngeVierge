using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PhalanxBox : MonoBehaviour
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


    public Vector2 m_InitPosition; // 初始位置
    public Vector2 m_Spacing;      // 偏移

    void Awake()
    {
        if (!temlateIsPrefab)
        {
			if(template != null){
				template.SetActive(false);
			}   
        }
		CreateComponent();
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

    // 重新设置位置
    public void ResetPosition(Vector2 newInitPos, Vector2 newSpacing)
    {
        m_InitPosition = newInitPos;
        m_Spacing = newSpacing;
        int maxNum = _items.Count;
        bool brFlag = false;
        for (int i = 0; i < maxRows; i++) 
        {
            for (int j = 0; j < maxColumns; j++) 
            {
                _items[i * maxColumns + j].transform.localPosition =
                            new Vector3(m_InitPosition.x + j * m_Spacing.x, m_InitPosition.y + i * m_Spacing.y, 0f);

                if (i * maxColumns + j + 1 >= maxItemCount)
                {
                    brFlag = true;
                    break;
                }
            }
            if (brFlag) break;
        }
    }

    public void _RefreshComponent()
    {
        int max = Mathf.Min(maxColumns * maxRows, maxItemCount);
        //int maxIndex = _items.Count;
        //Debug.Log("Count ：" + _items.Count + " Max :" + max);
        if (_items.Count > max)
        {
            for (int i = _items.Count - 1; i >= max; i--)
            {
                GameObject.Destroy(_items[i]);
                _items.RemoveAt(i);
            }
        }

        CreateComponent(0, 0);
    }

	private void CreateComponent(int beginRow = 0, int beginColumn = 0)
	{
		if (template != null)
		{
			//Bounds b = new Bounds();
            int maxExistNum = _items.Count;
			bool brFlag = false;
            for (int y = beginRow; y < maxRows; y++)
			{
				for (int x = beginColumn; x < maxColumns; x++)
				{
                    if (y * maxColumns + x + 1 <= maxExistNum)
                    {
                        _items[y * maxColumns + x].transform.localPosition =
                            new Vector3(m_InitPosition.x + x * m_Spacing.x, m_InitPosition.y + y * m_Spacing.y, 0f);
                    }
                    else 
                    {
                        GameObject go = NGUITools.AddChild(gameObject, template);
                        go.SetActive(true);
                        Transform t = go.transform;
                        // 计算偏移
                        t.localPosition = new Vector3(m_InitPosition.x + x * m_Spacing.x, m_InitPosition.y + y * m_Spacing.y, 0f);
                        //t.position = new Vector3(m_InitPosition.x + x * m_Spacing.x, m_InitPosition.y + y * m_Spacing.y, 0f);
                        //Debug.Log("Index x :" + x + " Index Y :" + y + "Test Commodity Position :" + t.localPosition);
                        if (useUIIndex)
                        {
                            go.AddComponent<UIIndex>().index = y * maxColumns + x;
                        }
                        _items.Add(go);
                    }

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
