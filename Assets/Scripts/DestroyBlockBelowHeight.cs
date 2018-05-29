using UnityEngine;

namespace OB.Game
{
    /// <summary>
    /// Destorys a block when it is below a specified height
    /// </summary>
    public class DestroyBlockBelowHeight : MonoBehaviour
    {
        [SerializeField] BlockConfig _blockConfig;

        void Update()
        {
            if (transform.position.y < _blockConfig.DestroyBlocksBelowHeight)
            {
                Destroy(gameObject);
            }
        }
    }
}