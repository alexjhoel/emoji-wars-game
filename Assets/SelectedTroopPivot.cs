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
    GameObject troopChildGameObject;

    GameObject troopPrefab;

    bool dragEnabled;

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

        troopChildGameObject.transform.position = cellPosition;


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!dragEnabled || !Input.GetMouseButton(0)) return;
        cellPosition = collision.transform.position;

        troopChildGameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        troopChildGameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255,0f);
    }



    public void SetTroop(GameObject troopPrefab)
    {
       dragEnabled = true;
       this.troopPrefab = troopPrefab;
    }
}
