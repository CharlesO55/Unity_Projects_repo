using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


using Newtonsoft.Json;
using System.Net.Http;
using UnityEngine.AdaptivePerformance.Provider;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 |                    This script has [TODO]'s.                    |
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class WebAPIManager : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         PROVIDED FIELDS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public static WebAPIManager Instance;

    [SerializeField]
    private string _baseURL = "https://omgvamp-hearthstone-v1.p.rapidapi.com/";

    [SerializeField]
    private string _baseTextureURL = "https://art.hearthstonejson.com/v1/orig/";

    /* [TODO] : Replace this with your own API Key. */
    [SerializeField]
    private string _apiKey = "8031f520camsh4f5b4b0164da981p11e5edjsn09bcb7fd2224";
    //private string _apiKey = "88fbc0a7a7mshde94dc2146c78b5p1ad0cajsn3ee75cbc11fd";

    [SerializeField]
    private string _apiHost = "omgvamp-hearthstone-v1.p.rapidapi.com";

    private Dictionary<int, List<MinionData>> _minionTiers = new Dictionary<int, List<MinionData>>();
    public Dictionary<int, List<MinionData>> MinionTiers {
        get { return this._minionTiers; }
    }


    bool bHasStarted = false;
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                        INCOMPLETE METHODS                       |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    /* [TODO] : Populate [this._minionTiers] with the values from the
     *          provided API. Store each JSON object as a [MinionData]
     *          object equivalent.
     *          
     *          [this._minionTiers] is a [Dictionary] with [int] values
     *          as keys. These values represent the minion's tier. The
     *          tier is equivalent to the "cost" key in the Hearthstone
     *          API.
     *          
     *          A single call to this method will only populate the
     *          [Dictinary] value with minions under the specified [tier].
     *          
     *          This method must somehow use the [tier] parameter in the
     *          formed [url].
     *          
     *          Take note that [this._minionTiers] will only use cards
     *          from the "Battlegrounds" Card Set. Hence, the value of
     *          [cardSet] is already set to "Battlegrounds". */

    private IEnumerator RequestCardSet(string cardSet, int tier) {
        /* [1] : Form the [url] by analysing the linked [RapidAPI Hearthstone] API. */
        string url = this._baseURL;
        url += "cards/sets/Battlegrounds";

        /* Provided code, do NOT remove. */
        using (UnityWebRequest request = new UnityWebRequest(url, "GET")) {
            request.SetRequestHeader("X-RapidAPI-Key", this._apiKey);
            request.SetRequestHeader("X-RapidAPI-Host", this._apiHost);

            /* * * * * [YOUR CODE HERE] * * * * */
            
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            /* [2] : BEFORE adding the retrieved [List] of [MinionData] under the
               specific [this._minionTiers] key, pass it through the [this.Clean()]
               method first, similar to the code snippet shown below :

               yourRetrievedList = this.Clean(yourRetrievedList); */

            if (string.IsNullOrEmpty(request.error))
            {
                Debug.Log("RequestCardSet Success " + url);


                List<MinionData> results = JsonConvert.DeserializeObject<List<MinionData>>(request.downloadHandler.text);
                
                //Clean the results
                results = this.Clean(results);

                
                foreach (var result in results)
                {
                    if(result.Type == "Minion" && result.Cost <= 6 && result.Cost > 0)
                    {
                        Debug.Log($"Cost: {result.Cost} {result.Type}");

                        this._minionTiers[result.Cost].Add(result);
                        Debug.Log("Added minion");
                    }
                }
                Debug.Log("RequestCardsSet Success");
            }
            else
            {
                Debug.LogError("RequestCardSet failed" );
            }
        }

        /* [3] : Remove this line once you have completed the method. */
        //yield return null;
    }

    /* [TODO] : This is the method that downloads an image given by a [url], formed
     *          with the [cardId] parameter. It also sets the downloaded texture
     *          as the sprite of the [targetImage] parameter. */

    public IEnumerator RequestTexture(string cardId, Image targetImage) {
        /* [1] : Form the [url] by analysing the linked [Hearthston.JSON] API. */
        string url = this._baseTextureURL;

        /* [2] : Setup texture downloading, as discussed in class. */
        url += cardId;
        
        
        using (UnityWebRequest request = new UnityWebRequest())
        {
            var request2 = UnityWebRequestTexture.GetTexture(url, targetImage);
            request2.downloadHandler = new DownloadHandlerTexture();
            
            yield return request2.SendWebRequest();

            if (request.result != UnityWebRequest.Result.ConnectionError)
            {
                DownloadHandlerTexture imgExtractor = (DownloadHandlerTexture)request2.downloadHandler;
                Texture2D tex = imgExtractor.texture;

                Sprite sprite = Sprite.Create(tex,
                new Rect(0.0f, 0.0f, tex.width, tex.height),
                new Vector2(0.5f, 0.5f));
                targetImage.sprite = sprite;
            }
        }


        /* [3] : To set the [targetImage]'s sprite to the downloaded texture, use
         * the following code snippet below :
         * 
         * Texture2D texture = <YOUR DOWNLOADED TEXTURE HERE>;
         * Sprite sprite = Sprite.Create(texture,
         *                               new Rect(0.0f, 0.0f, texture.width, texture.height),
         *                               new Vector2(0.5f, 0.5f));
         * targetImage.sprite = sprite; */

        /* [4] : Remove this line once you have completed the method. */
        yield return null;
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         GENERAL METHODS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private IEnumerator InitializeMinionTiers() {
        string cardSet = "Battlegrounds";
        List<Coroutine> coroutines = new List<Coroutine>();

        for(int i = 1; i <= Settings.Instance.MaxTavernLevel; i++) {
            coroutines.Add(this.StartCoroutine(this.RequestCardSet(cardSet, i)));
        }

        Debug.Log("[Message] : LOADING InitializeMinionTiers().");
        
        StartScreen.Instance.HideScreen();

        foreach (Coroutine coroutine in coroutines) {
            yield return coroutine;
        }

        Debug.Log("[Message] : InitializeMinionTiers() DONE.");

    }

    private List<MinionData> Clean(List<MinionData> minions) {
        List<MinionData> result = new List<MinionData>();
        foreach(MinionData minion in minions) {
            if(minion.Type == "Minion" && minion.Img != null) {
                result.Add(minion);
            }
        }

        return result;
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                        LIFECYCLE METHODS                        |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start() {
        //this.StartCoroutine(this.InitializeMinionTiers());

        
    }

    public void StartInitializeOnClick()
    {
        Debug.Log("Start");
        StartScreen.Instance.Unlink();
        this.StartCoroutine(this.InitializeMinionTiers());
    }
}
