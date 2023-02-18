using System;
using UnityEngine;

// Dummy classes that are sent to the UI controller
namespace DataClasses
{
    [Serializable]
    public struct PlayerInfo
    {
        public string playerName;
        public string playerClan;
        public int playerXP;
    }

    [Serializable]
    public struct CardInfo
    {
        public string cardName;
        public int cardQwiexCost;
        public Texture2D cardTexture;
    }
}