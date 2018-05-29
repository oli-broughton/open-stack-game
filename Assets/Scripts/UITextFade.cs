using DG.Tweening;
using OB.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace OB.UI
{
    /// <summary>
    /// Fade in and out UI Text fields.
    /// </summary>
    [RequireComponent(
    typeof(Text))]
    public class UITextFade : MonoBehaviour, IUIFade
    {
        [SerializeField] float _fadeTime = 0;

        Text m_text;

        void Awake()
        {
            m_text = this.GetComponentAssert<Text>();
        }

        public void FadeIn()
        {
            m_text.DOFade(1, _fadeTime);
        }

        public void FadeOut()
        {
            m_text.DOFade(0, _fadeTime);
        }
    }
}