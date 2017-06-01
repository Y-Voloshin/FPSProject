using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.MatchStats
{
    public class StatisticsModel
    {
        public bool Victory;
        public int EnemiesKilled;
        public System.TimeSpan MatchTime;

        public void Clear()
        {
            EnemiesKilled = 0;
            MatchTime = new System.TimeSpan();
        }
        //public int 
    }
}