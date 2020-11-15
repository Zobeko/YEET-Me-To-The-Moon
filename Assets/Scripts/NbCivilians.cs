using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NbCivilians : MonoBehaviour
{
    [SerializeField] private Text civiliansText = null;

    // Update is called once per frame
    void Update()
    {
        civiliansText.text = "Civilians : " + GameController.instance.remainingCivilians.ToString();
    }
}
