using System.Collections.Generic;
using Components.Common;
using Components.Units;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Systems.Units
{
    [UsedImplicitly]
    public class TargetDetection : ComponentSystem
    {
        private struct UnitData : IComponentData
        {
            public Entity Entity;
            public float SquaredRange;
            public float2 Position2D;
            public int Faction;
        }

        private ComponentGroup _unitInfoGroup;
        private Dictionary<int, NativeList<UnitData>> _unitsDataByPaths;

        protected override void OnCreateManager()
        {
            _unitInfoGroup = GetComponentGroup(
                ComponentType.ReadOnly(typeof(Unit)),
                ComponentType.ReadOnly(typeof(Range)),
                ComponentType.ReadOnly(typeof(Position2D)),
                ComponentType.ReadOnly(typeof(Faction)),
                ComponentType.ReadOnly(typeof(AnimationData)),
                ComponentType.Subtractive(typeof(Dying))
            );

            _unitsDataByPaths =
                new Dictionary<int, NativeList<UnitData>>(5); // TODO: move this magic number to constants file
        }

        protected override void OnUpdate()
        {
            GetUnitsDataByPaths();

            ProcessUnits();
        }

        private void GetUnitsDataByPaths()
        {
            var units = _unitInfoGroup.GetComponentDataArray<Unit>();
            var ranges = _unitInfoGroup.GetComponentDataArray<Range>();
            var positions = _unitInfoGroup.GetComponentDataArray<Position2D>();
            var factions = _unitInfoGroup.GetComponentDataArray<Faction>();
            var entities = _unitInfoGroup.GetEntityArray();

            for (var i = 0; i < entities.Length; ++i)
            {
                var unit = units[i];
                if (!_unitsDataByPaths.ContainsKey(unit.Path))
                {
                    _unitsDataByPaths.Add(unit.Path, new NativeList<UnitData>(10, Allocator.Temp));
                }

                var range = ranges[i].Value;
                _unitsDataByPaths[unit.Path].Add(new UnitData
                {
                    SquaredRange = range * range,
                    Position2D = positions[i].Value,
                    Faction = factions[i].Value,
                    Entity = entities[i]
                });
            }
        }

        private void ProcessUnits()
        {
            foreach (KeyValuePair<int, NativeList<UnitData>> unitsDataByPath in _unitsDataByPaths)
            {
                var unitsOnPath = unitsDataByPath.Value;
                for (var i = 0; i < unitsOnPath.Length; ++i)
                {
                    var currentUnit = unitsDataByPath.Value[i];
                    var currentUnitEntity = currentUnit.Entity;
                    var isWalking = true;
                    for (var j = 0; j < unitsOnPath.Length; ++j)
                    {
                        var otherUnit = unitsOnPath[j];
                        if (currentUnit.Faction == otherUnit.Faction)
                            continue;

                        if (math.lengthsq(otherUnit.Position2D - currentUnit.Position2D) < currentUnit.SquaredRange)
                        {
                            isWalking = false;
                            PostUpdateCommands.AddComponent(currentUnitEntity, new Target
                            {
                                Entity = otherUnit.Entity
                            });
                            break;
                        }
                    }
                    
                    EntityManager.SetComponentData(currentUnitEntity, new AnimationData {SetIsWaling = isWalking});
                }

                unitsOnPath.Dispose();
            }

            _unitsDataByPaths.Clear();
        }
    }
}