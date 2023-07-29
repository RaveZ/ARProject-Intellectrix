using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIPopupManager : MonoBehaviour
    {
        public UnityEvent OnShowPopup = null;
        public float animationDuration = 0.2f;
        public Ease animationEase = Ease.OutCubic;
        public bool AffectTouchSwipe = true;

        private Graphic[] graphics;

        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }



        public void ShowPopup()
        {
            if (AffectTouchSwipe)
            {
                //DisableTouchSwipe();
            }
            OnShowPopup?.Invoke();
            graphics = GetComponentsInChildren<Graphic>();

            // Fade in the popup and child objects
            foreach (var graphic in graphics)
            {
                var button = graphic.GetComponent<Button>();
                if (button != null)
                {
                    button.interactable = true;
                }
                graphic.DOFade(1, animationDuration);
            }

        }

        public void HidePopup()
        {
            if (AffectTouchSwipe)
            {
                //EnableTouchSwipe();
            }
            graphics = GetComponentsInChildren<Graphic>();
            // Fade out the popup and child objects
            foreach (var graphic in graphics)
            {

                var button = graphic.GetComponent<Button>();
                if (button != null)
                {
                    button.interactable = false;
                }
                graphic.DOFade(0, animationDuration)
                    .OnComplete(() => {
                        gameObject.SetActive(false);
                    }); ;

            }

        }

        

        /*private void EnableTouchSwipe()
        {
            mainCamera.GetComponent<SwipeTouchManager>().SetCanSwipeTouch(true);
        }

        private void DisableTouchSwipe()
        {
            mainCamera.GetComponent<SwipeTouchManager>().SetCanSwipeTouch(false);

        }*/
    }
}