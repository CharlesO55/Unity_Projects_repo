using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private TextMeshProUGUI scoreUI;
    private int nScore = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoreUI = this.GetComponent<TextMeshProUGUI>();
        scoreUI.text = "Score : "+ nScore.ToString();
    }

    public void IncrementScore()
    {
        this.nScore++;
        scoreUI.text = "Score : " + nScore.ToString(); 
    }
}
