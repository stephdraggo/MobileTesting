using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BUTTer.Mobile;

public class MobileTest : MonoBehaviour
{
    #region Variables

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
        transform.position += transform.forward * MobileInput.GetJoystickAxis(JoystickAxis.Vertical) * Time.deltaTime;
        transform.position += transform.right * MobileInput.GetJoystickAxis(JoystickAxis.Horizontal) * Time.deltaTime;
    }
    #endregion

    #region Methods

    #endregion
}
