using TMPro;
using UnityEngine;

namespace OGT.Examples
{
    // ReSharper disable once InconsistentNaming
    public class CoreUI_CustomUIWindow_Responsive : UITabWindow
    {
        [Header("CoreUI_CustomUIWindow")]
        [SerializeField] private TMP_Text m_title;
        [SerializeField] private TMP_Text m_message;

        protected override void OnInit()
        {
            base.OnInit();
        }
    }  
}