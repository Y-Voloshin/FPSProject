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

        static Vector3 vec3Zero = Vector3.zero;
        static Quaternion qIdent = Quaternion.identity;
        public static Vector3 Vector3Zero { get { return vec3Zero; } }
        public static Quaternion QuaternionIdentity { get { return qIdent; } }

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

        public void Hide()
        {
            myGO.SetActive(false);
        }

        public void Show()
        {
            myGO.SetActive(true);
        }

        public void OrientAndAttach(Transform parent)
        {
            if (parent)
            {
                myTransform.rotation = parent.rotation;
                myTransform.position = parent.position;
                myTransform.SetParent(parent);
            }
            else
            {
                myTransform.localPosition = vec3Zero;
                myTransform.localRotation = qIdent;
            }
        }
    }
}