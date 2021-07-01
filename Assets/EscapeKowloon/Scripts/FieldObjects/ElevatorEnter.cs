using EscapeKowloon.Scripts.SceneTransitionManager;
using SceneTransitionManager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace EscapeKowloon.Scripts.FieldObjects
{
    [RequireComponent(typeof(Collider))]
    public class ElevatorEnter : MonoBehaviour
    {
        [SerializeField] private Elevator _elevator;
        [SerializeField] private SceneType NextScene = SceneType.Roof;
        private void Start()
        {
            var col = GetComponent<Collider>();
            col.OnTriggerEnterAsObservable()
                .Where(c=> c.CompareTag("Player"))
                .Subscribe(_ =>
                {
                    _elevator.DoorsClosing();
                    SceneLoader.LoadScene(NextScene.ToString(), __ => {});
                });
        }
    }
}