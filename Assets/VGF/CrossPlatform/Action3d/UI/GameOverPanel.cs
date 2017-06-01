using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.UintyUI;

namespace VGF.Action3d.UI
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField]
        string VictoryText, DefeatText;
        [SerializeField]
        LabelValueGroup Caption, Time, Enemies;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Show()
        {
            var m = MatchStats.StatisticsController.Model;
            Caption.SetValueSafe(m.Victory ? VictoryText : DefeatText);
            Time.SetValueSafe(string.Format("{0}:{1}:{2}", m.MatchTime.Hours, m.MatchTime.Minutes, m.MatchTime.Seconds));
            Enemies.SetValueSafe(m.EnemiesKilled);

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}