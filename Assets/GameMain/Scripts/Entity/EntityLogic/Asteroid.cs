using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    public class Asteroid : Entity
    {
        public AsteroidData data;

        public Vector3 random;

        private MainProcedure main;
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            bg sceneBackground = FindObjectOfType<bg>();
            if (sceneBackground == null)
            {
                Debug.LogWarning("Can not find scene background.");
                return;
            }
            data = userData as AsteroidData;
            if (data == null)
            {
                Debug.LogError("Aircraft data is invalid.");
                return;
            }

            random = Random.insideUnitSphere;
            main= (MainProcedure)GameEntry.Procedure.GetProcedure<MainProcedure>();
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            
            CachedTransform.Translate(Vector3.forward * -data.Speed * elapseSeconds, Space.World);
            CachedTransform.Rotate(random*90*elapseSeconds, Space.Self);
        }

        private void OnTriggerEnter(Collider other)
        {
             Entity entity=other.gameObject.GetComponent<Entity>();
             if (entity == null)
             {
                 return;
             }
             else if (entity.GetType()==(typeof(Bullet)))//被子弹打中
             {
                 main.m_GameUI.AddScore(10);
                 GameEntry.Entity.HideEntity(this);
                 GameEntry.Entity.HideEntity(entity);
                 GameEntry.Entity.ShowEffect(new EffectData(GameEntry.Entity.GenerateSerialId(), 70002)
                 {
                     Position = CachedTransform.position,
                 });
             }
             else if(entity.GetType()==(typeof(Aircraft)))//碰到玩家
             {
                 GameEntry.Entity.HideEntity(this);
                 GameEntry.Entity.HideEntity(entity);
                 GameEntry.Entity.ShowEffect(new EffectData(GameEntry.Entity.GenerateSerialId(), 70000) 
                 {
                     Position = CachedTransform.position,
                 });
             }                             
        }
    }
}

