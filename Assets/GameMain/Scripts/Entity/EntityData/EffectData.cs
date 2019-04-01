using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    public class EffectData : EntityData
    {
        public const float KeepTime=3;
        
        public float keeptime; 
        public EffectData(int entityId, int typeId) : base(entityId, typeId)
        {
            
        }
    }

}

