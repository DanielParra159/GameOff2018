using Unity.Mathematics;
using UnityEngine;

namespace Components.Path
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private float2 _heading;

        public float2 Heading => _heading;
    }
}