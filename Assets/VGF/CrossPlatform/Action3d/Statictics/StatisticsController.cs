using System.Collections;
using System.Collections.Generic;
using System;

namespace VGF.Action3d.MatchStats
{
    public class StatisticsController
    {
        static StatisticsModel model = new StatisticsModel();
        public static StatisticsModel Model { get { return model; } }

        // Use this for initialization
        public static void Init()
        {
            Model.Clear();
        }

        public static void SetMatchEnd (bool victory, float seconds, int enemiesKilled)
        {
            model.Victory = victory;
            model.MatchTime = TimeSpan.FromSeconds(seconds);
            model.EnemiesKilled = enemiesKilled;
        }
    }
}