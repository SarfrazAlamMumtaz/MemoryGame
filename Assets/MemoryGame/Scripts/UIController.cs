using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


namespace MemoryGame
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameSetting gameSetting;
        [SerializeField] private TextMeshProUGUI layoutText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI turnText;

        private StringBuilder scoreSB = new StringBuilder();
        private StringBuilder turnSB = new StringBuilder();

        void Start()
        {
            LayoutDisplay();
        }
        private void OnEnable()
        {
            ScoreSystem.OnScoreChanged += UpdateScoreUI;
            ScoreSystem.OnTurnChanged += UpdateTurnUI;
        }

        private void OnDisable()
        {
            ScoreSystem.OnScoreChanged -= UpdateScoreUI;
            ScoreSystem.OnTurnChanged -= UpdateTurnUI;
        }
        public void LayoutDisplay()
        {
            StringBuilder layout = new StringBuilder();
            layout.Append("Layout : ");
            layout.Append(gameSetting.rows);
            layout.Append(" x ");
            layout.Append(gameSetting.columns);
 
            layoutText.SetText(layout);
        }
        public void UpdateScoreUI(int score)
        {
            scoreSB.Clear();
            scoreSB.Append("Matched : ");
            scoreSB.Append(score);

            scoreText.SetText(scoreSB);
        }
        private void UpdateTurnUI(int turn)
        {
            turnSB.Clear();
            turnSB.Append("Turn : ");
            turnSB.Append(turn);

            turnText.SetText(turnSB);
        }
    }
}

