using Assets.Scripts.Map;
using Assets.Scripts.Players;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Match
{
    public class MatchManager : MonoBehaviour
    {
        private const int MinNumberOfPlayers = 1;
        private const int MaxNumberOfPlayers = 4;

        private List<long> _playersIds;
        private BoardManager _boardManager;

        public int CurrentPlayer { get; set; }

        public bool Pause { get; set; }

        public void Awake()
        {
            _boardManager = FindObjectOfType<BoardManager>();

            _playersIds = new List<long>();

            Pause = true;
        }

        public void CreatePlayers()
        {
            var numberOfPlayersText = GameObject.Find("NumberOfPlayersText").GetComponent<Text>();
            int numberOfPlayers = 0;

            if (numberOfPlayersText != null && int.TryParse(numberOfPlayersText.text, out numberOfPlayers) 
                && numberOfPlayers >= MinNumberOfPlayers && numberOfPlayers <= MaxNumberOfPlayers)
            {
                for (var i = 0; i < numberOfPlayers; i++)
                {
                    _playersIds.Add(i);
                    _boardManager.CreatePlayerToken(i);
                }

                var numberOfPlayersPanel = GameObject.Find("NumberOfPlayersPanel");

                numberOfPlayersPanel.SetActive(false);

                Pause = false;
            }
        }
    }
}
