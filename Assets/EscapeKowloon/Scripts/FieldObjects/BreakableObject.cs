using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EscapeKowloon.Scripts.FieldObjects
{
    public class BreakableObject : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _breakEffect;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Collider _collider;

        private AudioSource _audioSource;
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        [Button]
        public async UniTask Break()
        {
            _renderer.enabled = false;
            _collider.enabled = false;
            _breakEffect.Play();
            _audioSource?.Play();
            await _breakEffect.GetAsyncParticleSystemStoppedTrigger().OnParticleSystemStoppedAsync();
            Destroy(gameObject);
        }
    }
}