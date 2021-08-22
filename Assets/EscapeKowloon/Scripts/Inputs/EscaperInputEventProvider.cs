using System;
using EscapeKowloon.Scripts.Utility;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace EscapeKowloon.Scripts.Inputs
{
    public class EscaperInputEventProvider :  IEscaperInputEventProvider
    {
        public IObservable<Unit> OnPressRightUseItemButton =>
            OVRInputRx.OnPressRawButtonAsObservable(OVRInput.RawButton.RIndexTrigger);

        public IObservable<Unit> OnPressLeftUseItemButton =>
            OVRInputRx.OnPressRawButtonAsObservable(OVRInput.RawButton.LIndexTrigger);

        public IObservable<Unit> OnPressDownTeleportButton =>
            OVRInputRx.OnPressRawButtonDownAsObservable(OVRInput.RawButton.RThumbstickDown);

        public IObservable<Unit> OnPressUpTeleportButton =>
            OVRInputRx.OnPressRawButtonUpAsObservable(OVRInput.RawButton.RThumbstickDown);

        public IObservable<Unit> OnMoveForward =>
            Observable.EveryUpdate().Where(_ => Input.GetKey(KeyCode.W)).AsSingleUnitObservable();
    }
}