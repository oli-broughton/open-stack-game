using UnityEngine;
using UnityEngine.SceneManagement;
using OB.Events;

namespace OB.Game
{
    /// <summary>
    /// Restarts the game by reloading the main scene
    /// </summary>
    public class SceneReload : MonoBehaviour
    {
        [SerializeField] GameEvent _sceneReloadResponseEvent;
        
        void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        void OnEnable()
        {
            _sceneReloadResponseEvent.AddListener(Reload);
        }

        void OnDisable()
        {
            _sceneReloadResponseEvent.RemoveListener(Reload);
        }

        void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
