using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    
    public class Hide : MonoBehaviour
    {
        private MainProcedure main;
        private void Start()
        {
            main= (MainProcedure)GameEntry.Procedure.GetProcedure<MainProcedure>();
        }

        private void OnTriggerExit(Collider other)
        {
            GameObject go = other.gameObject;
            Entity entity = go.GetComponent<Entity>();
            if (entity == null)
            {
                Debug.LogWarning("Unknown GameObject, you must use entity only.");
                Destroy(go);
                return;
            }
            main.m_GameUI.AddScore(-1);
            GameEntry.Entity.HideEntity(entity);
        }
    }
}
