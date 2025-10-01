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

        private int score;
        private int turn;

        private StringBuilder scoreSB = new StringBuilder();
        private StringBuilder turnSB = new StringBuilder();

        void Start()
        {
            score = 0;
            turn = 0;

            LayoutDisplay();
        }
        private void OnEnable()
        {
            GameController.ActionWin += UpdateScoreByOne;
            GameController.ActionTurn += UpdateTurn;
        }

        private void OnDisable()
        {
            GameController.ActionWin -= UpdateScoreByOne;
            GameController.ActionTurn -= UpdateTurn;
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
        public void UpdateScoreByOne()
        {
            score++;

            scoreSB.Clear();
            scoreSB.Append("Matched : ");
            scoreSB.Append(score);

            scoreText.SetText(scoreSB);
        }
        private void UpdateTurn()
        {
            turn++;

            turnSB.Clear();
            turnSB.Append("Turn : ");
            turnSB.Append(turn);

            turnText.SetText(turnSB);
        }
    }
}

