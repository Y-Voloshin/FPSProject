using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.UintyUI;
using VGF;

namespace VGF.Action3d.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        LabelValueGroup Healt,
            Enemies;
        [SerializeField]
        GameOverPanel GameOverPanel;

        static UIController instance;


        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public static void UpdateHealth(int value)
        {
            instance.Healt.SetValueSafe(value);
        }

        public static void UpdateEnemiesLeft(int value)
        {
            instance.Enemies.SetValueSafe(value);
        }

        public void Restart()
        {
            MyCursor.SetHide(true);
            GameManager.RestartMatch();
            GameOverPanel.Hide();
        }

        void GameOver()
        {
            MyCursor.SetHide(false);
            if (GameOverPanel)
                GameOverPanel.Show();
        }

        public static void OnGameOver()
        {
            instance.GameOver();
        }
    }
}