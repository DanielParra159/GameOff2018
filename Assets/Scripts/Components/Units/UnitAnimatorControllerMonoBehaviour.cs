using Unity.Entities;
using UnityEngine;

namespace Components.Units
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(GameObjectEntity))]
    public class UnitAnimatorControllerMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObjectEntity _gameObjectEntity;

        private int _walkingHash;

        private void Awake()
        {
            _walkingHash = Animator.StringToHash("Walking");
        }

        private void LateUpdate()
        {
            var entityManager = World.Active.GetExistingManager<EntityManager>();
            var entity = _gameObjectEntity.Entity;
            var animationData = entityManager.GetComponentData<AnimationData>(entity);
            
            Walking(animationData.IsWaling);
        }

        public void DoDamageEvent()
        {
            var entityManager = World.Active.GetExistingManager<EntityManager>();
            var entity = _gameObjectEntity.Entity;
            var attack = entityManager.GetComponentData<Attack>(entity);
            attack.IsReady = true;
            entityManager.SetComponentData(entity, attack);
        }

        private void Walking(bool isWalking)
        {
            _animator.SetBool(_walkingHash, isWalking);
        }
    }
}