using Assets.Scripts.Match;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Players
{
    public class PlayerInfoPanel : MonoBehaviour
    {
        private Image _image;
        private Text _playerIdText;
        private Text _playerGoldText;

        public void Awake()
        {
            _image = GameObject.Find("PlayerInfoPanel").GetComponent<Image>();
            _playerIdText = GameObject.Find("PlayerIdText").GetComponent<Text>();
            _playerGoldText = GameObject.Find("PlayerGoldText").GetComponent<Text>();
        }

        public void SetPlayerInfo(Player currentPlayer, PlayerToken currentPlayerToken)
        {
            _image.color = currentPlayerToken.Color;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.2f);

            _playerIdText.text = "Player " + currentPlayerToken.PlayerId;
            _playerGoldText.text = "Gold = " + currentPlayer.Gold;
        }
    }
}
