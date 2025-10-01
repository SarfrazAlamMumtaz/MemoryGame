using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace MemoryGame
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameSetting gameSetting;
        [SerializeField] private TextMeshProUGUI layoutText;

        void Start()
        {
            LayoutDisplay();
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
    }
}

