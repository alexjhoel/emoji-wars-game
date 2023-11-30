using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private Image waveBar; 

    private BoardGenerator boardGenerator;
    
    public List<WaveData> waves = new List<WaveData>();

    private List<GameObject> spawnedEnemies = new List<GameObject>();
    bool readyToSpawn = true;
    float timer = 0;
    float timeToSpawn = 0;
    int waveIndex = 0;

    int totalEnemiesToSpawn;

    IEnumerator nextWave;
    void Start()
    {
        boardGenerator = GetComponent<BoardGenerator>();
        timeToSpawn = waves[0].delay;

        waves.ForEach(x => totalEnemiesToSpawn+= x.enemiesList.Count) ;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer >= timeToSpawn && waveIndex < waves.Count)
        {
            List<GameObject> eList = waves[waveIndex].enemiesList;

            List<GameObject> eListToSpawn = new List<GameObject>();

            for (int y = 0; y < 5; y++)
            {
                if (y > eList.Count - 1) eListToSpawn.Add(null);
                else eListToSpawn.Add(eList[y]);
            }

            eListToSpawn.Shuffle();

            SpawnWave(eListToSpawn);

            for (int i = 5; i < eList.Count; i += 5)
            {
                eListToSpawn = new List<GameObject>();

                for (int y = i; y < i + 5; y++)
                {
                    if (y > eList.Count - 1) eListToSpawn.Add(null);
                    else eListToSpawn.Add(eList[y]);
                }

                //Método Shuffle creado en la clase RandomExtensions, randomizamos la wave de enemigos
                eListToSpawn.Shuffle();

                StartCoroutine(SpawnWaveCoroutine(i / 5 * 1.5f, eListToSpawn));
            }
            if(waveIndex < waves.Count - 1)
                timeToSpawn = timer + waves[waveIndex + 1].delay;
            waveIndex++;
            UpdateUI();
        }

        CheckSpawnedEnemies();


    }

    private void UpdateUI()
    {
        waveBar.fillAmount = spawnedEnemies.Count / (float)totalEnemiesToSpawn;
    }

    private void CheckSpawnedEnemies()
    {
        if(!spawnedEnemies.Any(x => x != null) && waveIndex < waves.Count && waveIndex != 0)
        {
            Debug.Log("huh?");
            timeToSpawn = 0;
        }
    }

    private void SpawnWave(List<GameObject> eListToSpawn)
    {
        for (int y = 0; y < eListToSpawn.Count; y++)
        {
            if (eListToSpawn[y] == null) continue;
            Vector2 pos = boardGenerator.getCellPosition(y, boardGenerator.rows + 2);
            GameObject enemyGameObject = Instantiate(eListToSpawn[y], null);
            enemyGameObject.transform.position = pos;
            spawnedEnemies.Add(enemyGameObject);
        }
    }

    private IEnumerator SpawnWaveCoroutine(float delay, List<GameObject> eListToSpawn) 
    {
        yield return new WaitForSeconds(delay);
        SpawnWave(eListToSpawn);
        
    }

    public void NextWave()
    {
        timeToSpawn = 0;
    }

}
