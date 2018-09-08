using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityManagerProviderSO", menuName = "ScriptableObject/EntityManagerProviderSO")]
public class EntityManagerProviderSO : ScriptableObject
{
    private EntityManager GameEntityManager;

    public EntityManager GetEntityManager
    {
        get
        {
            if (GameEntityManager == null)
            {
                GameEntityManager = World.Active.GetOrCreateManager<EntityManager>();
            }
            return GameEntityManager;
        }
    }
}
