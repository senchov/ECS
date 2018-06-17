using UnityEngine;

namespace MovementSystem
{
    using Unity.Entities;

    public class MoveComponentSystem : ComponentSystem
    {
        struct Group
        {
            public Transform TargetTransform;
            public MoveDataComponent MovementData;
        }

        protected override void OnUpdate()
        {
            float deltaTime = Time.deltaTime;
            int counter = 0;
            foreach (Group entity in GetEntities<Group>())
            {
                Debug.Log("find ->" + counter++);
            }
            
        }
    }
}