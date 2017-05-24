using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VGF.UintyUI
{

    public static class ButtonExtensions
    {
        public static void SetActivity(this Button b, bool active)
        {
            if (b == null)
                return;
            //b.enabled = active;
            b.gameObject.SetActive(active);
        }
    }

    public static class TextExtensions
    {
        //Caching most frequent int-to-strings to save memory and preformance - strings are heavy :o)
        static List<string> PositiveInts = FillPositiveInts(),
            NegativeInts = FillNegativeInts();

        static List<string> FillPositiveInts()
        {
            var result = new List<string>();
            for (int i = 0; i < 100; i++)
                result.Add(i.ToString());
            return result;
        }

        static List<string> FillNegativeInts()
        {
            var result = new List<string>();
            for (int i = 0; i < 100; i++)
                result.Add((-i).ToString());
            return result;
        }

        public static void TrySetText(this Text t, string text)
        {
            if (t == null)
                return;
            t.text = text;
        }

        public static void TryClear(this Text t)
        {
            if (t == null)
                return;
            t.text = string.Empty;
        }

        /// <summary>
        /// Simplifies putting int value to text label
        /// </summary>
        /// <param name="t"></param>
        /// <param name="value"></param>
        public static void TrySetInt(this Text t, int value)
        {
            if (t == null)
                return;

            if (value < 0)
            {
                value = -value;
                if (value < NegativeInts.Count)
                    t.text = NegativeInts[value];
                else
                    t.text = (-value).ToString();
            }
            else
            {
                if (value < PositiveInts.Count)
                    t.text = PositiveInts[value];
                else
                    t.text = value.ToString();
            }
        }

        public static string GetCachedIntString(this int value)
        {
            if (value < 0)
            {
                value = -value;
                if (value < NegativeInts.Count)
                    return NegativeInts[value];
                else
                    return (-value).ToString();
            }
            else
            {
                if (value < PositiveInts.Count)
                    return PositiveInts[value];
                else
                    return value.ToString();
            }
        }
    }

    public static class GameObjectExtensions
    {
        public static void TrySetActive(this GameObject go, bool active)
        {
            if (go == null)
                return;
            if (go.activeSelf != active)
                go.SetActive(active);
        }
    }
}