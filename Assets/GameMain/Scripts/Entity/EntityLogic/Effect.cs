using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{    
    public class Effect : Entity
    {
        public EffectData data;
        
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            data = userData as EffectData;
            if (data == null)
            {
                Debug.LogError("Effect data is invalid.");
                return;
            }
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            data.keeptime += elapseSeconds;
            if (data.keeptime > EffectData.KeepTime)
            {
                GameEntry.Entity.HideEntity(this);
            }    
        }
    }
}