using UnityEngine;
using NullRE = System.NullReferenceException;
using InvalidOE = System.InvalidOperationException;

namespace BUTTer.Mobile
{
    public class MobileInput : MonoBehaviour
    {
        #region Variables
        public static bool initilised => instance != null;

        private static MobileInput instance = null;

        [SerializeField] private JoystickInput joystickInput;
        #endregion

        #region Methods
        /// <summary>
        /// If the system is not set up, instantiate mobile input prefab and assign static reference
        /// </summary>
        public static void Initialise()
        {
            if (initilised) //if already initialised
            {
                throw new InvalidOE("Mobile Input already initialised."); //throw error
            }

            MobileInput prefabInstance = Resources.Load<MobileInput>("Mobile Input Prefab"); //load prefab
            instance = Instantiate(prefabInstance); //instantiate into scene and set reference

            instance.gameObject.name = "Mobile Input"; //foolproof naming
            DontDestroyOnLoad(instance.gameObject); //make permanent throughout scenes
        }
        /// <summary>
        /// return joystick axis value if valid
        /// </summary>
        /// <param name="_axis">the axis to get value of (x or y)</param>
        /// <returns>valid joystick axis value</returns>
        public static float GetJoystickAxis(JoystickAxis _axis)
        {
            if (!initilised)//check we have input set up
            {
                throw new InvalidOE("Mobile Input not initialised.");
            }

            if (instance.joystickInput == null) //check joystick is not null
            {
                throw new NullRE("Joystick Input reference not set.");
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
