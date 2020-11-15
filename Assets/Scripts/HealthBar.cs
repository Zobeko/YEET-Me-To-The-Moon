using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthBarSlider = null;

    public GameObject playerShip;
    private PlayerShipController playerShipController;

    void Awake()
    {
        healthBarSlider = this.gameObject.GetComponent<Slider>();
    }

    void Start()
    {
        playerShipController = playerShip.GetComponent<PlayerShipController>();
    }

    void Update()
    {
        healthBarSlider.value = playerShipController.health;
    }
}
