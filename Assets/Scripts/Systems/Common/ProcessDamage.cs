using Components.Common;
using JetBrains.Annotations;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace Systems.Common
{
    [UsedImplicitly]
    public class ProcessDamage : JobComponentSystem
    {
        [BurstCompile]
        private struct ProcessDamageJob : IJobParallelFor
        {
            public ComponentDataArray<Health> Healths;
            [ReadOnly] public BufferArray<Damage> Damages;

            public void Execute(int i)
            {
                var damagesBuffer = Damages[i];
                var totalDamage = 0.0f;
                for (int j = 0; j < damagesBuffer.Length; ++j)
                {
                    totalDamage += damagesBuffer[j].Value;
                }

                var newHealth = math.max(0, Healths[i].Value - totalDamage);
                Healths[i] = new Health
                {
                    Value =  newHealth
                };

                damagesBuffer.Clear();
            }
        }

        ComponentGroup _processDamage;

        protected override void OnCreateManager()
        {
            _processDamage = GetComponentGroup(
                typeof(Health),
                ComponentType.ReadOnly(typeof(Damage))
                );
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var processDamageJob = new ProcessDamageJob
            {
                Healths = _processDamage.GetComponentDataArray<Health>(),
                Damages = _processDamage.GetBufferArray<Damage>()
            };
            var processDamageJobHandle = processDamageJob.Schedule(_processDamage.CalculateLength(), 64, inputDeps);
            return processDamageJobHandle;
        }
    }
}