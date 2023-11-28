using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoardGenerator : MonoBehaviour
{
    public Vector2 cellSize;
    public int rows = 9;
    public int columns = 5;
    public float offset = 3.6f;
    public GameObject cellPrefab;
    void Start()
    {

    }

    [ExecuteAlways]
    private void Awake()
    {
        foreach (Transform o in GetComponentsInChildren<Transform>())
        {
            if (o != null && o != transform) DestroyImmediate(o.gameObject);
        }

        for (int i = 0; i < columns; i++)
        {
            GameObject nG = new GameObject();
            nG.transform.parent = transform;
            nG.transform.localPosition = new Vector3(i * (cellSize.x + offset) - columns / 2 * (cellSize.x + offset), 0, 0);
            nG.transform.localScale = Vector3.one;
        }

        Transform[] children = GetComponentsInChildren<Transform>();

        foreach (Transform o in children)
        {
            if(gameObject.transform != o)
            for (int i = 0; i < rows; i++)
            {
                GameObject nG = Instantiate(cellPrefab, o);
                nG.transform.localPosition = new Vector3(0, i * (cellSize.y + offset) - rows / 2 * (cellSize.y + offset), 0);
                nG.transform.localScale = cellSize;
            }
        }

    }
}
