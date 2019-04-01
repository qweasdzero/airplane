using GameFramework.DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    public class AircraftData : EntityData
    {
        public AircraftData(int entityId, int typeId,int speed) : base(entityId, typeId)
        {
            IDataTable<DRAircraft> dtAircraft = GameEntry.DataTable.GetDataTable<DRAircraft>();
            DRAircraft drAircraft = dtAircraft.GetDataRow(TypeId);
            if (drAircraft == null)
            {
                return;
            }

            Speed = speed;
        }
        public readonly float Speed;
        
        public const float attackinterval=0.3f;//攻击间隔
        
        public float AttackInterval=0;
    }
}