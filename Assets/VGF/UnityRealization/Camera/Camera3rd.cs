using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF;

namespace FPSProject.Camera
{
    public class Camera3rd : CachedBehaviour
    {
        [SerializeField]
        Transform Player;
        Transform pivot;
        Vector3 curPos;
        float PivotPlayerDist;

        // Use this for initialization
        void Start()
        {
            pivot = GameObject.Find("CameraPivot").transform;
            if (pivot)
                PivotPlayerDist = pivot.localPosition.magnitude;

            if (!(pivot && Player))
                gameObject.SetActive(false);
        }

        // Update is called once per frame
        protected void Update()
        {
            myTransform.rotation = pivot.rotation;
            myTransform.position = GetPosAvoidingWalls();
        }        

        Vector3 GetPosAvoidingWalls()
        {
            Ray r = new Ray(pivot.position + pivot.forward * PivotPlayerDist, -pivot.forward);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit))
            {
                if (hit.distance < Vector3.Distance(pivot.position, Player.position))
                {
                    return hit.point + pivot.forward * 0.2f + pivot.up * 0.2f;
                }
                else
                {
                }
            }
            else
            {
            }
            return pivot.position;
        }
    }
}