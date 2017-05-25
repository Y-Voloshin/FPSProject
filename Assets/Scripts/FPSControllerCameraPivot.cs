using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace FPSProject.StandartAssetsOverride
{
    public class FPSControllerCameraPivot : RigidbodyFirstPersonController
    {
        [SerializeField]
        protected Transform CameraPivot;

        protected override Transform CamTransform
        {
            get
            {
                return CameraPivot;
            }
        }

        // Use this for initialization
        void Start()
        {
            base.Start();
            if (CameraPivot == null)
                CameraPivot = base.cam.transform;
        }

        // Update is called once per frame
        void Update()
        {
            base.Update();
        }
    }
}