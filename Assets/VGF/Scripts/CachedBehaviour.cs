using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace VGF
{
    public class CachedBehaviour : MonoBehaviour
    {
        public Transform myTransform { get; protected set; }
        public NavMeshAgent myNavMeshAgent { get; protected set; }

        protected virtual void Awake()
        {
            myTransform = transform;
            myNavMeshAgent = GetComponent<NavMeshAgent>();
        }
    }
}