using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalCrew = 5;
    public int totalCivilians = 4;
    public int remainingCrew = 5;
    public int remainingCivilians = 4;

    private bool currentYeetStatus = false;
    private bool previousYeetStatus = false;

    public float reversedControls = 1;
    public bool leftEngineerBool = true;
    public bool rightEngineerBool = true;
    public bool leftGunnerBool = true;
    public bool rightGunnerBool = true;

    public GameObject[] waveArrayEasy;
    public GameObject[] waveArrayHard;
    
    private int score = 0;

    public static GameController instance;

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
            Instantiate(waveArrayEasy[Random.Range(0, waveArrayEasy.Length-1)]);
        }
        else
        {
            Instantiate(waveArrayHard[Random.Range(0, waveArrayHard.Length - 1)]);
        }
    }

    public void SpawnFixedWave(bool isEasy, int waveIndex) //fais apparaître la vague d'index waveIndex
    {
        if (isEasy)
        {
            if (waveIndex > (waveArrayEasy.Length - 1))
            {
                return;
            }
            else
            {
                Instantiate(waveArrayEasy[waveIndex]);
            }
        }
        else
        {
            if (waveIndex > (waveArrayHard.Length - 1))
            {
                return;
            }
            else
            {
                Instantiate(waveArrayHard[waveIndex]);
            }
        }
    }

    public void DestroyEnemies() // Detruit tous les ennemis || ATTENTION A CE QUE CA NE DETRUISE PAS LES ENNEMIS FUTURS
    {
        foreach (GameObject ennemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(ennemy);
        }
    }
        
}
