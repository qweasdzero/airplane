using System;
using GameFramework.DataTable;
using GameFramework.Event;
using UnityEngine.SocialPlatforms;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using  GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using UnityEngine.UI;

namespace StarForce
{
    public class MainProcedure : ProcedureBase
    {
        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }

        private float m_ElapseSeconds;

        public bool isSurvival;

        private float endtime = 0;

        public GameUI m_GameUI;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            isSurvival = true;
            endtime = 0;
            GameEntry.Event.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUiFormSuccess);
            
            GameEntry.UI.OpenUIForm(UIFormId.GameUI, this);

            IDataTable<DRScene> dtScene = GameEntry.DataTable.GetDataTable<DRScene>();
            DRScene drScene = dtScene.GetDataRow(2);
            GameEntry.Scene.LoadScene(AssetUtility.GetSceneAsset(drScene.AssetName), Constant.AssetPriority.SceneAsset, this);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            m_ElapseSeconds += elapseSeconds;
            if (m_ElapseSeconds >= 1f)
            {
                m_ElapseSeconds = 0f;
                IDataTable<DRAsteroid> dtAsteroid = GameEntry.DataTable.GetDataTable<DRAsteroid>();
                float randomPositionX = Utility.Random.GetRandom(-7,7);
                float randomPositionZ = Utility.Random.GetRandom(18,22);;
                GameEntry.Entity.ShowAsteroid(new AsteroidData(GameEntry.Entity.GenerateSerialId(), 60000 + Utility.Random.GetRandom(dtAsteroid.Count),5)
                {
                    Position = new Vector3(randomPositionX, 0f, randomPositionZ),
                });
            }

            if (!isSurvival)
            {
                endtime += elapseSeconds;
                if (endtime > 2f)
                {
                    ChangeState<MenuProcedure>(procedureOwner);
                }
            }
        }

        private void OnLoadSceneSuccess(object sender, GameEventArgs e)
        {
            LoadSceneSuccessEventArgs ne = (LoadSceneSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            GameEntry.Entity.ShowAircraft(new AircraftData(GameEntry.Entity.GenerateSerialId(), 10000,8));
        }

        private void OnOpenUiFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            m_GameUI = (GameUI)ne.UIForm.Logic;
        }
        

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUiFormSuccess);
            
            GameEntry.Entity.HideAllLoadingEntities();
            GameEntry.Entity.HideAllLoadedEntities();
            
            string[] loadedSceneAssetNames = GameEntry.Scene.GetLoadedSceneAssetNames();
            for (int i = 0; i < loadedSceneAssetNames.Length; i++)
            {
                GameEntry.Scene.UnloadScene(loadedSceneAssetNames[i]);
            }
            
            base.OnLeave(procedureOwner, isShutdown);
        }
    }
}