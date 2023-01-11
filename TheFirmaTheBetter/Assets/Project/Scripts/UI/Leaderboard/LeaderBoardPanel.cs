using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LeaderBoardPanel : MonoBehaviour
    {
        private int playerIndex;
        private int playerPoints;

        [Header("Ui Elements")]
        [SerializeField]
        private TMP_Text playerIndexUI;
        [SerializeField]
        private TMP_Text playerPointsUI;

        private void OnEnable()
        {
            playerIndexUI.text = playerIndex.ToString();
            playerPointsUI.text = playerPoints.ToString();
        }

        public void UpdateStats(int index, int points)
        {
            SetPlayerIndex(index);
            SetPoints(points);
        }

        public void SetPlayerIndex(int index)
        {
            playerIndex = index;
            playerIndexUI.text = playerIndex.ToString();
        }

        public void SetPoints(int points)
        {
            playerPoints = points;
            playerPointsUI.text = playerPoints.ToString();
        }

        public void UpdatePoints(int pointsToAdd)
        {
            playerPoints += pointsToAdd;
            playerPointsUI.text = playerPoints.ToString();
        }

        public int PlayerIndex { get { return playerIndex; } }
        public int PlayerPoints { get { return playerPoints; } }
    }
}
