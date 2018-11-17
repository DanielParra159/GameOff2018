using Components.Player;
using JetBrains.Annotations;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Systems.Player
{
    [UsedImplicitly]
    public class RechargeEnergy : JobComponentSystem
    {
        [BurstCompile]
        private struct RechargeEnergyJob : IJobParallelFor
        {
            public ComponentDataArray<Energy> Energies;
            [ReadOnly] public ComponentDataArray<EnergyRecharger> EnergyRechargers;
            [ReadOnly] public float DeltaTime;

            public void Execute(int i)
            {
                var energy = Energies[i];
                var recharger = EnergyRechargers[i];
                energy.CurrentValue = math.min(energy.CurrentValue + recharger.EnergyPerSecond * DeltaTime, energy.MaxValue);
                Energies[i] = energy;
            }
        }

        ComponentGroup _rechargeEnergyGroup;

        protected override void OnCreateManager()
        {
            _rechargeEnergyGroup = GetComponentGroup(
                typeof(Energy),
                ComponentType.ReadOnly(typeof(EnergyRecharger))
            );
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var rechargeEnergyJob = new RechargeEnergyJob
            {
                Energies = _rechargeEnergyGroup.GetComponentDataArray<Energy>(),
                EnergyRechargers = _rechargeEnergyGroup.GetComponentDataArray<EnergyRecharger>(),
                DeltaTime = Time.deltaTime
            };
            var rechargeEnergyJobHandle = rechargeEnergyJob.Schedule(_rechargeEnergyGroup.CalculateLength(), 64, inputDeps);
            return rechargeEnergyJobHandle;
        }
    }
}