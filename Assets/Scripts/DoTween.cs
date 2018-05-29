using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace OB.Game
{
    /// <summary>
    /// Initialise the DoTween library
    /// </summary>
    public class DoTween : MonoBehaviour
    {
        void Start()
        {
            DOTween.Init(true);
        }

		void OnDestroy()
        {
            DOTween.Clear(true);
        }

    }
}
