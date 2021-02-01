using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Swiper no swiping!
 * 
 * 
 */

public class MobileInput : MonoBehaviour
{
    #region Variables
    [Tooltip("How long the swipe has been running for in seconds.")]
    float swipeTime = 0;

    [Tooltip("Origin point of the swipe.")]
    Vector2 swipeOrigin = Vector2.positiveInfinity;

    [Tooltip("Index of finger initiating swipe.")]
    int initialFingerIndex = -1;
    #endregion

    #region Properties
    /// <summary>
    /// direction that the last flick occured, positive infinity means no flick occured
    /// </summary>
    public static Vector2 Flick { get; private set; } = Vector2.positiveInfinity;

    /// <summary>
    /// how hard the player flicked, positive infinity means no flick occured
    /// </summary>
    public static float FlickPower { get; private set; } = float.PositiveInfinity;
    #endregion

    #region Start
    void Start()
    {
        
    }
    #endregion

    #region Update
    void Update()
    {
        //check for touch input
        if (Input.touchCount > 0)
        {
            //loop through touches
            foreach (Touch touch in Input.touches)
            {
                //if this is the first frame there exists a touch
                if (touch.phase == TouchPhase.Began && swipeOrigin.Equals(Vector2.positiveInfinity))
                {
                    swipeOrigin = touch.position; //set touch position as swipe origin
                    initialFingerIndex = touch.fingerId;
                }
                else if (touch.phase == TouchPhase.Ended && touch.fingerId == initialFingerIndex && swipeTime < 1)
                {
                    CalculateFlick(touch.position);
                }
            }

            swipeTime += Time.deltaTime; //increment length in time of swipe
        }
        //if none, reset swipe
        else
        {
            //optimise this so not every frame
            ResetSwipe();
        }
    }
    #endregion

    #region Methods
    #region calculate flick
    /// <summary>
    /// calculates flick based off origin and end point of touch
    /// </summary>
    /// <param name="_endPoint">the position the touch ended at</param>
    void CalculateFlick(Vector2 _endPoint)
    {
        Vector2 heading = swipeOrigin - _endPoint;
        Flick = heading.normalized; //direction
        FlickPower = heading.magnitude; //length

        //reset swipe origin after calculation
        swipeOrigin = Vector2.positiveInfinity;
    }
    #endregion
    #region reset flick
    /// <summary>
    /// resets swipe details
    /// </summary>
    void ResetSwipe()
    {
        Flick = Vector2.positiveInfinity;
        FlickPower = float.PositiveInfinity;
        swipeOrigin = Vector2.positiveInfinity;
        swipeTime = 0;
        initialFingerIndex = -1;
    }
    #endregion
    #endregion
}
