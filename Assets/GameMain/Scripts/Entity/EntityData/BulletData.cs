using System;
using UnityEngine;

namespace StarForce
{
    [Serializable]
    public class BulletData : EntityData
    {
        [SerializeField]
        private int m_OwnerId = 0;

        [SerializeField]
        private float m_Speed = 0f;

        public BulletData(int entityId, int typeId, int ownerId,float speed)
            : base(entityId, typeId)
        {
            m_OwnerId = ownerId;
            m_Speed = speed;
        }

        public int OwnerId
        {
            get
            {
                return m_OwnerId;
            }
        }


        public float Speed
        {
            get
            {
                return m_Speed;
            }
        }
    }
}