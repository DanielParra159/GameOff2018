using Systems.Common;
using Components.Common;
using Components.Units;
using JetBrains.Annotations;
using Unity.Entities;

namespace Systems.Units
{
    [UsedImplicitly]
    [UpdateAfter(typeof(ProcessDamage))]
    public class CheckForDeathUnits : ComponentSystem
    {
        private ComponentGroup _group;

        protected override void OnCreateManager()
        {
            _group = GetComponentGroup(
                ComponentType.ReadOnly(typeof(Unit)),
                ComponentType.ReadOnly(typeof(Health)),
                ComponentType.Subtractive(typeof(Dying))
            );
        }
                
        protected override void OnUpdate()
        {
            var healths = _group.GetComponentDataArray<Health>();
            var entities = _group.GetEntityArray();
           
            for (var i = 0; i < entities.Length; ++i)
            {
                if (healths[i].Value > 0)
                {
                    continue;
                }

                var entity = entities[i];
                EntityManager.AddComponentData(entity, new Dying());
                EntityManager.SetComponentData(entity, new AnimationData
                {
                    SetIsDying = true
                });
            }
        }
    }
}