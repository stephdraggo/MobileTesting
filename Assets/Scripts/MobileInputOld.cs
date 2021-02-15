using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace old
{
    public class MobileInputOld : MonoBehaviour
    {
        #region Swipe Class
        /// <summary>
        /// Swiper no swiping!
        /// Contains all information about one swipe.
        /// (eg points along swipe path)
        /// </summary>
        public class Swipe
        {
            #region Variables
            /// <summary>
            /// Index of finger associated with this swipe.
            /// </summary>
            public int fingerId = 0;
            /// <summary>
            /// The position the swipe started from.
            /// </summary>
            public Vector2 initialPosition = new Vector2();
            /// <summary>
            /// List of points along the swipe path.
            /// </summary>
            public List<Vector2> positions = new List<Vector2>();
            #endregion
            public Swipe(Vector2 _initialPos, int _fingerId)
            {
                initialPosition = _initialPos; //set start position
                fingerId = _fingerId; //set finger id
                positions.Add(_initialPos); //add start position to list of swipe positions
            }
        }
        #endregion

        #region Variables
        [Tooltip("How long the swipe has been running for in seconds.")]
        float flickTime = 0;

        [Tooltip("Origin point of the swipe.")]
        Vector2 flickOrigin = Vector2.positiveInfinity;

        [Tooltip("Index of finger initiating swipe.")]
        int initialFingerIndex = -1;

        [Tooltip("Contains all swipes being processed, each key is the corresponding finger index.")]
        private static Dictionary<int, Swipe> swipes = new Dictionary<int, Swipe>();
        #endregion

        #region Properties
        /// <summary>
        /// direction that the last flick occured, positive infinity means no flick occured
        /// </summary>
        public static Vector2 FlickDirection { get; private set; } = Vector2.positiveInfinity;

        /// <summary>
        /// how hard the player flicked, positive infinity means no flick occured
        /// </summary>
        public static float FlickStrength { get; private set; } = float.PositiveInfinity;

        /// <summary>
        /// How many swipes are in progress.
        /// This uses Lamba, which means can only get value, not set.
        /// </summary>
        public static int SwipeCount => swipes.Count;
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
                    if (touch.phase == TouchPhase.Began && flickOrigin.Equals(Vector2.positiveInfinity))
                    {
                        flickOrigin = touch.position; //set touch position as swipe origin
                        initialFingerIndex = touch.fingerId;
                    }
                    else if (touch.phase == TouchPhase.Ended && touch.fingerId == initialFingerIndex && flickTime < 1)
                    {
                        CalculateFlick(touch.position);
                    }
                    #region store swipe data
                    //Swipe storage time
                    if (touch.phase == TouchPhase.Began)
                    {

                        //add this swipe to dictionary
                        swipes.Add(touch.fingerId, new Swipe(touch.position, touch.fingerId));
                    }
                    if (touch.phase == TouchPhase.Moved && swipes.TryGetValue(touch.fingerId, out Swipe swipe)) //if this swipe moved and it exists in dictionary
                    {
                        //add position to list of positions for this swipe
                        swipe.positions.Add(touch.position);
                    }
                    else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && swipes.TryGetValue(touch.fingerId, out swipe)) //if this swipe has ended and it exists in dictionary
                    {
                        //remove this swipe from the dictionary
                        swipes.Remove(swipe.fingerId);
                    }
                    #endregion
                }

                flickTime += Time.deltaTime; //increment length in time of swipe
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
        #region public static get swipe
        /// <summary>
        /// Attempt to retrieve swipe based on passed finger index.
        /// </summary>
        /// <param name="_index">finger index being checked for swipes</param>
        /// <returns>swipe if not null</returns>
        public static Swipe GetSwipe(int _index)
        {
            Swipe temp; //this is null
            swipes.TryGetValue(_index, out temp); //checks if there is a swipe at this index from dictionary and assigns it
            return temp; //returns swipe or null
        }
        #endregion
        #region private calculate flick
        /// <summary>
        /// calculates flick based off origin and end point of touch
        /// </summary>
        /// <param name="_endPoint">the position the touch ended at</param>
        void CalculateFlick(Vector2 _endPoint)
        {
            Vector2 heading = flickOrigin - _endPoint;
            FlickDirection = heading.normalized; //direction
            FlickStrength = heading.magnitude; //length

            //reset swipe origin after calculation
            flickOrigin = Vector2.positiveInfinity;
        }
        #endregion
        #region private reset flick
        /// <summary>
        /// resets swipe details
        /// </summary>
        void ResetSwipe()
        {
            FlickDirection = Vector2.positiveInfinity;
            FlickStrength = float.PositiveInfinity;
            flickOrigin = Vector2.positiveInfinity;
            flickTime = 0;
            initialFingerIndex = -1;
        }
        #endregion
        #endregion
    }
}