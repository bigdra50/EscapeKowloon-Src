using UniRx;

namespace EscapeKowloon.Scripts.Items
{
    public interface IItem
    {
        IReadOnlyReactiveProperty<bool> IsAvailable { get; }
        void Use();
    }
}