using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MapSelection
{
    public abstract class MapData : ScriptableObject
    {
        [SerializeField]
        private string mapName;
        [SerializeField]
        private Image mapPicture;
        [SerializeField]
        private string gameMode;
        [SerializeField]
        private int mapSceneIndex;


        public string MapName { get { return mapName; } }
        public Image MapPicture { get { return mapPicture; } }
        public string GameMode { get { return gameMode; } }
        public int MapSceneIndex { get { return mapSceneIndex; } }
    }
}