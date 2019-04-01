using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    public class Aircraft : Entity
    {
        private Rect m_PlayerMoveBoundary = default(Rect);

        public AircraftData data;

        private Vector3 m_TargetPosition;
        

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            bg sceneBackground = FindObjectOfType<bg>();
            if (sceneBackground == null)
            {
                Debug.LogWarning("Can not find scene background.");
                return;
            }
            data = userData as AircraftData;
            if (data == null)
            {
                Debug.LogError("Aircraft data is invalid.");
                return;
            }

            m_PlayerMoveBoundary = new Rect(sceneBackground.PlayerMoveBoundary.bounds.min.x, sceneBackground.PlayerMoveBoundary.bounds.min.z,
                sceneBackground.PlayerMoveBoundary.bounds.size.x, sceneBackground.PlayerMoveBoundary.bounds.size.z);
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            transform.Translate(h * data.Speed * elapseSeconds, 0, v * data.Speed * elapseSeconds);

            Vector3 speed;
            if (Input.GetMouseButton(0))
            {
                Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                m_TargetPosition = new Vector3(point.x, 0f, point.z);
                Vector3 direction = m_TargetPosition - CachedTransform.localPosition;
                if (direction.sqrMagnitude <= Vector3.kEpsilon)
                {
                    return;
                }
                speed = Vector3.ClampMagnitude(direction.normalized * data.Speed * elapseSeconds, direction.magnitude);
            }
            else
            {
                speed = Vector3.zero;
            }
            
            CachedTransform.localPosition = new Vector3
            (
                Mathf.Clamp(CachedTransform.localPosition.x+speed.x, m_PlayerMoveBoundary.xMin, m_PlayerMoveBoundary.xMax),
                0f,
                Mathf.Clamp(CachedTransform.localPosition.z+speed.z, m_PlayerMoveBoundary.yMin, m_PlayerMoveBoundary.yMax)
            );

            data.AttackInterval += elapseSeconds;
            Attack();
        }

        protected override void OnHide(object userData)
        {
            base.OnHide(userData);
            //游戏结束
            MainProcedure main= (MainProcedure)GameEntry.Procedure.GetProcedure<MainProcedure>();
            main.isSurvival = false;
        }

        private void Attack()//发射子弹
        {
            if ((Input.GetKey(KeyCode.Space)||Input.GetMouseButton(0))&& data.AttackInterval>AircraftData.attackinterval)
            {
                GameEntry.Entity.ShowBullet(new BulletData(GameEntry.Entity.GenerateSerialId(), 50000,1,10f)
                {
                    Position = CachedTransform.localPosition,
                });
                data.AttackInterval = 0;
            }
        }
    }
}



