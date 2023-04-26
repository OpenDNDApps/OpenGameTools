using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OGT
{
    public static class UIExtensions
    {
        public static string GetLocalizedText(this TMP_Text tmp)
        {
            // if defaultAsFail just set key as string.
            // TODO: Get Localization
            return tmp.text;
        }

        public static void SetLocalizedText(this TMP_Text tmp, string localizationKey)
        {
            // if defaultAsFail just set key as string.
            // TODO: Localization
            tmp.text = localizationKey;
        }

        public static void Enable(this CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = GameResources.Settings.UI.EnabledAlpha;
            canvasGroup.Activate();
        }

        public static void Activate(this CanvasGroup canvasGroup)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.gameObject.SetActive(true);
        }

        public static void Disable(this CanvasGroup canvasGroup, bool softDisable = false)
        {
            canvasGroup.alpha = softDisable ? GameResources.Settings.UI.DisabledAlpha : 0f;
            canvasGroup.Deactivate(softDisable);
            if (!softDisable)
            {
                canvasGroup.DOKill();
            }
        }

        public static void Deactivate(this CanvasGroup canvasGroup, bool softDeactivate = false)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            if (!softDeactivate)
            {
                canvasGroup.gameObject.SetActive(false);
            }
        }

        public static void Enable(this TMP_Text tmp)
        {
            tmp.alpha = GameResources.Settings.UI.EnabledAlpha;
            tmp.Activate();
        }

        public static void Activate(this TMP_Text tmp)
        {
            tmp.raycastTarget = false;
            tmp.gameObject.SetActive(true);
        }

        public static void Disable(this TMP_Text tmp, bool softDisable = false)
        {
            tmp.alpha = softDisable ? GameResources.Settings.UI.DisabledAlpha : 0f;
            tmp.Deactivate(softDisable);
            if (!softDisable)
            {
                tmp.DOKill();
            }
        }

        public static void Deactivate(this TMP_Text tmp, bool softDeactivate = false)
        {
            tmp.raycastTarget = false;
            if (!softDeactivate)
            {
                tmp.gameObject.SetActive(false);
            }
        }
        
        public static void SetHighlight(this List<UIButton> buttons, bool isHighlight)
        {
            foreach (UIButton button in buttons)
            {
                button.IsHighlighted = isHighlight;
            }
        }
        
        public static void Enable(this List<UIButton> buttons)
        {
            foreach (UIButton button in buttons)
            {
                button.Enable();
            }
        }
        
        public static void Activate(this List<UIButton> buttons)
        {
            foreach (UIButton button in buttons)
            {
                button.Activate();
            }
        }
        
        public static void Disable(this List<UIButton> buttons, bool softDisable = false)
        {
            foreach (UIButton button in buttons)
            {
                button.Disable(softDisable);
            }
        }
        
        public static void Deactivate(this List<UIButton> buttons)
        {
            foreach (UIButton button in buttons)
            {
                button.Deactivate();
            }
        }

        public static void NotifyToggleOn(this ToggleGroup group, UIToggle toggle)
        {
            group.NotifyToggleOn(toggle.Toggle);
        }

        public static void SetImageAlpha(this Image image, float newAlpha)
        {
            var color = image.color;
            color.a = newAlpha;
            image.color = color;
        }

        public static void Disable(this Image image, bool softDisable = false)
        {
            image.SetImageAlpha(softDisable ? GameResources.Settings.UI.DisabledAlpha : 0);
            image.raycastTarget = false;

            if (!softDisable)
            {
                image.gameObject.SetActive(false);
            }
        }

        public static void Enable(this Image image, bool isRaycastTarget = false)
        {
            image.SetImageAlpha(GameResources.Settings.UI.EnabledAlpha);
            image.raycastTarget = isRaycastTarget;
            image.gameObject.SetActive(true);
        }
    }
}
