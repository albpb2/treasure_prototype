using Assets.Scripts.Match;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Players
{
    public class PlayerInfoPanel : MonoBehaviour
    {
        private Image _image;
        private Text _text;
        private MatchManager _matchManager;

        public void Awake()
        {
            _image = GameObject.Find("PlayerInfoPanel").GetComponent<Image>();
            _text = GameObject.Find("PlayerIdText").GetComponent<Text>();

            _matchManager = FindObjectOfType<MatchManager>();
        }

        public void SetPlayerInfo(PlayerToken currentPlayerToken)
        {
            _image.color = currentPlayerToken.Color;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.2f);

            _text.text = "Player " + currentPlayerToken.PlayerId;
        }
    }
}
