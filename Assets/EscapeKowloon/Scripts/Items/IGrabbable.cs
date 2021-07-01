using UnityEngine;

namespace EscapeKowloon.Scripts.Items
{
    public interface IGrabbable
    {
        bool isGrabbed { get; }
        Transform grabbedTransform { get; }
        Collider[] grabPoints { get; }
    }
}