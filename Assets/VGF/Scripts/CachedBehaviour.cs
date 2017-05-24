using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF
{
    public class CachedBehaviour : MonoBehaviour
    {
        public Transform myTransform { get; protected set; }

        protected virtual void Awake()
        {
            myTransform = transform;
        }

        /*
        // Use this for initialization
        protected virtual void Start()
        {

        }
        */

        // Update is called once per frame
        protected virtual void Update()
        {

        }
    }
}