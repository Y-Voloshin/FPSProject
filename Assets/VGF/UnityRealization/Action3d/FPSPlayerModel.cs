using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d
{
    [System.Serializable]
    public class FPSPlayerModel
    {
        public float ForwardSpeed = 6f;   // Speed when walking forward
        //public float BackwardSpeed = 4.0f;  // Speed when walking backwards
        public float StrafeSpeed = 6f;    // Speed when walking sideways
        public float RunMultiplier = 2.0f;   // Speed when sprinting
        public float JumpForce = 4;
    }
}