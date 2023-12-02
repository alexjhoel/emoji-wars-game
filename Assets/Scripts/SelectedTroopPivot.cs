using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedTroopPivot : MonoBehaviour
{
    [SerializeField]
    BoardGenerator boardGenerator;

    [SerializeField]
    TroopCreationManager troopCreationManager;

    GameObject troopChildGameObject;

    GameObject troopPrefab;

    bool dragEnabled;
    bool cellDetected;

    Vector3 cellPosition;



    void Start()
    {
        
    }

    void Update()
    {
        if (!dragEnabled) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(mousePos.x, mousePos.y , 0);    

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            transform.position = touch.position;

            Debug.Log("Touch Position : " + touch.position);
        }

        

        Vector2 nearestCell = boardGenerator.getNearestCell(transform.position);
        int cI = (int)nearestCell.x;
        int rI = (int)nearestCell.y;

        Vector2 troopChildPosition = boardGenerator.getCellPosition(cI, rI);

        troopChildGameObject.transform.position = troopChildPosition;

        bool insideBoard = boardGenerator.isInsideBoard(troopChildPosition);

        troopChildGameObject.SetActive(insideBoard && troopCreationManager.isCellEmpty(cI, rI));

        Debug.Log(cI + " " + rI);

        if (troopCreationManager.isCellEmpty(cI,rI) && dragEnabled && Input.GetMouseButtonUp(0))
        {
            dragEnabled = false;
            Destroy(troopChildGameObject);
            troopCreationManager.CreateTroop(cI,rI, troopChildPosition);
        }


    }

    public void SetTroop(GameObject troopPrefab)
    {
       //cellDetected = false;
       dragEnabled = true;
       this.troopPrefab = troopPrefab;
       troopChildGameObject = Instantiate(troopPrefab, transform, false);
       troopChildGameObject.GetComponentInChildren<Animator>().enabled = false;
       troopChildGameObject.GetComponent<TroopController>().enabled = false;
       troopChildGameObject.GetComponent<BoxCollider2D>().enabled = false;

        foreach (SpriteRenderer spriteRenderer in troopChildGameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.color = new Color(255, 255, 255, 0.7f);
        }

    }
}
