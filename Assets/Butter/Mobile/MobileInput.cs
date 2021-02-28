using UnityEngine;
using NullReEx = System.NullReferenceException;
using InvalidOpEx = System.InvalidOperationException;

namespace BUTTer.Mobile
{
    public class MobileInput : MonoBehaviour
    {
        #region Variables
        public static bool initilised => instance != null;

        private static MobileInput instance = null;

        [SerializeField] private JoystickInput joystickInput;
        [SerializeField] private SwipeInput swipeInput;
        #endregion

        #region Methods

        #region initialise
        /// <summary>
        /// If the system is not set up, instantiate mobile input prefab and assign static reference
        /// </summary>
        public static void Initialise()
        {
            if (initilised) //if already initialised
            {
                throw new InvalidOpEx("Mobile Input already initialised."); //throw error
            }

            MobileInput prefabInstance = Resources.Load<MobileInput>("Mobile Input Prefab"); //load prefab
            instance = Instantiate(prefabInstance); //instantiate into scene and set reference

            instance.gameObject.name = "Mobile Input"; //foolproof naming
            DontDestroyOnLoad(instance.gameObject); //make permanent throughout scenes
        }
        #endregion
        
        #region get joystick
        /// <summary>
        /// return joystick axis value if valid
        /// </summary>
        /// <param name="_axis">the axis to get value of (x or y)</param>
        /// <returns>valid joystick axis value</returns>
        public static float GetJoystickAxis(JoystickAxis _axis)
        {
            if (!initilised)//check we have input set up
            {
                throw new InvalidOpEx("Mobile Input not initialised.");
            }

            if (instance.joystickInput == null) //check joystick is not null
            {
                throw new NullReEx("Joystick Input reference not set.");
            }

            switch (_axis)
            {
                case JoystickAxis.Horizontal:
                    return instance.joystickInput.Axis.x;
                case JoystickAxis.Vertical:
                    return instance.joystickInput.Axis.y;
                default:
                    return 0;
            }
        }
        #endregion

        #region get swipe
        /// <summary>
        /// Attempt to retrieve swipe based on passed finger index.
        /// </summary>
        /// <param name="_index">finger index being checked for swipes</param>
        /// <returns>swipe if not null</returns>
        public static SwipeInput.Swipe GetSwipe(int _index)
        {
            if (!initilised)//check we have input set up
            {
                throw new InvalidOpEx("Mobile Input not initialised.");
            }

            if (instance.swipeInput == null) //check joystick is not null
            {
                throw new NullReEx("Swipe Input reference not set.");
            }

            //go get swipe from swipe input class
            return instance.swipeInput.GetSwipe(_index);
        }
        #endregion

        #region get flick data
        public static void GetFlickData(out float _flickStrength,out Vector2 _flickDirection)
        {
            if (!initilised)//check we have input set up
            {
                throw new InvalidOpEx("Mobile Input not initialised.");
            }

            if (instance.swipeInput == null) //check joystick is not null
            {
                throw new NullReEx("Swipe Input reference not set.");
            }

            _flickStrength = instance.swipeInput.FlickStrength;
            _flickDirection = instance.swipeInput.FlickDirection;
        }
        #endregion

        #endregion
    }
}

/*
 
     
     
    #region Variables

    #endregion

    #region Start
    void Start()
    {
        
    }
    #endregion

    #region Update
    void Update()
    {
        
    }
    #endregion

    #region Methods

    #endregion
     
     
     
     */
