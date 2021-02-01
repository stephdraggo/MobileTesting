using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileFun : MonoBehaviour
{
    #region Variables
    

    #endregion
    #region Start
    void Start()
    {
        if (Input.touchCount > 0)
        {
           Touch touch = Input.GetTouch(0);
            //touchphase is state of touch (0=started, 1=stationary, 2=moving, 3=ended, 4=errored out)
        }

        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            Input.gyro.enabled = false;
        }
    }
    #endregion
    #region Update
    void Update()
    {
        Gyroscope _gyro = Input.gyro; //do not cache as class variables bc gets overridden
    }
    #endregion
}
