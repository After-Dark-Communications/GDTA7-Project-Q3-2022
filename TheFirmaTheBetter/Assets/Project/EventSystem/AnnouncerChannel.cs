using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EventSystem
{
    public class AnnouncerChannel
    {
        public Action OnGameBoot;
        public Action OnPlayGameStart;
        public Action OnPlayGameStop;
        public Action OnPlayFirstBlood;
        public Action OnPlayPlayerEliminated;
        public Action OnPlayDoubleKill;
        public Action OnPlayEnergyZoneMoved;
        public Action OnShipSelection;
    }
}

