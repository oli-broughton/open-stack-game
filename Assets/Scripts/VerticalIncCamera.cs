using UnityEngine;
using OB.Events;
using OB.Extensions;

namespace OB.Game
{
    /// <summary>
    /// Camera that can move vertically by integer amounts.
    /// </summary>
    public class VerticalIncCamera : MonoBehaviour
    {
        [System.Serializable]
        public class ResponseEvents
        {
            public GameEvent IncrementHeight;
            public GameEvent ZoomOut;
        }

        [SerializeField] ResponseEvents _responseEvents;
        [SerializeField] CameraConfig _cameraConfig;

        OrthCameraZoom _cameraZoom;
        int m_currentHeight;
        Vector3 m_targetHeight;
        Vector3 m_smoothFollowVelocity = Vector3.one;

        void Awake()
        {
            _cameraZoom = this.GetComponentInChildrenAssert<OrthCameraZoom>();
        }

		void OnEnable()
		{
            _responseEvents.IncrementHeight.AddListener(IncrementHeight);
            _responseEvents.ZoomOut.AddListener(ZoomOut);
		}

		void OnDisable()
		{
            _responseEvents.IncrementHeight.RemoveListener(IncrementHeight);
            _responseEvents.ZoomOut.RemoveListener(ZoomOut);		
		}

		void Start()
        {
            m_currentHeight = _cameraConfig.StartHeight;
            transform.position = m_targetHeight = (_cameraConfig.StartHeight + _cameraConfig.HeightOffset) * Vector3.up;
        }

        void FixedUpdate()
        {
            transform.position = Vector3.SmoothDamp(transform.position, m_targetHeight, ref m_smoothFollowVelocity, _cameraConfig.HeightIncreaseTime);
        }

        #region Private Methods

        void IncrementHeight()
        {
            m_currentHeight += _cameraConfig.HeightIncrement;
            m_targetHeight = Vector3.up * (m_currentHeight + _cameraConfig.HeightOffset);
        }

        void ZoomOut()
        {
            int heightDiff = m_currentHeight - _cameraConfig.StartHeight;
            _cameraZoom.Zoom(heightDiff);
        }

        #endregion
    }
}