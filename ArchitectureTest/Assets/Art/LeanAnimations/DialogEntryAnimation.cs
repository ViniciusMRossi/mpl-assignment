using UnityEngine;
using UnityEngine.UI;

namespace Art.LeanAnimations
{
    public class DialogEntryAnimation : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image backgroundImage;
        [SerializeField] private GameObject panelsGO;

        [Header("Values")] 
        [SerializeField] private float animTime;

        private float _targetBackgroundAlpha;
        private Color _backgroundColor;

        private void Awake()
        {
            animTime /= 2;
            _backgroundColor = backgroundImage.color;
            _targetBackgroundAlpha = _backgroundColor.a;
            backgroundImage.color = new Color(_backgroundColor.r, _backgroundColor.g, _backgroundColor.b, 0);
        }

        public void Animate()
        {
            LeanTween.value(0, _targetBackgroundAlpha, animTime)
                .setOnUpdate(deltaAlpha =>
                {
                    backgroundImage.color = new Color(_backgroundColor.r, _backgroundColor.g, _backgroundColor.b, deltaAlpha);
                })
                .setEase(LeanTweenType.easeOutCubic)
                .setOnComplete(
                    () => LeanTween.moveLocal(panelsGO, Vector2.zero, animTime)
                        .setEase(LeanTweenType.easeOutCubic));
        }
    }
}