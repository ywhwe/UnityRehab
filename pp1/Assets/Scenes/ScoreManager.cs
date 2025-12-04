using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int _currentTotalScore = 0;
    private Coroutine _scoreUpdateCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreDisplay(_currentTotalScore.ToString());
    }

    public void AddScore(int scoreToAdd)
    {
        if (_scoreUpdateCoroutine != null) StopCoroutine(_scoreUpdateCoroutine);
        _scoreUpdateCoroutine = StartCoroutine(ScoreUpdateSequence(scoreToAdd));
    }

    private IEnumerator ScoreUpdateSequence(int scoreToAdd)
    {
        if (scoreText != null) scoreText.text = $"{_currentTotalScore} <color=#FFD700>+{scoreToAdd}</color>";

        yield return new WaitForSeconds(1.0f);

        _currentTotalScore += scoreToAdd;
        UpdateScoreDisplay(_currentTotalScore.ToString());
        _scoreUpdateCoroutine = null;
    }

    private void UpdateScoreDisplay(string textToDisplay)
    {
        if (scoreText != null) scoreText.text = textToDisplay;
    }

    public string GetTotalScore()
    {
        return _currentTotalScore.ToString();
    }

    public void SetTotalScore(int newScore)
    {
        if (_scoreUpdateCoroutine != null)
        {
            StopCoroutine(_scoreUpdateCoroutine);
            _scoreUpdateCoroutine = null;
        }

        _currentTotalScore = newScore;
        UpdateScoreDisplay(_currentTotalScore.ToString());
    }
}
