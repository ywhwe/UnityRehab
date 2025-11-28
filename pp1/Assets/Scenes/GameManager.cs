using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Dice> allDice = new List<Dice>();
    public CheckZone checkZone;
    public GameObject gameEnd;
    [HideInInspector] public GameManager instance;

    void Awake()
    {
        if (checkZone == null)
        {
            enabled = false;
            return;
        }
        if (allDice.Count == 0) Debug.Log("[GameManager] allDice 리스트가 비어있음");

        if (gameEnd != null) gameEnd.SetActive(false);
        instance = this;
    }

    void Start()
    {
        InitialGameSetup();
    }

    void InitialGameSetup()
    {
        Debug.Log("[GameManager] 게임 초기화 시작");
        if (checkZone != null) checkZone.ResetAllDice();

        foreach (Dice dice in allDice)
        {
            if (dice != null) dice.ResetPosition();
        }
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
}
