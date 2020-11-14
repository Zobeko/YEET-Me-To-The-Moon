using System.Collections;
using System.Net.Http.Headers;
using UnityEngine;

public class YeetController : MonoBehaviour
{
    public static YeetController instance = null;

    public float yeetDuration = 0f;
    public float yeetTimeScale = 0f;

    public bool isYeetActivated = false;

    


    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

   

    void Update()
    {
        if (Input.GetButtonDown("Yeet"))
        {
            isYeetActivated = true;
        }

        Debug.Log(Time.timeScale);

        if (isYeetActivated)
        {
            Yeet();
        }
    }


    private void Yeet()
    {
        Time.timeScale = 0f;
        yeetTimeScale += 1 / 60f;

 

        if (yeetTimeScale >= yeetDuration)
        {
            isYeetActivated = false;
            yeetTimeScale = 0f;
            Time.timeScale = 1f;


        }
    }


    /*private IEnumerator StartingCoroutine()
    {
        passengerScript.enabled = true;
        yield return new WaitForSeconds(0.5f);
        passengerScript.enabled = false;
        Debug.Log("Coroutine");
    }*/

}
