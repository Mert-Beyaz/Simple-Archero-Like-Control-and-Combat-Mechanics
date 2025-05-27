using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Archero
{
    public class BaseSkillButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image activeImage;
        [SerializeField] private Image passiveImage;
        public SkillButtonType ButtonType;

        private bool isActive = false;
        public bool IsActive { get => isActive; set => isActive = value; }

        public virtual void OnClick()
        {
            isActive = !isActive;
            activeImage.gameObject.SetActive(isActive);
            passiveImage.gameObject.SetActive(!isActive);
            button.transform.DOScale(0.8f, 0.1f).SetLoops(2, LoopType.Yoyo);
        }
    }

    public enum SkillButtonType
    {
        None,
        AttackSpeed,
        Bounce,
        Burn,
        Multiplication,
        Rage
    }
}