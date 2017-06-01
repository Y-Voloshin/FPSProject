using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF
{
    /// <summary>
    /// Behaviour used for initialization of all the static classes
    /// </summary>
    public class StaticSystemClassesInitializer : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            Action3d.GameManager.Init();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}