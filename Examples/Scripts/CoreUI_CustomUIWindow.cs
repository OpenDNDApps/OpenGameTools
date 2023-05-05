using TMPro;
using UnityEngine;

namespace OGT.Examples
{
    public class CoreUI_CustomUIWindow : UIWindow
    {
        [Header("CoreUI_CustomUIWindow")] [SerializeField]
        private TMP_Text m_title;

        [SerializeField] private TMP_Text m_message;

        [SerializeField] private UIToggle m_buttonActivatorToggle;
        [SerializeField] private UIButton m_button;

        protected override void OnInit()
        {
            base.OnInit();
            m_title.SetLocalizedText("My Custom Window");
            m_message.SetLocalizedText("This window was created at runtime and is using UI Animations.");

            m_button.OnClick += () => Debug.Log("Button Clicked!");

            // Optionally: A button can subscribe to a Func<bool>() in order to be clickable. 
            m_button.Conditionable = () => m_buttonActivatorToggle.IsOn;

            m_buttonActivatorToggle.SetLabel("Button Activator");
        }

        private void AlternativeButtonActivator()
        {
            m_buttonActivatorToggle.OnValueChanged += newValue =>
            {
                if (newValue)
                {
                    // This can be used in any UIItem. The item is turned off.
                    m_button.Enable();
                }
                else
                {
                    // This can be used in any UIItem, if the parameter is true, is a soft disable and the item gets disabled and transparent.
                    m_button.Disable(true);
                }
            };
        }
    }
}