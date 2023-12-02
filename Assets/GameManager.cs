using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverObject;
    [SerializeField]
    private GameObject winObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverObject.SetActive(true);
    }

    public void WinGame()
    {
        winObject.SetActive(true);
    }

    public void Exit()
    {

    }
}
