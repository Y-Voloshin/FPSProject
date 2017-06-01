using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF
{
    /// <summary>
    /// Class with methods for static cursor
    /// </summary>
    public static  class MyCursor
    {
        public static void SetHide (bool hide)
        {
            
            Cursor.lockState = hide? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !hide;
        }
    }
}