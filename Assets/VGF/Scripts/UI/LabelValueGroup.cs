using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VGF;

namespace VGF.UintyUI
{
    public class LabelValueGroup : CachedBehaviour
    {
        [SerializeField]
        protected Text Label, Value;
        [SerializeField]
        protected bool HideIfZeroIntValue = false;

        public void SetValue(int value)
        {
            if (HideIfZeroIntValue)
            {
                if (value == 0)
                {
                    Label.gameObject.TrySetActive(false);
                    Value.gameObject.TrySetActive(false);
                    return;
                }
                else
                {
                    Label.gameObject.TrySetActive(true);
                    Value.gameObject.TrySetActive(true);
                }
            }
            Value.TrySetInt(value);
        }

        public void SetValue(string value)
        {
            Value.TrySetText(value);
        }
    }
    public static class LabelValueGroupExtensions
    { 
        public static void SetValueSafe(this LabelValueGroup lvg, int value)
        {
            if (lvg == null)
                return;
            lvg.SetValue(value);
        }

        public static void SetValueSafe(this LabelValueGroup lvg, string value)
        {
            if (lvg == null)
                return;
            lvg.SetValue(value);
        }
    }
}