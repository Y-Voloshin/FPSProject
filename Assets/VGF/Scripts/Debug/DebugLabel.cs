using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VGF.UintyUI;

namespace VGF.DebugTools
{
    public class DebugLabel : MonoBehaviour
    {
        [SerializeField]
        Text[] Labels = new Text[10];

        static DebugLabel instance;

        // Use this for initialization
        void Start()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public static void ShowValue(object value, int labelId = 0)
        {
            if (instance == null)
                return;
            if (instance.Labels.Length == 0)
                return;
            if (labelId < 0 || labelId > instance.Labels.Length)
                labelId = 0;
            instance.Labels[labelId].TrySetText(value == null? "null" : value.ToString());
        }
    }
}