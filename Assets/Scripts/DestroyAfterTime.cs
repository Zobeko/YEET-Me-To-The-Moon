using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyTime;

    void Start()
    {
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

}
