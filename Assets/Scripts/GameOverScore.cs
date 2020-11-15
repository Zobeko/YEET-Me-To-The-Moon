using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour
{
    [SerializeField] private GameObject scoreKeeper = null;
    private ScoreKeeper scoreKeeperScript = null;

    private int score = 0;

    void Start()
    {
        scoreKeeper = GameObject.Find("ScoreKeeper");
        scoreKeeperScript = scoreKeeper.GetComponent<ScoreKeeper>();

        score -= (scoreKeeperScript.nbInitialCivilians - scoreKeeperScript.nbRemainingCivilians) * 1000;

        
    }

    void Update()
    {
        this.gameObject.GetComponent<Text>().text = score.ToString();
    }
}
