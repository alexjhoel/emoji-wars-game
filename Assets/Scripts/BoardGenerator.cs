using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoardGenerator : MonoBehaviour
{
    public GameObject boardParent;
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
        foreach (Transform o in boardParent.transform.GetComponentsInChildren<Transform>())
        {
            if (o != null && o != boardParent.transform) DestroyImmediate(o.gameObject);
        }

        for (int i = 0; i < columns; i++)
        {
            GameObject nG = new GameObject();
            nG.transform.parent = boardParent.transform;
            nG.transform.localPosition = new Vector3(i * (cellSize.x + offset) - columns / 2 * (cellSize.x + offset), 0, 0);
            nG.transform.localScale = Vector3.one;
        }

        Transform[] children = boardParent.GetComponentsInChildren<Transform>();

        foreach (Transform o in children)
        {
            if(boardParent.transform != o)
            for (int i = 0; i < rows; i++)
            {
                GameObject nG = Instantiate(cellPrefab, o);
                nG.transform.localPosition = new Vector3(0, i * (cellSize.y + offset) - rows / 2 * (cellSize.y + offset), 0);
                nG.transform.localScale = cellSize;
            }
        }

    }

    public Vector2 getCellPosition(int cI, int rI)
    {
        cI = cI - columns / 2;
        rI = rI - rows / 2;

        Vector2 pos = new Vector2
            (
               cI * (cellSize.x + offset),
               rI * (cellSize.y + offset) - 0.38f
            );
        return pos;
    }

    public Vector2 getNearestCell(Vector2 pos)
    {
        int cI = (int)Mathf.Round(pos.x / (cellSize.x + offset));
        int rI = (int)Mathf.Round(pos.y / (cellSize.y + offset));

        cI = cI + columns / 2;
        rI = rI + rows / 2;

        return new Vector2(cI, rI);
    }

    public bool isInsideBoard(Vector2 pos)
    {
        return 
            pos.x > -1 * (cellSize.x + offset) - columns / 2 * (cellSize.x + offset) &&
            pos.x < (columns) * (cellSize.x + offset) - columns / 2 * (cellSize.x + offset) &&
            pos.y < (rows - 1) * (cellSize.y + offset) - rows / 2 * (cellSize.y + offset) &&
            pos.y > -1 * (cellSize.y + offset) - rows / 2 * (cellSize.y + offset);
    }
}
