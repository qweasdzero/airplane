using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace StarForce
{  
    public class GameUI : UIFormLogic
    {
        private Text Score;
        private int score;
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            Score=CachedTransform.Find("Integral").GetComponent<Text>();
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            Score.text = score.ToString();
        }

        public void AddScore(int score)
        {
            this.score += score;
        }
        
        
    }
}


