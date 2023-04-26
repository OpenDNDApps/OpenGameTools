using System;
using System.Collections.Generic;

namespace OGT
{
    public static class UITabSectionExtensions
    {
        public static UITabSection GetByID(this List<UITabSection> list, string id)
        {
            return list.Find(item => item.ID.Equals(id));
        }
        
        public static void Init(this List<UITabSection> list, UITabWindow owner, Action<UITabSection> onTabButtonClick)
        {
            foreach (UITabSection tab in list)
            {
                tab.Init(owner, onTabButtonClick);
            }
        }
    }
}