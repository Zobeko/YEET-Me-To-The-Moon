using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{

    public Vector3 wavePosition;
    public WaveType waveType;

    private Vector3 centerPosition = new Vector3(-5f, 0f, 0f);

    public enum WaveType
    {
        SideArrival,
        TopArrival,
        ZoomArrival,
    };


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //if ((wavePosition - centerPosition).magnitude <= 0.05f && waveType != WaveType.ZoomArrival)
        //{
        //    switch (waveType)
        //    {
        //        case (WaveType.SideArrival):
        //            wavePosition += (centerPosition - wavePosition) * Time.deltaTime / 2.5f;
        //            break;

        //        case (WaveType.TopArrival):
        //            wavePosition += (centerPosition - wavePosition) * Time.deltaTime / 2.5f;
        //            break;
        //    }
        //}

        //else if (waveType == WaveType.ZoomArrival)
        //{
        //    foreach (Transform enemy in this.transform)
        //    {
        //        if ((wavePosition - enemy.position).magnitude <= 2f)
        //        {
        //            enemy.position += (wavePosition - enemy.position) * Time.deltaTime / 2.5f;
        //        }
        //    }
        //}
        switch (waveType)
        {
            case (WaveType.SideArrival):
                wavePosition += (centerPosition - wavePosition) * Time.deltaTime / 2.5f;
                break;

            case (WaveType.TopArrival):
                wavePosition += (centerPosition - wavePosition) * Time.deltaTime / 2.5f;
                break;
            case (WaveType.ZoomArrival):
                foreach (Transform enemy in this.transform)
                {
                    if ((wavePosition - enemy.position).magnitude >= 10f)
                    {
                        enemy.position += (wavePosition - enemy.position) * Time.deltaTime / 2.5f;
                    }
                }
                break;
        }
        transform.position = wavePosition;
    }

}
