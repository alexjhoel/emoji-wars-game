using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoardGenerator : MonoBehaviour
{
    [SerializeField]
    int rows = 9;
    [SerializeField]
    int columns = 5;
    [SerializeField]
    float offset = 3.6f;
    [SerializeField]
    GameObject cellPrefab;
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
            nG.transform.localPosition = new Vector3(i * offset - columns / 2 * offset, 0, 0);
            nG.transform.localScale = Vector3.one;
        }

        Transform[] children = GetComponentsInChildren<Transform>();

        foreach (Transform o in children)
        {
            for (int i = 0; i < rows; i++)
            {
                Instantiate(cellPrefab, o).transform.localPosition = new Vector3(0, i*offset - rows / 2 * offset, 0);
            }
        }

    }
}
