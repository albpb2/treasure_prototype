﻿using System.Collections;
using Assets.Scripts.CommandHandlers;
using Assets.Scripts.Commands;
using Assets.Scripts.Map;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Match
{
    public class TurnActionsPanel : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private GameObject _turnActionsInnerPanel;

        private MatchManager _matchManager;
        private CommandBus _commandBus;
        private TurnManager _turnManager;
        private bool _ready = false;

        public Tile Tile { get; set; }

        public void OnEnable()
        {
            StartCoroutine(SetReadyAfterDelay());
        }

        public void Awake()
        {
            _matchManager = FindObjectOfType<MatchManager>();
            _commandBus = FindObjectOfType<CommandBus>();
            _turnManager = FindObjectOfType<TurnManager>();
        }

        public void OnDisable()
        {
            _ready = false;
        }

        public void Enable(Tile tile)
        {
            gameObject.SetActive(true);
            Tile = tile;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Dig()
        {
            _commandBus.ExecuteInThisTurn(new DigCommand
            {
                PlayerId = _matchManager.CurrentPlayerId,
                TileId = Tile.Id
            });
            _turnManager.HideActionsPanel();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_ready)
            {
                HidePanelIfClickOutside();
            }
        }

        private IEnumerator SetReadyAfterDelay()
        {
            yield return new WaitForSeconds(0.5f);
            _ready = true;
        }

        private void HidePanelIfClickOutside()
        {
            Rect rect = RectTransformToScreenSpace(_turnActionsInnerPanel.GetComponent<RectTransform>());
            if (!rect.Contains(UnityEngine.Input.mousePosition))
            {
                _turnManager.HideActionsPanel();
            }
        }

        private static Rect RectTransformToScreenSpace(RectTransform transform)
        {
            Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
            return new Rect((Vector2)transform.position - (size * 0.5f), size);
        }
    }
}
