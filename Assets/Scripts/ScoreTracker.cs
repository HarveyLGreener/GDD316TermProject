using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreBoard;
    [SerializeField] private TextMeshProUGUI winText;
    private int player1score = 0;
    private int player2score = 0;
    public int pointVal = 2;
    public int firstTo = 8;
    // Start is called before the first frame update

    public static ScoreTracker Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        scoreBoard.text = "" + player1score + "     " + player2score;
        DontDestroyOnLoad(this.gameObject);

    }

    public void ScorePointsP1()
    {
        player1score += pointVal;
    }

    public void ScorePointsP2()
    {
        player2score += pointVal;
    }
    public void UpdateScoreBoard(string playerName)
    {
        scoreBoard.text = "" + player1score + "     " + player2score;
        if (player1score >= firstTo || player2score >= firstTo)
        {
            StartCoroutine(ResetGame(playerName));
        }
        else
        {
            StartCoroutine(WaitForNextRound(playerName));
        }
    }

    public IEnumerator WaitForNextRound(string playerName)
    {
        winText.text = playerName + " Scores!";
        yield return new WaitForSeconds(2f);
        winText.text = "";
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator ResetGame(string playerName)
    {
        winText.text = playerName + " Wins!";
        yield return new WaitForSeconds(2f);
        winText.text = "";
        player1score = 0;
        player2score = 0;
        scoreBoard.text = "" + player1score + "     " + player2score;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
