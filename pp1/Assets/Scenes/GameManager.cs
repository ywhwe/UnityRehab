using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Dice> allDice = new List<Dice>();

    public CheckZone checkZone;
    public ScoreManager scoreManager;
    public DiceManager diceManager;

    public GameObject gameResult;
    public TextMeshProUGUI totalScore;

    [HideInInspector] public static GameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        if (checkZone == null)
        {
            enabled = false;
            return;
        }
        if (allDice.Count == 0) Debug.Log("[GameManager] allDice 리스트가 비어있음");

        if (gameResult != null) gameResult.SetActive(false);
    }

    void Start()
    {
        InitialGameSetup();
        Time.timeScale = 1.5f;
    }

    public void InitialGameSetup()
    {
        Debug.Log("[GameManager] 게임 초기화 시작");
        if (gameResult.activeSelf) gameResult.SetActive(false);

        if (checkZone != null) checkZone.ResetAllDice();
        if (scoreManager != null) scoreManager.SetTotalScore(0);
        if (diceManager != null) diceManager.indeterminateReroll = 0;

        foreach (Dice dice in allDice)
        {
            if (dice != null) dice.ResetPosition();
        }
    }

    public void EndGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void RollAllDice()
    {
        if (checkZone == null || allDice.Count == 0)
        {
            Debug.Log("[GameManager] RollAllDice() 호출 실패");
            return;
        }

        checkZone.ResetAllDice();
        Debug.Log("[GameManager] 이전 상태 초기화 완료");

        foreach (Dice dice in allDice)
        {
            if (dice != null) dice.DiceRoll();
        }

        checkZone.EnableDetection();
    }

    public void SetScore(int score)
    {
        scoreManager.AddScore(score);
    }

    public void DisplayScore()
    {
        totalScore.text = $"<color=#FFD700>{scoreManager.GetTotalScore()}</color>";
    }
}
