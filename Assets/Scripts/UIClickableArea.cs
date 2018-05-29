using OB.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OB.UI
{
    /// <summary>
    /// Invokes a provided event when the attached UI element is clicked.
    /// </summary>
    public class UIClickableArea : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] GameEvent _onAreaClickedEvent;

        void Awake()
        {
            Cursor.visible = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _onAreaClickedEvent.Invoke();
        }
    }

}