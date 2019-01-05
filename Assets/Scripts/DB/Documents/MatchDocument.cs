using Amazon.DynamoDBv2.DataModel;

namespace Assets.Scripts.DB.Documents
{
    [DynamoDBTable("TreasurePrototypeMatches")]
    public class MatchDocument
    {
        [DynamoDBHashKey]
        public string MatchId { get; set; }
        [DynamoDBProperty]
        public string StateJson { get; set; }
    }
}
