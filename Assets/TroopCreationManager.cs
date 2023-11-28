using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopCreationManager : MonoBehaviour
{


    [SerializeField]
    private GameObject troopSelectorGameObject;

    [SerializeField]
    private SelectedTroopPivot selectedTroopPivot;



    [SerializeField]
    private List<GameObject> troopList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SelectTroop(int index)
    {

        Debug.Log("a");
        selectedTroopPivot.SetTroop(troopList[index]);
    }

}
