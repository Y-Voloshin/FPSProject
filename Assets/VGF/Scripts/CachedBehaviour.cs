using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace VGF
{
    public class CachedBehaviour : MonoBehaviour
    {
        /*
        public Transform myTransform { get; protected set; }
        public NavMeshAgent myNavMeshAgent { get; protected set; }
        public GameObject myGO { get; protected set; }
        //*/

        protected Transform myTransform;
        protected NavMeshAgent myNavMeshAgent;
        protected GameObject myGO;
        protected Rigidbody myRigidbody;
        protected Collider myCollider;

        protected virtual void Awake()
        {
            myTransform = transform;
            myNavMeshAgent = GetComponent<NavMeshAgent>();
            myGO = gameObject;
            myRigidbody = GetComponent<Rigidbody>();
            myCollider = GetComponent<Collider>();
        }
    }
}