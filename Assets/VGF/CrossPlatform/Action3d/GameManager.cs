using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d
{
    public class GameManager
    {
        static int EnemiesCount;
        static int EnemiesCountCurrent;
        static float MatchStartSeconds;
        static PlayerController player;

        public static void Init()
        {
            player = GameObject.FindObjectOfType<PlayerController>();
            player.OnDamaged += OnPlayerDamagedHandler;
            AbstractAliveController.OnDead += OnAliveDeadHandler;
            EnemiesCount = GameObject.FindObjectsOfType<NPC.Enemy.EnemyController>().Length;
            InitMatch();
        }

        public static void RestartMatch()
        {
            SaveLoadBehaviour.LoadInitAll();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            InitMatch();
        }

        static void InitMatch()
        {
            EnemiesCountCurrent = EnemiesCount;
            MatchStartSeconds = Time.realtimeSinceStartup;
            UI.UIController.UpdateEnemiesLeft(EnemiesCountCurrent);
            UI.UIController.UpdateHealth(player.CurrentHealth);
        }        

        static void OnPlayerDamagedHandler(int hpLeft)
        {
            UI.UIController.UpdateHealth(hpLeft);
        }

        static void OnAliveDeadHandler(AbstractAliveController alive)
        {
            if (alive == player)
                OnPlayerDead();
            else
                OnEnemyDead();
        }

        static void OnPlayerDead()
        {
            GameOver(false);
        }

        static void OnEnemyDead()
        {
            EnemiesCountCurrent--;
            UI.UIController.UpdateEnemiesLeft(EnemiesCountCurrent);
            if (EnemiesCountCurrent == 0)
                GameOver(true);
                
        }

        static void GameOver (bool victory)
        {
            MatchStats.StatisticsController.SetMatchEnd(victory, Time.realtimeSinceStartup - MatchStartSeconds, EnemiesCount - EnemiesCountCurrent);
            UI.UIController.OnGameOver();
        }
    }
}