using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public int score = 0;

    public int nbInitialCivilians = 0;

    public int nbRemainingCivilians = 0;


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        score = GameController.instance.score;

        nbInitialCivilians = GameController.instance.totalCivilians;

        nbRemainingCivilians = GameController.instance.remainingCivilians;

        
    }
}
