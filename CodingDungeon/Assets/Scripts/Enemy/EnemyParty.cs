using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParty : SceneSingleMono<EnemyParty>
{
    [SerializeField] protected List<Enemy>_party;

    public List<Enemy> Party
    {
        get => _party;
        set => _party = value;
    }
    private int _index = 0;

    public int Index
    {
        get => _index;
        set
        {
            if (value != _index)
            {
                _index = value;
                ExecuteMove();
            }
        }
    }

    private void Start()
    {
        TurnManager.Instance.OnMonsterMoves += EnemyMove;
    }

    public void EnemyMove()
    {
        _index = 0;
        ExecuteMove();
    }
    public void ExecuteMove()
    {
        Debug.Log(_index);
        if (_party.Count > 0)
        {
            if (_index >= 0 && _index < _party.Count)
            {
                if (_party[_index].Attributes.CurrentHp <= 0)
                {
                    _party.RemoveAt(_index);
                }
                else
                {
                    _party[_index].Decision();
                }
            }
            else if (_index >= _party.Count)
            {
                TurnManager.Instance.SetTurnStatus(TurnStatus.PlayerMoves);
                Debug.Log("모든 적 행동 완료");
                return;
            }
        }
    }

    public void NextEnemy()
    {
        Debug.Log("증가");
        Index++;
    }
}
