using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NbCrewmates : MonoBehaviour
{
    [SerializeField] private Text crewmatesText = null;

    // Update is called once per frame
    void Update()
    {
        crewmatesText.text = "Crewmates : " + GameController.instance.remainingCrew.ToString();
    }
}
