using Assets.Scripts.DB;
using Assets.Scripts.DB.Documents;
using Assets.Scripts.Map;
using System;
using UnityEngine;

namespace Assets.Scripts.Match
{
    public class TurnManager : MonoBehaviour
    {
        private BoardManager _boardManager;
        private MatchManager _matchManager;

        public bool HasTurnBeenPlayed { get; private set; }

        public delegate void TurnReset();
        public static event TurnReset onTurnReset;

        public void Awake()
        {
            _boardManager = FindObjectOfType<BoardManager>();
            _matchManager = FindObjectOfType<MatchManager>();
        }

        public void EndTurn()
        {
            HasTurnBeenPlayed = false;

            _matchManager.SwitchCurrentPlayer();

            onTurnReset();

            DbManager.instance.Context.SaveAsync(new MatchDocument
            {
                MatchId = Guid.NewGuid().ToString(),
                StateJson = Guid.NewGuid().ToString(),
            }, (result)=>{
                if (result.Exception == null)
                    Debug.Log("Status saved.");
                else
                    Debug.Log(result.Exception);
            });
        }

        public void PlayTurn()
        {
            HasTurnBeenPlayed = true;
        }
    }
}
