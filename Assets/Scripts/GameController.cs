﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalCrew = 5;
    public int totalCivilians = 4;
    public int remainingCrew = 5;
    public int remainingCivilians = 4;

    public int waveNumber;
    private int currentWave = 0;

    private bool currentYeetStatus = false;
    private bool previousYeetStatus = false;

    public float reversedControls = 1;
    public bool leftEngineerBool = true;
    public bool rightEngineerBool = true;
    public bool leftGunnerBool = true;
    public bool rightGunnerBool = true;

    public GameObject[] waveArrayEasy;
    public GameObject[] waveArrayHard;
    public float[] delayBetweenWaves;

    //    private Vector3 waveSpawnPoint;

    private float timeBetweenWave = 10f;
    public float gameStartDate = 5f;
    
    public int score = 0;
    public int waveIndex = 0;

    public GameObject playerObject;

    public static GameController instance;

    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(10f);
        print("Game started");
        StartCoroutine("SpawnEnemyCoroutine");
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        if (currentWave < waveNumber-1)
        {
            SpawnFixedWave(true, waveIndex);
            yield return new WaitForSeconds(delayBetweenWaves[waveIndex]);
            waveIndex++;
            currentWave++;
            StartCoroutine("SpawnEnemyCoroutine");
        }
        else
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }

private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("PlayerShip");
        StartCoroutine("StartGameCoroutine");
        //waveSpawnPoint = new Vector3(-4.5f, 0f, 0f);
    }

    // Update is called once per frame
    void Update() 
    {

        previousYeetStatus = currentYeetStatus;
        currentYeetStatus = YeetController.instance.isYeetActivated;
        if (previousYeetStatus == true && currentYeetStatus == false) //mets à jour en cas de yeet  
        {
            OnPassengerYeet(YeetController.instance.yeetedPassengerType);
            DestroyEnemies();
        }
    }

    public void OnEnemyDeath() //incremente le score sur la mort d'un ennemi 
    {
        score += 100;
    }

    public void OnPassengerYeet(Passenger.PassengerType yeetedPassengerType) // mets à jour les booléns et valeurs en fonction du passager yeete
    {
        switch (yeetedPassengerType)
        {
            case Passenger.PassengerType.civilian:
                remainingCivilians -= 1;
                if (remainingCivilians == 0)
                {
                    SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
                }
                return;

            case Passenger.PassengerType.rightEngineer:
                remainingCrew -= 1;
                rightEngineerBool = false;
                return;

            case Passenger.PassengerType.leftEngineer:
                remainingCrew -= 1;
                leftEngineerBool = false;
                return;

            case Passenger.PassengerType.rightGunner:
                remainingCrew -= 1;
                rightGunnerBool = false; 
                return;

            case Passenger.PassengerType.leftGunner:
                remainingCrew -= 1;
                leftGunnerBool = false;
                return;

            case Passenger.PassengerType.pilote:
                remainingCrew -= 1;
                reversedControls = -1;
                return;
        }
    }

    public void CountScore() //calcul final du score
    {
        score -= (totalCivilians - remainingCivilians) * 1000;
        //score += playerShip.health * 300
    } 

    public void SpawnRandomWave(bool isEasy) //fais apparaîte une vague aléatoire
    {
        if (isEasy)
        {
            GameObject spawnedWaveObject = waveArrayEasy[Random.Range(0, waveArrayEasy.Length)];
            Wave spawnedWave = spawnedWaveObject.GetComponent<Wave>();

            switch (spawnedWave.waveType)
            {
                case (Wave.WaveType.SideArrival):
                    spawnedWave.wavePosition = new Vector3(-12f, 8f, 0f);
                    break;

                case (Wave.WaveType.TopArrival):
                    spawnedWave.wavePosition = new Vector3(-5f, 8f, 0f);
                    break;

                case (Wave.WaveType.ZoomArrival):
                    spawnedWave.wavePosition = new Vector3(-5f, 0f, 0f);
                    break;
            }

            Instantiate(spawnedWaveObject, spawnedWave.wavePosition, Quaternion.identity);
        }
        else
        {
            GameObject spawnedWaveObject = waveArrayHard[Random.Range(0, waveArrayHard.Length)];
            Wave spawnedWave = spawnedWaveObject.GetComponent<Wave>();

            switch (spawnedWave.waveType)
            {
                case (Wave.WaveType.SideArrival):
                    spawnedWave.wavePosition = new Vector3(-12f, 5f, 0f);
                    break;

                case (Wave.WaveType.TopArrival):
                    spawnedWave.wavePosition = new Vector3(-5f, 5f, 0f);
                    break;

                case (Wave.WaveType.ZoomArrival):
                    spawnedWave.wavePosition = new Vector3(-5f, 0f, 0f);
                    break;
            }

            Instantiate(spawnedWaveObject, spawnedWave.wavePosition, Quaternion.identity);
        }
    }

    public void SpawnFixedWave(bool isEasy, int waveIndex) //fais apparaître la vague d'index waveIndex
    {
        if (isEasy)
        {
            if (waveIndex > (waveArrayEasy.Length))
            {
                return;
            }
            else
            {
                GameObject spawnedWaveObject = waveArrayEasy[waveIndex];
                Wave spawnedWave = spawnedWaveObject.GetComponent<Wave>();
                Instantiate(spawnedWaveObject, spawnedWave.wavePosition, Quaternion.identity);
            }
        }
        else
        {
            if (waveIndex > (waveArrayHard.Length))
            {
                return;
            }
            else
            {
                GameObject spawnedWaveObject = waveArrayHard[Random.Range(0, waveArrayHard.Length)];
                Wave spawnedWave = spawnedWaveObject.GetComponent<Wave>();
                Instantiate(spawnedWaveObject, spawnedWave.wavePosition, Quaternion.identity);
            }
        }
    }

    public void DestroyEnemies() // Detruit tous les ennemis || ATTENTION A CE QUE CA NE DETRUISE PAS LES ENNEMIS FUTURS
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
        
            Destroy(enemy);
        }
    }
        
}
