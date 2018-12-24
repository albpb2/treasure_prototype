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
            var numberOfPlayers = GetNumberOfPlayers();

            if (IsNumberOfPlayersCorrect(numberOfPlayers))
            {
                for (var i = 0; i < numberOfPlayers; i++)
                {
                    CreatePlayer(i);
                }

                DisableNumberOfPlayersPanel();

                Pause = false;
            }
        }

        private int GetNumberOfPlayers()
        {
            var numberOfPlayersText = GameObject.Find("NumberOfPlayersText").GetComponent<Text>();
            int numberOfPlayers = 0;

            if (numberOfPlayersText != null)
            {
                int.TryParse(numberOfPlayersText.text, out numberOfPlayers);
            }

            return numberOfPlayers;
        }

        private bool IsNumberOfPlayersCorrect(int numberOfPlayers)
        {
            return numberOfPlayers >= MinNumberOfPlayers && numberOfPlayers <= MaxNumberOfPlayers;
        }

        private void CreatePlayer(int playerId)
        {
            _playersIds.Add(playerId);
            _boardManager.CreatePlayerToken(playerId);
        }

        private void DisableNumberOfPlayersPanel()
        {
            var numberOfPlayersPanel = GameObject.Find("NumberOfPlayersPanel");

            numberOfPlayersPanel.SetActive(false);
        }
    }
}
