using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopCreationManager : MonoBehaviour
{

    private BoardGenerator boardGenerator;

    [SerializeField]
    private GameObject troopSelectorGameObject;

    [SerializeField]
    private SelectedTroopPivot selectedTroopPivot;

    private MoneyManager moneyManager;

    private GameObject selectedTroop;

    private GameObject[,] createdTroops;

    [SerializeField]
    private List<GameObject> troopList;

    private void Start()
    {
        boardGenerator = GetComponent<BoardGenerator>();
        moneyManager = GetComponent<MoneyManager>();
        createdTroops = new GameObject[boardGenerator.columns, boardGenerator.rows];
    }
    void Update()
    {
    }


    public void SelectTroop(int index)
    {
        if (moneyManager.GetMoney() < troopList[index].GetComponent<TroopController>().price)
        {
            //Mensaje de falta dinero
            return;
        }
        selectedTroopPivot.SetTroop(troopList[index]);
        Debug.Log(index);
        selectedTroop = troopList[index];
    }

    public bool isCellEmpty(int cI, int rI)
    {
        return cI >= 0 && rI >= 0 && cI <= createdTroops.GetLength(0) - 1 && rI <= createdTroops.GetLength(1) - 1 && createdTroops[cI,rI] == null;
    }

    public void CreateTroop(int cI, int rI, Vector2 position)
    {
        if (!isCellEmpty(cI, rI)) return;
        moneyManager.SpendMoney(selectedTroop.GetComponent<TroopController>().price);
        createdTroops[cI,rI] = Instantiate(selectedTroop, position, Quaternion.identity);

    }

    public int GetMoneyGenerators()
    {
        int i = 0;
        foreach (GameObject troop in createdTroops)
        {
            if (troop != null && troop.transform.tag == "MoneyGenerator") i++;
        }
        return i;
    }

}
