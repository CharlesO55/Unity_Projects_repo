using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |   You may NOT edit this script. Why would you even want to...?  |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class BackgroundManager : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         PROVIDED FIELDS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    [SerializeField]
    private RawImage _stillImage;

    [SerializeField]
    private List<Texture> _stillImages;

    [SerializeField]
    private VideoPlayer _boardOpen;
    public VideoPlayer BoardOpen {
        get { return this._boardOpen; }
    }

    [SerializeField]
    private VideoPlayer _boardLoop;
    public VideoPlayer BoardLoop {
        get { return this._boardLoop; }
    }

    [SerializeField]
    private VideoPlayer _boardClose;
    public VideoPlayer BoardClose {
        get { return this._boardClose; }
    }

    private RawImage _rawImage;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         GENERAL METHODS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public void OpenBattleground() {
        this._rawImage.color = new Color(255, 255, 255, 255);
        this._boardOpen.loopPointReached += this.StartBoardLoop;

        this._stillImage.texture = this._stillImages[(int)EStillImage.CLOSE];
        this._rawImage.texture = this._boardOpen.targetTexture;

        VideoPlayerManager.Instance.Stop(this._boardClose);
        VideoPlayerManager.Instance.Stop(this._boardOpen);
        this._boardOpen.Play();
    }

    public void OpenTavern() {
        this._rawImage.color = new Color(255, 255, 255, 255);
        this._stillImage.texture = this._stillImages[(int)EStillImage.OPEN];
        this._rawImage.texture = this._boardClose.targetTexture;

        VideoPlayerManager.Instance.Stop(this._boardLoop);
        VideoPlayerManager.Instance.Stop(this._boardClose);
        this._boardClose.Play();
    }

    private void StartBoardLoop(VideoPlayer videoPlayer) {
        this._boardOpen.loopPointReached -= this.StartBoardLoop;
        this._boardOpen.Stop();

        this._stillImage.texture = this._stillImages[(int)EStillImage.OPEN];
        this._rawImage.texture = this._boardLoop.targetTexture;

        VideoPlayerManager.Instance.Stop(this._boardOpen);
        VideoPlayerManager.Instance.Stop(this._boardLoop);
        this._boardLoop.Play();
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                        LIFECYCLE METHODS                        |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Start() {
        this._rawImage = this.GetComponent<RawImage>();
        this._rawImage.color = new Color(0, 0, 0, 0);
    }
}
