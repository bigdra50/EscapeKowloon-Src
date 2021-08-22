using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace EscapeKowloon.Scripts.Escaper
{
    public class LinearRotator : MonoBehaviour
    {
        public bool _enableRotation = true;

        public float RotationAmount = 1.5f;
        private float RotationScaleMultiplier = 1.0f;
        private float SimulationRate = 60f;
        private bool prevHatLeft = false;
        private bool prevHatRight = false;
        public float RotationRatchet = 45.0f;
        private float buttonRotation = 0f;

        private void Start()
        {
        }

        private void Update()
        {
            if (_enableRotation)
            {
                Vector3 euler = transform.rotation.eulerAngles;
                float rotateInfluence = SimulationRate * Time.deltaTime * RotationAmount * RotationScaleMultiplier;

                bool curHatLeft = OVRInput.Get(OVRInput.Button.PrimaryShoulder);

                if (curHatLeft && !prevHatLeft)
                    euler.y -= RotationRatchet;

                prevHatLeft = curHatLeft;

                bool curHatRight = OVRInput.Get(OVRInput.Button.SecondaryShoulder);

                if (curHatRight && !prevHatRight)
                    euler.y += RotationRatchet;

                prevHatRight = curHatRight;

                euler.y += buttonRotation;
                buttonRotation = 0f;
                Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);


                euler.y += secondaryAxis.x * rotateInfluence;
                transform.rotation = Quaternion.Euler(euler);
            }
        }
    }
}