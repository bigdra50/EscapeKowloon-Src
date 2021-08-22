using System;
using UniRx;

namespace EscapeKowloon.Scripts.Inputs
{
    public interface IEscaperInputEventProvider
    {
        IObservable<Unit> OnPressRightUseItemButton { get; }
        IObservable<Unit> OnPressLeftUseItemButton { get; }
        IObservable<Unit> OnPressDownTeleportButton { get; }
        IObservable<Unit> OnPressUpTeleportButton { get; }
        IObservable<Unit> OnMoveForward { get; }
    }
}