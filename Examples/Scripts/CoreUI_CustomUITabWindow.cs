using TMPro;
using UnityEngine;

namespace OGT.Examples
{
    public class CoreUI_CustomUITabWindow : UITabWindow
    {
        [Header("CoreUI_CustomUIWindow")]
        [SerializeField] private TMP_Text m_title;
        [SerializeField] private TMP_Text m_message;

        protected override void OnInit()
        {
            base.OnInit();
            m_title.SetLocalizedText("My Custom Tab Window");
            m_message.SetLocalizedText("This window was created in runtime and is using UI Animations.");
        }
    }  
}