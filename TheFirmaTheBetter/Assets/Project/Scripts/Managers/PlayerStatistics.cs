using EventSystem;
using ShipParts.Ship;
using UnityEngine;
namespace Managers
{
    public class PlayerStatistics : MonoBehaviour
    {
        [SerializeField]
        private int playerIndex;
        [SerializeField]
        private bool isAlive;
        [SerializeField]
        private float timeSurvived;
        [SerializeField]
        private int playersKilled;
        [SerializeField]
        private float distanceTravelled;

        private void Awake()
        {
            Setup();
        }

        private void OnEnable()
        {
            Channels.OnPlayerBecomesDeath += OnPlayerKilled;
        }

        private void OnDisable()
        {
            Channels.OnPlayerBecomesDeath -= OnPlayerKilled;
        }

        private void Setup()
        {
            playerIndex = GetComponent<ShipBuilder>().PlayerNumber;
            playersKilled = 0;
            timeSurvived = 0;
            distanceTravelled = 0;
            isAlive = true;
        }

        private void OnPlayerKilled(ShipBuilder shipThatDied, int killerIndex)
        {
            if (killerIndex == playerIndex)
            {
                playersKilled++;
            }
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public float TimeSurvived
        {
            get { return timeSurvived; }
            set { timeSurvived = value; }
        }

        public int PlayersKilled
        {
            get { return playersKilled; }
        }

        public float DistanceTravelled
        {
            get { return distanceTravelled; }
            set { distanceTravelled = value; }
        }
    }
}