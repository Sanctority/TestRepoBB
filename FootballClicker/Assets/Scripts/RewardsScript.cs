using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RewardsScript : MonoBehaviour {

    private void Start()
    {
        if(PlayerPrefs.GetInt("FirstTimeReward") != 1)
        {
            PlayerPrefs.SetInt("DailyTimeHolderDay", System.DateTime.Now.Day);
            PlayerPrefs.SetInt("DailyTimeHolderMonth", System.DateTime.Now.Month);

            PlayerPrefs.SetInt("FirstTimeReward", 1);
        }

        DailyReward();
    }

    private void DailyReward()
    {
        if (PlayerPrefs.GetInt("DailyTimeHolderDay") < System.DateTime.Now.Day && PlayerPrefs.GetInt("DailyTimeHolderMonth") <= System.DateTime.Now.Month)
        {
            //code for the reward goes here
        }
    }


}
