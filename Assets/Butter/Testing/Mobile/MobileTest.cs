using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BUTTer.Mobile;

public class MobileTest : MonoBehaviour
{
    #region Variables
    [SerializeField] private bool testJoystick = false, testSwipe = false;
    #endregion

    #region Start
    void Start()
    {
        MobileInput.Initialise();
    }
    #endregion

    #region Update
    void Update()
    {
        if (testJoystick)
        {
            transform.position += transform.forward * MobileInput.GetJoystickAxis(JoystickAxis.Vertical) * Time.deltaTime;
            transform.position += transform.right * MobileInput.GetJoystickAxis(JoystickAxis.Horizontal) * Time.deltaTime;
        }

        if (testSwipe)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR

            //mobile input here

#else
            if (Input.GetMouseButtonDown(0)) //left click start
            {
                
            }
            if (Input.GetMouseButton(0)) //left click update
            {

            }
            if (Input.GetMouseButtonUp(0)) //left click end
            {
                
            }

            Vector2 touchPos = Input.mousePosition;
#endif
        }
    }
    #endregion

    #region Methods

    #endregion
}
