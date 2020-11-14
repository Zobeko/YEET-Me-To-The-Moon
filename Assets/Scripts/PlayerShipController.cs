using System;
using System.Collections;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{

    public float mvSpeed;
    public float invulnerableDuration;

    private bool invulnerable = false;

    private Rigidbody2D rBody;

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {

        float mvX = Convert.ToInt32(Input.GetKey("d")) - Convert.ToInt32(Input.GetKey("a"));
        float mvY = Convert.ToInt32(Input.GetKey("w")) - Convert.ToInt32(Input.GetKey("s"));

        Vector2 mvDirection = new Vector2(mvX,mvY);
        if(mvDirection.magnitude > 0)
        {
            rBody.velocity = mvDirection * mvSpeed;
        } else
        {
            rBody.velocity = Vector3.zero;
        }
    }

    IEnumerator InvulnerableTimeCoroutine()
    {
        yield return new WaitForSeconds(invulnerableDuration);
        invulnerable = false;
    }

}
