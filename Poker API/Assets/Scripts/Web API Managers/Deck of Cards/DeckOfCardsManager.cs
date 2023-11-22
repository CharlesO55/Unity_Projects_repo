using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using Unity.VisualScripting;

/* * * * * * * * * * * * * * * * * * * * * *
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 |     You SHOULD edit this script. :)     |
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 * * * * * * * * * * * * * * * * * * * * * */

public class DeckOfCardsManager : MonoBehaviour {
    /* This class is a Singleton. */
    public static DeckOfCardsManager Instance;

    /* Holds a reference to the API's base URL. */
    [SerializeField]
    private string _baseURL = "https://deckofcardsapi.com/api/deck/";

    /* Holds a reference to the [CardManager] objects in the scene. */
    [SerializeField]
    private List<CardManager> _cardManagers;

    /* Holds a reference to the number of standard decks making up a single deck. */
    [SerializeField]
    private int _count = 1;

    /* Holds a reference to the currently loaded deck. */
    private Deck _deck;

    /* A toggle for reloading the deck through the Editor. */
    [SerializeField]
    private bool _reload = false;

    /* Function to start the [this.RequestDeck()] coroutine. */
    public void CreateDeck() {
        Debug.Log("[CALLED] : CreateDeck()");
        this.StartCoroutine(this.RequestDeck(this._count));
    }

    /* Requests a shuffled deck consisting of N standard decks, where N is the value
     * of [this._count]. After this function, the value of [this._deck] must be set
     * according to the details of the response. */
    private IEnumerator RequestDeck(int count) {
        /* * * * * [TODO] * * * * */
        string url = this._baseURL + "new/shuffle/?deck_count=" + count.ToString() ;

        Debug.Log($"Getting new deck from {url}");
        using (UnityWebRequest request = new UnityWebRequest(url, "GET"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            Debug.Log($"response CODE: {request.responseCode}");

            if (string.IsNullOrEmpty(request.error))
            {
                Deck response = JsonConvert.DeserializeObject<Deck>(request.downloadHandler.text);
                this._deck = response;
            }
            else
                Debug.Log($"Error CODE: {request.error}");
        }
        yield return null;
    }

    /* Function to start the [this.RequestCards()] coroutine. */
    public void DrawCards() {
        Debug.Log("[CALLED] : DrawCards()");
        this.StartCoroutine(this.RequestCards());
    }

    /* Requests 5cards from the currently loaded deck, and creates corresponding
     * Card objects according to the details of the response. Afterwards, it
     * sets the created Card objects to the [_cardReference] fields of the
     * objects in [this._cardManagers]. */
    private IEnumerator RequestCards()
    {
        /* * * * * [TODO] * * * * */
        
        //FLIP THE CARDS
        foreach (var card in this._cardManagers)
        {
            //card.SetTexture("https://deckofcardsapi.com/static/img/back.png");
            //card.SetTexture("https://img.game8.co/3621497/bfa9dcead3872310a70227f229f8ecd1.png/show");

            string imgURL = "https://preview.redd.it/";
            imgURL += (Random.Range(1, 20) == 1) ?
                "ftopysq38qq51.jpg?width=640&crop=smart&auto=webp&s=ce10e56459cf748fb5fc8bc08fdc8ae48ef95d9c" :
                "pfi7ctq38qq51.jpg?width=1125&format=pjpg&auto=webp&s=e318d58fa2696e9832cc28b9fd0f01f16c7211e2";

            card.SetTexture(imgURL);
        }

        //PROCEED
        string m_URL = this._baseURL + this._deck.Deck_ID + "/draw/?count=5";
        using (UnityWebRequest m_Request = new UnityWebRequest(m_URL, "GET"))
        {
            m_Request.downloadHandler = new DownloadHandlerBuffer();
            yield return m_Request.SendWebRequest();

            if (string.IsNullOrEmpty(m_Request.error))
            {
                DrawResponse m_DrawResponse = JsonConvert.DeserializeObject<DrawResponse>(m_Request.downloadHandler.text);

                for (int i = 0; i < 5; i++)
                {
                    this._cardManagers[i].CardReference = m_DrawResponse.Cards[i];
                    this._cardManagers[i].SetTexture(this._cardManagers[i].CardReference.Image);
                }
                CardReader.Instance.CheckResult(m_DrawResponse.Cards);
            }

            else
                Debug.Log($"Error CODE : {m_Request.error}");
        }
    }

    private void Awake() {
    if(Instance == null)
        Instance = this;
    else
        Destroy(this.gameObject);
    }

    private void Start() {
        GUIManager.Instance.Draw.clicked += this.DrawCards;
    }

    private void Update() {
        if(this._reload) {
            this._reload = false;
            this.CreateDeck();
        }
    }
}
