using GameFramework;
using GameFramework.Event;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace StarForce
{

    public class LaunchProcedure : ProcedureBase
    {
        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }

        }
        private Dictionary<string,bool> isEnd=new Dictionary<string, bool>();
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);

            Init();
        }
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            foreach (var item in isEnd)
            {
                if (!item.Value)
                    return;
            }

            ChangeState<MenuProcedure>(procedureOwner);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);

            base.OnLeave(procedureOwner, isShutdown);

        }

        private void Init()//初始化
        {
            LoadDataTable("UIForm");
            LoadDataTable("Scene");
            LoadDataTable("Aircraft");
            LoadDataTable("Entity");
            LoadDataTable("Asteroid");
        }

        private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
        {
            LoadDataTableSuccessEventArgs ne = (LoadDataTableSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            isEnd[Utility.Text.Format("DataTable.{0}", ne.DataTableName)] = true;
        }
        private void LoadDataTable(string dataTableName)
        {
            isEnd.Add(Utility.Text.Format("DataTable.{0}", dataTableName), false);
            GameEntry.DataTable.LoadDataTable(dataTableName, LoadType.Text, this);
        }
    }
}
