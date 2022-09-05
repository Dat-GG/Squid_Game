using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DataAccount;
using UnityEngine;
using UnityEngine.UI;


namespace UI.WinLose
{
    public class CheckReward : MonoBehaviour
    {
        [SerializeField] private Text rewardTxt;
        [SerializeField] private Text diamondTxt;
        [SerializeField] private Text goldTxt;
        
        protected internal string LabelReward { get; private set; }


        private int valueReward;
        
        
        // Start is called before the first frame update
        void Start()
        {
            //DataAccountPlayer.PlayerMoney.SetMoney()
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void RewardCheck( RewordCheck value)
        {
            switch (value)
            {
                case RewordCheck.X1:
                    rewardTxt.text = "+" + rewardTxt;
                    break;
                case RewordCheck.X2:
                    
                    break;
                case RewordCheck.X3:
                    break;
                
            }
            
            
            
        }

       
           
        
        
    }

    public enum RewordCheck
    {
        X1,X2,X3,
    }
}


