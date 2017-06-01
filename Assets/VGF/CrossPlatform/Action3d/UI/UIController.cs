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
        [SerializeField]
        GameObject ExitMenu;

        static UIController instance;
        public static event System.Action OnRestart;

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
            CheckExitGameMenu();
        }

        void CheckExitGameMenu()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameOverPanel.gameObject.activeSelf)
                    return;
                if (!ExitMenu)
                    return;
                bool exitActive = ExitMenu.activeSelf;
                MyCursor.SetHide(exitActive);
                ExitMenu.SetActive(!exitActive);
            }
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
            GameOverPanel.Hide();
            OnRestart.CallEventIfNotNull();
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

        public void OnGameExit()
        {
            Application.Quit();
        }

        public void OnBackToGame()
        {
            MyCursor.SetHide(true);
            ExitMenu.SetActive(false);
        }
        
    }
}