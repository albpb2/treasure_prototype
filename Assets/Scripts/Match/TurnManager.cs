using UnityEngine;

namespace Assets.Scripts.Match
{
    public class TurnManager : MonoBehaviour
    {
        public bool HasTurnBeenPlayed { get; private set; }

        public void ResetTurn()
        {
            HasTurnBeenPlayed = false;
        }

        public void PlayTurn()
        {
            HasTurnBeenPlayed = true;
        }
    }
}
