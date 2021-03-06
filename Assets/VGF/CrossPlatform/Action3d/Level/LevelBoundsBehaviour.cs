﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace VGF.Action3d.Level
{
    public class LevelBoundsBehaviour : MonoBehaviour
    {
        public static readonly Vector3 ZeroV3 = new Vector3(0, 0, 0);

        static LevelBoundsBehaviour instance;
        static bool instanceExists = false;
        float XMin, Xmax, Ymin, Ymax, ZMin, ZMax;

        private void Awake()
        {
            if (instance)
            {
                Destroy(this.gameObject);
                return;
            }
            instance = this;
            instanceExists = true;
            SetFloatsFromBounds();

            Random.InitState(System.DateTime.Now.Millisecond * System.DateTime.Now.Second);
        }

        void SetFloatsFromBounds()
        {
            Vector3 pos = transform.position;
            Vector3 halfScale = transform.lossyScale * 0.5f;

            XMin = pos.x - halfScale.x;
            Xmax = pos.x + halfScale.x;
            Ymin = pos.y - halfScale.y;
            Ymax = pos.y + halfScale.y;
            ZMin = pos.z - halfScale.z;
            ZMax = pos.z + halfScale.z;

            //Debug.Log(GetPointWithinBounds(transform.position, 5));
        }

        /// <summary>
        /// Returns the Vector3 point available to go to
        /// </summary>
        /// <param name="center">center of search, usually the position of an NPC we're looking point for</param>
        /// <param name="range">Max fistance from center to result point</param>
        /// </param>
        /// <returns></returns>
        public static Vector3 GetPointWithinBounds(Vector3 center, float range = 30)
        {
            //I did theis way for not checking (forward * relativeForwarStep == Vector3.Zero) when there is no forward offset
            if (instanceExists)
            {
                for (int i = 0; i < 30; i++) // navMesh tries from unity tutorial
                {
                    for (int j = 0; j < 10; j++)// bounds tries
                    {
                        //The only line that differs
                        Vector3 randomPoint = center + Random.insideUnitSphere  * range;
                        //for saving time. If point is out of borders, that means that center is closer to borders thatn range
                        //let's try just to invert direction of point relative to center
                        //this fails only if range is bigger than distance between bounds
                        //or we are in the corner, where two borders are to close to each other
                        if (instance.PointIsOutOfBorders(randomPoint))
                            randomPoint = center * 2 - randomPoint;
                        if (!instance.PointIsOutOfBorders(randomPoint))
                        {
                            NavMeshHit hit;
                            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas))
                            {
                                return hit.position;
                            }
                        }
                    }
                }
            }
            //If can't find a new point, just return initial
            Debug.Log("BoundsBehaviour can't find new point to go");
            return center;
        }

        /// <summary>
        /// Returns the Vector3 point available to go to
        /// </summary>
        /// <param name="center">center of search, usually the position of an NPC we're looking point for</param>
        /// <param name="range">Max fistance from center to result point</param>
        /// <param name="forward">An NPC should move mostly FORWARD. Center and range give us a sphere, which means ANY direction.</param>
        /// <param name="relativeForwardStep">So we just move the sphere center: result center is: center + forward * range * relativeForwardStep</param>
        /// <returns></returns>
        public static Vector3 GetPointWithinBounds(Vector3 center,  Vector3 forward, float relativeForwardStep, float range = 30)
        {
            if (instanceExists)
            {
                for (int i = 0; i < 30; i++) // navMesh tries from unity tutorial
                {
                    for (int j = 0; j < 10; j++)// bounds tries
                    {
                        //The only line that differs
                        Vector3 randomPoint = center + (Random.insideUnitSphere + forward * relativeForwardStep) * range;
                        //for saving time. If point is out of borders, that means that center is closer to borders thatn range
                        //let's try just to invert direction of point relative to center
                        //this fails only if range is bigger than distance between bounds
                        //or we are in the corner, where two borders are to close to each other
                        if (instance.PointIsOutOfBorders(randomPoint))
                            randomPoint = center * 2 - randomPoint;
                        if (!instance.PointIsOutOfBorders(randomPoint))
                        {
                            NavMeshHit hit;
                            //NavMeshDataInstance
                            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas))
                            {
                                return hit.position;
                            }
                            //else
                            //    Debug.Log(hit.position);
                        }
                    }
                }
            }
            //If can't find a new point, just return initial
            Debug.Log("BoundsBehaviour can't find new point to go");
            return center;
        }
        
        bool PointIsOutOfBorders(Vector3 point)
        {
            return point.x < XMin || point.x > Xmax
                || point.y < Ymin || point.y > Ymax
                || point.z < ZMin || point.z > ZMax;
        }
    }
}