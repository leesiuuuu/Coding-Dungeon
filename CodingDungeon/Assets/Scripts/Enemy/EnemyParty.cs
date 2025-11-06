using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParty : SceneSingleMono<EnemyParty>
{
    [SerializeField] protected List<Enemy>_party;
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

        }
        else
        {
            //게임 승리 로직
        }

    }

    public void NextEnemy()
    {
        Index++;
    }
}
