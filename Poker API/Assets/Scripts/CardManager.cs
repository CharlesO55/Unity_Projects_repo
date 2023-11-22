using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/* * * * * * * * * * * * * * * * * * * * * *
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 |     You SHOULD edit this script. :)     |
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 * * * * * * * * * * * * * * * * * * * * * */

public class CardManager : MonoBehaviour {
    /* Holds the card's original card back material. */
    [SerializeField]
    private Material _cardBack;

    /* Holds a reference to the currently loaded card. */
    private Card _cardReference;

    /* Public field for [this._cardReference]. */
    public Card CardReference {
        get { return this._cardReference; }
        set { this._cardReference = value; }
    }

    /* Attempts to download the image in the specified [url]
     * and sets it as the main texture of [this.gameObject]. */
    private IEnumerator DownloadTexture(string url)
    {
        /* * * * * [TODO] * * * * */
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error downloading texture.");
        }
        else
        {
            DownloadHandlerTexture response =
                (DownloadHandlerTexture)request.downloadHandler;
            Texture texture = response.texture;
            this.gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
        }
    }

    /* Sets [this.gameObject]'s main texture to the image
     * specified by the [url]. */
    public void SetTexture(string url) {
        this.StartCoroutine(DownloadTexture(url));
    }

    /* Sets [this.gameObject]'s main texture back to its
     * original card back. */
    public void ResetTexture() {
        this.gameObject.GetComponent<Renderer>().material = this._cardBack;
    }
}
