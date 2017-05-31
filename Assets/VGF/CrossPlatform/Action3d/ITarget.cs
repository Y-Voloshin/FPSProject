using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d
{
    /// <summary>
    /// Interface for objects that can be targets for AI logic: characters, places, things
    /// </summary>
    public interface ITarget
    {
        /// <summary>
        /// Checks if the target can be reached (character is alive, gameobject is active etc.)
        /// </summary>
        bool IsAvailable { get; }
        Vector3 Position { get; }
    }
}