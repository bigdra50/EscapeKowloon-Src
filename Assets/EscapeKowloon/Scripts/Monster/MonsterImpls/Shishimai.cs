using EscapeKowloon.Scripts.NpcActions;
using UniRx;

namespace EscapeKowloon.Scripts.Monster.MonsterImpls
{
    public class Shishimai : MonsterEntity, IMonster
    {
        public bool IsAttacking { get; } = true;

        private void Start()
        {
            InitializeActionEffect();
            UpdateState();
        }

        private void InitializeActionEffect()
        {
            if (!TryGetComponent<INpcActionsRepository<NpcState>>(out var actionsRepository)) return;

            var actionEffector = new MonsterActionableEffector(this, actionsRepository);
            _currentState.Subscribe(actionEffector.ApplyEffect).AddTo(this);
        }

        private void UpdateState()
        {
            this.ObserveEveryValueChanged(x => x._currentTarget)
                .Subscribe(target =>
                {
                    if (target != null)
                    {
                        AddState(NpcState.FoundTarget);
                        RemoveState(NpcState.Vigilance);
                    }
                    else if (target == null)
                    {
                        AddState(NpcState.Vigilance);
                        RemoveState(NpcState.FoundTarget);
                    }
                });
        }
    }
}