using Components.Common;
using Components.Units;
using JetBrains.Annotations;
using Unity.Entities;
using UnityEngine;

namespace Systems.Units
{
    [UpdateAfter(typeof(TargetDetection))]
    [UsedImplicitly]
    public class ProcessTarget : ComponentSystem
    {
        private ComponentGroup _unitInfoGroup;
        
        protected override void OnCreateManager()
        {
            _unitInfoGroup = GetComponentGroup(
                ComponentType.ReadOnly(typeof(Target)),
                typeof(Attack)
            );
        }
        
        protected override void OnUpdate()
        {
            var targets = _unitInfoGroup.GetComponentDataArray<Target>();
            var attacks = _unitInfoGroup.GetComponentDataArray<Attack>();
            var entities = _unitInfoGroup.GetEntityArray();
            
            for (var i = 0; i < entities.Length; ++i)
            {
                var target = targets[i];
                var attack = attacks[i];

                if (attack.IsReady)
                {
                    attack.IsReady = false;
                    var damageBuffer = EntityManager.GetBuffer<Damage>(target.Entity);
                    damageBuffer.Add(new Damage
                    {
                        Value = attacks[i].Damage
                    });
                }

                attacks[i] = attack;
                
                PostUpdateCommands.RemoveComponent<Target>(entities[i]); 
            }
        }
    }
}