using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace StarForce{
    public class MenuForm : UIFormLogic
    {
        private Button Star;
        private Button Exit;

        private MenuProcedure m_ProcedureMenu = null;
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_ProcedureMenu = (MenuProcedure)userData;

            Star = CachedTransform.Find("Menu/Star").GetComponent<Button>();
            Star.onClick.AddListener(StarOnClick);
            Exit = CachedTransform.Find("Menu/Exit").GetComponent<Button>();
            Exit.onClick.AddListener(ExitOnClick);
            
        }
        protected override void OnClose(object userData)
        {
            base.OnClose(userData);
            Star.onClick.RemoveAllListeners();
            Exit.onClick.RemoveAllListeners();
        }

        private void ExitOnClick()
        {
            UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit);
        }

        private void StarOnClick()
        {
            m_ProcedureMenu.StartGame();

        }
    }
}


