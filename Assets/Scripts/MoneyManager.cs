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


    private float money = 100;
    private float totalToAdd = 1;
    private int moneyToAddMultiplier = 1;
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
        totalToAdd = moneyToAddMultiplier * multiplierValue;
        money += totalToAdd * Time.deltaTime;
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
        money -= amount * 10;
    }

    public void SetMoney(float amount)
    {
        money = amount;
    }

    public float GetMoney()
    {
        return money / 10;
    }

    public void AddMultiplier()
    {
        moneyToAddMultiplier ++;
    }

    public void RemoveMultiplier()
    {
        moneyToAddMultiplier--;
    }

    public void SetMultiplier(int amount)
    {
        moneyToAddMultiplier = amount;
    }
}
