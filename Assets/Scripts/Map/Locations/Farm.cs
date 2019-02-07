using Assets.Scripts.Match;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Map.Locations
{
    public class Farm : MonoBehaviour
    {
        private MatchManager _matchManager;
        private EconomyManager _economyManager;

        public Tile Tile { get; set; }

        public long PlayerId { get; set; }

        public void Start()
        {
            _matchManager = FindObjectOfType<MatchManager>();
            _economyManager = FindObjectOfType<EconomyManager>();

            TurnManager.onTurnReset += GenerateMoney;
        }

        public void OnDestroy()
        {
            TurnManager.onTurnReset -= GenerateMoney;
        }

        public static Farm Instantiate(Tile parentTile, PlayerToken ownerPlayerToken)
        {
            var prefabsManager = FindObjectOfType<PrefabsManager>();

            var farm = Instantiate(prefabsManager.FarmPrefab).GetComponent<Farm>();

            farm.SetParent(parentTile);

            farm.SetColor(ownerPlayerToken.Color);

            farm.Start();

            return farm;
        }

        public void SetParent(Tile tile)
        {
            Tile = tile;

            transform.parent = Tile.transform;
            var locationsSettings = FindObjectOfType<LocationsSettings>();
            transform.localPosition = locationsSettings.FarmLocalPosition;
        }

        public void SetColor(Color color)
        {
            var materialColored = new Material(Shader.Find("Diffuse"))
            {
                color = color
            };
            GetComponent<Renderer>().material = materialColored;
        }

        public void GenerateMoney()
        {
            _economyManager.CreditGoldToPlayer(PlayerId, _matchManager.FarmMoneyPerTurn);
        }
    }
}
