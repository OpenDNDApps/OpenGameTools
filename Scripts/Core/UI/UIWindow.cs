using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGDevs
{
    public class UIWindow : BaseUIScript
    {
        [SerializeField] private UIButton m_closeButton;

        protected override void Awake()
        {
            base.Awake();
            
            m_closeButton.AddClickEvent(Hide);
        }
    }
}
