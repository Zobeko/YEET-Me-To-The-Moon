using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBeyondScreen : MonoBehaviour
{

    SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!renderer.isVisible)
        {
            Destroy(gameObject);
        }
    }

}
