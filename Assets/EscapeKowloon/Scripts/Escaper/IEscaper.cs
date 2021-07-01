using UniRx;
using UnityEngine;

namespace EscapeKowloon.Scripts.Escaper
{
    public interface IEscaper
    {
        BoolReactiveProperty IsMovable { get; }
    }
}