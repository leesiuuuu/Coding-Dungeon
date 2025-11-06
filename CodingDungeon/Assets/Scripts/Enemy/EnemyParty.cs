using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParty : SceneSingleMono<EnemyParty>
{
    [SerializeField] protected List<Enemy> _party;

    public List<Enemy> Party
    {
        get => _party;
        set => _party = value;
    }
    
    private int _index = 0;
    public int currentEnemyCount = 0;

    private void Start()
    {
        currentEnemyCount = _party.Count;
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnMonsterMoves += EnemyMove;
        }
        else
        {
            Debug.LogError("TurnManager.Instance is null!");
        }
    }

    private void OnDestroy()
    {
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnMonsterMoves -= EnemyMove;
        }
    }

    public void EnemyMove()
    {
        _index = 0;
        
        RemoveDeadEnemies();
        
        ExecuteMove();
    }

    private void RemoveDeadEnemies()
    {
        for (int i = _party.Count - 1; i >= 0; i--)
        {
            if (_party[i] == null || _party[i].Attributes.CurrentHp <= 0)
            {
                Debug.Log($"적 제거: Index {i}");
                _party.RemoveAt(i);
            }
        }
    }

    private void ExecuteMove()
    {
        Debug.Log($"ExecuteMove - Current Index: {_index}, Party Count: {_party.Count}");
        
        if (_party.Count == 0 || _index >= _party.Count)
        {
            if (TurnManager.Instance != null)
            {
                TurnManager.Instance.SetTurnStatus(TurnStatus.PlayerMoves);
            }
            Debug.Log("모든 적 행동 완료");
            return;
        }
        
        if (_index >= 0 && _index < _party.Count)
        {
            Enemy currentEnemy = _party[_index];
            
            if (currentEnemy != null)
            {
                if (currentEnemy.Attributes.CurrentHp > 0)
                {
                    Debug.Log($"적 {_index} 행동 실행");
                    currentEnemy.Decision();
                }
                else
                {
                    Debug.Log($"적 {_index} HP 0 이하, 다음 적으로");
                    NextEnemy();
                }
            }
            else
            {
                Debug.LogWarning($"적 {_index}가 null이거나 Attributes가 null입니다.");
                NextEnemy();
            }
        }
    }

    public void NextEnemy()
    {
        Debug.Log($"NextEnemy 호출 - 현재 Index: {_index}");
        _index++;
        
        ExecuteMove();
    }
}
