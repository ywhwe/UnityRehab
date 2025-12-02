using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    private Dictionary<string, int> _diceFaces = new Dictionary<string, int>();

    public TextMeshProUGUI patternResult;
    public int totalDiceCount = 3;
    private Dictionary<string, bool> _diceStatus = new Dictionary<string, bool>();

    public int indeterminateReroll = 0;

    void Start()
    {
        _diceFaces["d61"] = 0;
        _diceFaces["d62"] = 0;
        _diceFaces["d63"] = 0;

        _diceStatus["d61"] = false;
        _diceStatus["d62"] = false;
        _diceStatus["d63"] = false;

        if (patternResult != null) patternResult.text = "Roll Dice!";
    }

    public void SetDiceFace(string diceName, int faceValue)
    {
        if (_diceFaces.ContainsKey(diceName))
        {
            if (_diceFaces[diceName] != faceValue)
            {
                _diceFaces[diceName] = faceValue;
                _diceStatus[diceName] = true;

                Debug.Log($"[DiceManage] 주사위 {diceName}의 값 : {faceValue}");

                CheckAllDiceSettledAndAnalyze();
            }
        }
        else Debug.LogWarning($"[DiceManager] 등록되지 않은 주사위 이름: {diceName}");
    }

    private void CheckAllDiceSettledAndAnalyze()
    {
        int settledCount = 0;

        foreach (var status in _diceStatus.Values) if (status) settledCount++;

        if (patternResult != null) {
            if (settledCount == totalDiceCount)
            {
                Debug.Log("[DiceManager] 모든 주사위 안정, 패턴 분석중...");
                patternResult.text = AnalyzeDicePattern();
            }
            else
            {
                patternResult.text = $"Dice Rolling... ({settledCount}/{totalDiceCount})";
            }
        }
    }

    private string AnalyzeDicePattern()
    {
        List<int> faceValues = new List<int>(_diceFaces.Values);

        if (faceValues.Count != totalDiceCount)
        {
            Debug.LogError("[DiceManager] 주사위 개수 불일치!");
            return "Dice Count mismatch!";
        }

        faceValues.Sort();

        int d1 = faceValues[0];
        int d2 = faceValues[1];
        int d3 = faceValues[2];

        if (d1 == d2 && d2 == d3)
        {
            if (d1 == 1) { GameManager.instance.SetScore(100); return "Pinzoro"; }
            else { GameManager.instance.SetScore(d1 * 14); return $"Arashi"; }
        }
        else if (d1 == 1 && d2 == 2 && d3 == 3) return "Hihumi - No score";
        else if (d1 == 4 && d2 == 5 && d3 == 6) 
            { GameManager.instance.SetScore(13); return "Shigoro"; }
        else if (d1 == d2 || d2 == d3)
        {
            int same, diff;

            if (d1 == d2) { same = d1; diff = d3; }
            else { same = d2; diff = d1; }

            GameManager.instance.SetScore(diff * 2);

            return $"pair ({same}, {diff}, {same})";
        }
        else {
            if (++indeterminateReroll >= 3) {
                GameManager.instance.DisplayScore();
                GameManager.instance.gameResult.SetActive(true);
                return "All chance has ran out";
            }
            return "Indeterminate - Re-roll";
        }
    }

    public void ResetDice()
    {
        foreach (var key in new List<string>(_diceFaces.Keys))
        {
            _diceFaces[key] = 0;
            _diceStatus[key] = false;
        }

        if (patternResult != null) patternResult.text = "Roll Dice!";
        Debug.Log("[DiceManager] 주사위 초기화");
    } 
}
