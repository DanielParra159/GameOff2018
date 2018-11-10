using System.Diagnostics.CodeAnalysis;
using Components.Common;
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
        private int _deathHash;

        private void Awake()
        {
            _walkingHash = Animator.StringToHash("Walking");
            _deathHash = Animator.StringToHash("Death");
        }

        private void LateUpdate()
        {
            var entityManager = World.Active.GetExistingManager<EntityManager>();
            var entity = _gameObjectEntity.Entity;
            var animationData = entityManager.GetComponentData<AnimationData>(entity);

            if (animationData.SetIsDying)
            {
                SetDyingAnimation(entityManager, entity, animationData);
                return;
            }
            Walking(animationData.SetIsWaling);
        }

        private void SetDyingAnimation(EntityManager entityManager, Entity entity, AnimationData animationData)
        {
            _animator.SetTrigger(_deathHash);
            animationData.SetIsDying = false;
            entityManager.SetComponentData(entity, animationData);
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private void DoDamageEvent()
        {
            var entityManager = World.Active.GetExistingManager<EntityManager>();
            var entity = _gameObjectEntity.Entity;
            var attack = entityManager.GetComponentData<Attack>(entity);
            attack.IsReady = true;
            entityManager.SetComponentData(entity, attack);
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private void DeathAnimationEndedEvent()
        {
            var entityManager = World.Active.GetExistingManager<EntityManager>();
            var entity = _gameObjectEntity.Entity;
            entityManager.AddComponentData(entity, new Dead());
        }

        private void Walking(bool isWalking)
        {
            _animator.SetBool(_walkingHash, isWalking);
        }
    }
}