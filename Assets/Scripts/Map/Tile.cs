﻿using Assets.Scripts.CommandHandlers;
using Assets.Scripts.Commands;
using Assets.Scripts.Match;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Map
{
    public class Tile : MonoBehaviour
    {
        public const float MaxCenterToCenterDistance = 1.73f;

        [SerializeField]
        private int id;

        private Shader _originalShader;
        private Shader _selectedShader;
        private Renderer _renderer;
        private MatchManager _matchManager;

        public bool IsUncovered { get; private set; }

        public PlayerToken PlayerToken { get; set; }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public void Awake()
        {
            IsUncovered = false;
            _originalShader = GetComponent<Renderer>().material.shader;
            _selectedShader = Shader.Find("Outlined/Silhouetted Diffuse");
            _renderer = GetComponent<Renderer>();
            _matchManager = FindObjectOfType<MatchManager>();
        }

        public void Uncover()
        {
            if (!IsUncovered)
            {
                transform.Rotate(Vector3.down, 180);

                IsUncovered = true;
            }
        }
        
        private void OnMouseOver()
        {
            if (!_matchManager.Pause)
            {
                _renderer.material.shader = _selectedShader;
            }
        }

        private void OnMouseExit()
        {
            if (!_matchManager.Pause)
            {
                _renderer.material.shader = _originalShader;
            }
        }

        private void OnMouseDown()
        {
            if (PlayerToken != null || _matchManager.Pause)
            {
                return;
            }

            var boardManager = FindObjectOfType<BoardManager>();

            var playerToken = boardManager.FindPlayerToken(_matchManager.CurrentPlayerId);

            if (playerToken.Selected && IsAdjacentTo(playerToken.Tile))
            {
                var commandBus = FindObjectOfType<CommandBus>();

                commandBus.ExecuteInThisTurn(new MovePlayerCommand
                {
                    PlayerId = playerToken.PlayerId,
                    TileId = Id
                });
            }
        }

        private bool IsAdjacentTo(Tile tile)
        {
            const float AdjacencyDistanceMargin = 0.2f;
            return Vector3.Distance(transform.position, tile.transform.position) < MaxCenterToCenterDistance * (1 + AdjacencyDistanceMargin);
        }
    }
}
