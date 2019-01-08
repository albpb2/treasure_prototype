using Amazon.DynamoDBv2;
using Assets.Scripts.DB;
using Assets.Scripts.DB.Documents;
using Assets.Scripts.Match.Status.Entities.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Match.Status
{
    public class MatchStatusLoader
    {
        private MatchManager _matchManager;

        public MatchStatusLoader(MatchManager matchManager)
        {
            _matchManager = matchManager;
        }

        private void LoadMatchStatus()
        {
            DbManager.instance.Context.LoadAsync<MatchDocument>(_matchManager.MatchId, 
                (AmazonDynamoDBResult<MatchDocument> result) =>
                {
                    if (result.Exception != null)
                    {
                        Debug.Log("Error retrieving status");
                        return;
                    }

                    _matchManager.SetStatus(JsonUtility.FromJson<MatchStatus>(result.Result.StateJson));
                });
        }
    }
}
