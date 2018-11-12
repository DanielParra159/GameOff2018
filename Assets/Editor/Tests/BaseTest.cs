using NUnit.Framework;
using Unity.Entities;

namespace Editor.Tests
{
    public class BaseTest
    {
        protected World World;
        protected EntityManager EntityManager;
        protected EntityManager.EntityManagerDebug ManagerDebug;

        [SetUp]
        public virtual void Setup()
        {
            World = new World("Test World");

            EntityManager = World.GetOrCreateManager<EntityManager>();
            ManagerDebug = new EntityManager.EntityManagerDebug(EntityManager);
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (EntityManager != null)
            {
                World.Dispose();
                World = null;

                EntityManager = null;
            }
        }
    }
}