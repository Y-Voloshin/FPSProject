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
        //bool intersects

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
        protected override void Update()
        {
            /*
            Vector3 ea = pivot.rotation.eulerAngles;
            ea.x = myTransform.rotation.eulerAngles.x;
            myTransform.rotation = Quaternion.Euler(ea);
            */
            myTransform.rotation = pivot.rotation;

            //myTransform.Translate(pivot.position - myTransform.position);
            //myTransform.LookAt(pivot.position);
            myTransform.position = GetPosAvoidingWalls();

            
        }

        /*
        void MoveIfTooFar()
        {
            float distToPlayer = (Player.position - myTransform.position).magnitude;
            if ( distToPlayer > PivotPlayerDist)
            {
                myTransform.position = pivot.position;
            }
        }
        */

        Vector3 GetPosAvoidingWalls()
        {
            //var dir = Player.pos
            Ray r = new Ray(pivot.position + pivot.forward * PivotPlayerDist, -pivot.forward);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit))
            {
                VGF.DebugTools.DebugLabel.ShowValue(hit.transform.gameObject.name);
                if (hit.distance < Vector3.Distance(pivot.position, Player.position))
                {
                    VGF.DebugTools.DebugLabel.ShowValue(hit.transform.gameObject.name, 1);
                    return hit.point + pivot.forward * 0.2f + pivot.up * 0.2f;
                }
                else
                {
                    VGF.DebugTools.DebugLabel.ShowValue(string.Empty, 1);
                }
            }
            else
            {
                VGF.DebugTools.DebugLabel.ShowValue(string.Empty);
            }
            return pivot.position;
        }
    }
}