using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateImage : MonoBehaviour
{
    float totalRotation = 5 * 360;   // total number of degrees to rotate - e.g. 5 times around in this case
    float totalTime = 6;         // seconds to take to rotate totalRotation degrees
    float currentRotationTime = 0;   // current elapsed time for the rotation



    void Update()
    {

        //// what percentage are we toward the goal?
        //float t = currentRotationTime / totalTime;

        //// interpolate the number of degrees between 0 and totalRotation, using t
        //float currentRotationAmount = Mathf.Lerp(0, totalRotation, t);
        //if (currentRotationAmount == -60f)
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, 0);
        //}


        //// set the rotation to that number of degrees
        //transform.rotation = Quaternion.Euler(0, 0, currentRotationAmount);


        //// update elapsed rotation time, making sure it doesn't go over 1
        //currentRotationTime = Mathf.Clamp(currentRotationTime + Time.deltaTime, 0, 1);

        transform.Rotate(-Vector3.forward * Time.deltaTime * 10f);
    }
}
