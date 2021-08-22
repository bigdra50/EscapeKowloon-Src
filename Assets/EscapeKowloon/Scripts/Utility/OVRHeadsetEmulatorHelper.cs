/************************************************************************************
Copyright (c) 2019 KOGA Mitsuhiro

This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
************************************************************************************/

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OVRHeadsetEmulator))]
public class OVRHeadsetEmulatorHelper : MonoBehaviour
{
    [SerializeField] private OVRHeadsetEmulator emulator;
    [SerializeField] private Transform head;
    [SerializeField] private float forwardScale = 1f;

    private OVRManager _manager;
    private bool _emulatorHasInitialized = false;

    private void Reset()
    {
        emulator = GetComponent<OVRHeadsetEmulator>();
        head = FindMainCamera().transform;
    }

    private bool MoveForward()
    {
        return Input.GetMouseButton(1);
    }

    private void Update()
    {
        if (!_emulatorHasInitialized)
        {
            if (OVRManager.OVRManagerinitialized)
            {
                _manager = OVRManager.instance;
                _emulatorHasInitialized = true;
            }
            else
            {
                return;
            }
        }

        bool emulationActivated = IsEmulationActivated();
        if (emulationActivated)
        {
            if (emulator.resetHmdPoseByMiddleMouseButton && Input.GetMouseButton(2))
            {
                return;
            }

            Vector3 emulatedTranslation = _manager.headPoseRelativeOffsetTranslation;
            Vector3 forward = head.forward.normalized * (Time.deltaTime * forwardScale);
            forward.y = 0;

            if (MoveForward())
            {
                // move forward
                emulatedTranslation -= forward;
                _manager.headPoseRelativeOffsetTranslation = emulatedTranslation;
            }
        }
    }

    private bool IsEmulationActivated()
    {
        if (emulator.opMode == OVRHeadsetEmulator.OpMode.Off)
        {
            return false;
        }

        if (emulator.opMode == OVRHeadsetEmulator.OpMode.EditorOnly && !Application.isEditor)
        {
            return false;
        }

        foreach (KeyCode key in emulator.activateKeys)
        {
            if (Input.GetKey(key))
                return true;
        }

        return false;
        }

    private Camera FindMainCamera()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("MainCamera");
        List<Camera> cameras = new List<Camera>(4);
        foreach (GameObject obj in objects)
        {
            Camera camera = obj.GetComponent<Camera>();
            if (camera != null && camera.enabled)
            {
                OVRCameraRig cameraRig = camera.GetComponentInParent<OVRCameraRig>();
                if (cameraRig != null && cameraRig.trackingSpace != null)
                {
                    cameras.Add(camera);
                }
            }
        }

        if (cameras.Count == 0)
        {
            return Camera.main; // pick one of the cameras which tagged as "MainCamera"
        }
        else if (cameras.Count == 1)
        {
            return cameras[0];
        }
        else
        {
            // return the camera with least depth
            cameras.Sort((Camera c0, Camera c1) =>
            {
                return c0.depth < c1.depth ? -1 : (c0.depth > c1.depth ? 1 : 0);
            });
            return cameras[0];
        }
    }
}