using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace VGF.Action3d.Level
{
    public class LevelBoundsBehaviour : MonoBehaviour
    {
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
        }

        public static Vector3 GetPointWithinBounds(Vector3 center, float range = 30)
        {
            if (instanceExists)
            {
                for (int i = 0; i < 30; i++) // navMesh tries from unity tutorial
                {
                    for (int j = 0; j < 10; j++)// bounds tries
                    {
                        Vector3 randomPoint = center + Random.insideUnitSphere * range;
                        //for saving time. If point is out of borders, that means that center is closer to borders thatn range
                        //let's try just to invert direction of point relative to center
                        //this fails only if range is bigger than distance between bounds
                        //or we are in the corner, where two borders are to close to each other
                        if (instance.PointIsOutOfBorders(randomPoint))
                            randomPoint = center * 2 - randomPoint;
                        if (!instance.PointIsOutOfBorders(randomPoint))
                        {
                            NavMeshHit hit;
                            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
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

        bool PointIsOutOfBorders(Vector3 point)
        {
            return point.x < XMin || point.x > Xmax
                || point.y < Ymin || point.y > Ymax
                || point.z < ZMin || point.z > ZMax;
        }
    }
}