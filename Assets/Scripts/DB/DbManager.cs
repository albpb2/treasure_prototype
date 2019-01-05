using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using UnityEngine;

namespace Assets.Scripts.DB
{
    public class DbManager : MonoBehaviour
    {
        public static DbManager instance = null;
        private DynamoDBContext _context;

        public DynamoDBContext Context { get; private set; }

        public void Awake()
        {
            //Check if instance already exists
            if (instance == null)

                //if not, set instance to this
                instance = this;

            //If instance already exists and it's not this:
            else if (instance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);

            UnityInitializer.AttachToGameObject(this.gameObject);

            AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

            CreateContext();
        }

        private void CreateContext()
        {
            CognitoAWSCredentials credentials = new CognitoAWSCredentials(
                "eu-west-1:1ad0f793-1bda-40b0-8894-d4c683c29ff6",
                RegionEndpoint.EUWest1
            );
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(credentials, RegionEndpoint.EUWest1);
            Context = new DynamoDBContext(client);
        }
    }
}
