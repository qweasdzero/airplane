using GameFramework.DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    public class AsteroidData : EntityData
    {
        public AsteroidData(int entityId, int typeId,int speed) : base(entityId, typeId)
        {
            IDataTable<DRAsteroid> dtAsteroid = GameEntry.DataTable.GetDataTable<DRAsteroid>();
            DRAsteroid drAsteroid = dtAsteroid.GetDataRow(typeId);
            if (drAsteroid == null)
            {
                return;
            }

            Speed = speed;
        }
        public readonly float Speed;
        
    }
}