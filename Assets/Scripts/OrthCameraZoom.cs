using OB.Extensions;
using UnityEngine;

namespace OB.Game
{
    /// <summary>
    /// Smoothly Zoom an orthographic camera over time.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class OrthCameraZoom : MonoBehaviour
    {
        [SerializeField] CameraConfig _cameraConfig;

        Camera _camera;
        float _targetSize;
        float _smoothZoomVelocity;

        void Awake()
        {
            _camera = this.GetComponentAssert<Camera>();
            _targetSize = _camera.orthographicSize;
        }

        void FixedUpdate()
        {
            _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, _targetSize, ref _smoothZoomVelocity, _cameraConfig.ZoomIncreaseTime);
        }

        public void Zoom(int size)
        {
            _targetSize = Mathf.Clamp(size, _cameraConfig.MinZoom, _cameraConfig.MaxZoom);
        }
    }
}
