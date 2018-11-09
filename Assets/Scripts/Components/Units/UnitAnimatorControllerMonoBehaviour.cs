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

        private int _attackHash;
        private int _walkingHash;

        private void Awake()
        {
            _attackHash = Animator.StringToHash("Attack");
            _walkingHash = Animator.StringToHash("Walking");
        }

        private void LateUpdate()
        {
            var entityManager = World.Active.GetExistingManager<EntityManager>();
            var entity = _gameObjectEntity.Entity;
            var animationData = entityManager.GetComponentData<AnimationData>(entity);
            
            Walking(animationData.IsWaling);
            if (animationData.StartAttack)
            {
                //animationData.StartAttack = false;
                //entityManager.SetComponentData(entity, animationData);
                Attack();
            }
        }

        private void Walking(bool isWalking)
        {
            _animator.SetBool(_walkingHash, isWalking);
        }

        private void Attack()
        {
            _animator.SetTrigger(_attackHash);
        }
    }
}