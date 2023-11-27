using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{

    [SerializeField]
    private Image moneyBar;

    [SerializeField]
    private TMP_Text moneyText ;

    [SerializeField]
    private SelectedTroopPivot selectedTroopPivot;


    private float money = 0;
    private float moneyAdd = 1;
    private int moneyAddMultiplier = 1;
    [SerializeField]
    private int multiplierValue;

    void Start()
    {
        
    }

    void Update()
    {
        //Agregar dinero pasivamente
        //Moneyadd: la cantidad de dinero que se sumará por frame
        //MoneyAddMultiplier: cantidad de generadores de dinero en el tablero
        //Multiplier value: valor de mulitiplicación por generador
        moneyAdd = moneyAddMultiplier * multiplierValue;
        money += moneyAdd * Time.deltaTime;
        UpdateUI();
    }

    private void UpdateUI()
    {
        //Actualizar interfaz gráfica con las variables
        moneyBar.fillAmount = money % 10  / 10;
        moneyText.text = Mathf.Floor(money / 10f) + ""; 
    }

    public void SpendMoney(float amount)
    {
        money -= amount;
    }

    public void SetMoney(float amount)
    {
        money = amount;
    }

    public float GetMoney()
    {
        return money;
    }

    public void AddMultiplier()
    {
        moneyAddMultiplier ++;
    }

    public void RemoveMultiplier()
    {
        moneyAddMultiplier--;
    }

    public void SetMultiplier(int amount)
    {
        moneyAddMultiplier = amount;
    }

    public void SelectTroop(int index)
    {

        Debug.Log("a");
        selectedTroopPivot.SetTroop(null);
    }
}
