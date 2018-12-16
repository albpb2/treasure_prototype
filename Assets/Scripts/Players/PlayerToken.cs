﻿using Assets.Scripts.Extensions;
using Assets.Scripts.Map;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Players
{
    public class PlayerToken : MonoBehaviour
    {
        private readonly List<Color> _colors = new List<Color>
        {
            Color.red,
            Color.blue,
            Color.black,
            Color.white,
            Color.green,
            Color.yellow
        };

        public static PlayerToken CreatePlayerToken()
        {
            var baseObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

            var playerToken = baseObject.AddComponent<PlayerToken>();
            
            const float TokenScale = 0.5f;
            playerToken.transform.localScale *= TokenScale;

            playerToken.SetRandomColor();

            return playerToken;
        }

        public void MoveTo(Tile tile)
        {
            const float HexagonHeight = 0.367f;

            transform.position = tile.transform.position + new Vector3(0, HexagonHeight, 0);

            if (!tile.IsUncovered)
            {
                tile.Uncover();
            }
        }
        
        private void SetRandomColor()
        {
            var materialColored = new Material(Shader.Find("Diffuse"));
            materialColored.color = _colors.GetRandomElement();
            GetComponent<Renderer>().material = materialColored;
        }
    }
}
