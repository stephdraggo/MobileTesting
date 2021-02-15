using UnityEngine;
using UnityEngine.EventSystems;

namespace BUTTer.Mobile
{
    public enum JoystickAxis
    {
        None,
        Horizontal,
        Vertical,
    }
    public class JoystickInput : MonoBehaviour, IDragHandler,IEndDragHandler,IPointerDownHandler,IPointerUpHandler
    {
        #region Variables
        /// <summary>
        /// Directional axis and strength represented by joystick
        /// </summary>
        public Vector2 Axis { get; private set; } = Vector2.zero;

        [Header("Visuals")]
        [SerializeField] private RectTransform handle;
        [SerializeField] private RectTransform background;
        [SerializeField, Range(0, 1)] private float deadZone = 0.25f;

        private Vector3 initialPosition = Vector3.zero;
        #endregion

        #region Start
        //sets initial position only
        void Start() => initialPosition = handle.transform.position;
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        public void OnDrag(PointerEventData _eventData)
        {
            //find multiplier for scaling these canvas elements
            float xDif = (background.rect.size.x - handle.rect.size.x) * .5f;
            float yDif = (background.rect.size.y - handle.rect.size.y) * .5f;

            //calculate axis and relative position of handle to background
            Axis = new Vector2(
                (_eventData.position.x - background.position.x) / xDif, //position of handle to background divided by difference
                (_eventData.position.y - background.position.y) / yDif);

            Axis = (Axis.magnitude > 1.0f) ? Axis.normalized : Axis; //make 1 or smaller

            //apply movement to visual handle
            handle.transform.position = new Vector3(
                (Axis.x * xDif) + background.position.x, //do the above thing but backwards and make it local
                (Axis.y * yDif) + background.position.y);

            //apply deadzone
            Axis = (Axis.magnitude < deadZone) ? Vector2.zero : Axis;
        }
        /// <summary>
        /// reset positions and axis
        /// </summary>
        public void OnEndDrag(PointerEventData _eventData)
        {
            Axis = Vector2.zero;
            handle.transform.position = initialPosition;
        }

        //catch for when unity has indigestion
        public void OnPointerDown(PointerEventData _eventData) => OnDrag(_eventData);
        public void OnPointerUp(PointerEventData _eventData) => OnEndDrag(_eventData);
        #endregion
    }
}