using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    public class bg : MonoBehaviour
    {
        public BoxCollider PlayerMoveBoundary = null;
        [SerializeField]
        private float m_ScrollSpeed = -0.25f;

        [SerializeField]
        private float m_TileSize = 30f;

        private Transform m_CachedTransform;
        private Vector3 m_StartPosition;
        void Start()
        {
            m_CachedTransform = transform;
            m_StartPosition = m_CachedTransform.position;
        }
        void Update()
        {
            float newPosition = Mathf.Repeat(Time.time * m_ScrollSpeed, m_TileSize);
            m_CachedTransform.position = m_StartPosition + Vector3.forward * newPosition;
        }
    }
}


