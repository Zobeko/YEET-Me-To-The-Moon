using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimer : EnemyShip
{

    public float aimingSpeed = 90;

    private void Update()
    {
        FacePlayer();
    }

    protected void FacePlayer()
    {

        Vector2 player_direction = (Vector2)(GameController.instance.playerObject.transform.position - transform.position);
        float angle = -Vector2.SignedAngle(player_direction, transform.up);

        transform.Rotate(new Vector3(0,0,angle)*aimingSpeed*Time.deltaTime);
            
        //rBody.SetRotation(Mathf.Lerp(transform.rotation, transform.rotation + angle, Time.fixedDeltaTime * rotationSpeed));

    }

}
