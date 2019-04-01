using UnityEngine;

namespace StarForce
{
    public class Bullet : Entity
    {
        public BulletData data;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            data = userData as BulletData;
            if (data == null)
            {
                Debug.LogError("Bullet data is invalid.");
                return;
            }
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            CachedTransform.Translate(Vector3.forward * data.Speed * elapseSeconds, Space.World);
        }
        
    }
}



