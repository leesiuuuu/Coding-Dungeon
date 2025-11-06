using System;
using UnityEngine;

[Serializable]
public class EnemyRuntimeStatus
{
	public int maxHealth;
	public int health;
	public int attack;
	public int defense;
}

public class Enemy : MonoBehaviour
{
	[SerializeField] private EnemyStatusSO _enemyStatsSo;
	[SerializeField] protected Fsm _fsm;
	[SerializeField] protected PathFinder _pathFinder;
	protected EnemyRuntimeStatus _runtimeStatus = new();
	

	private void Start()
	{
		Initialize();
	}

	protected virtual void Initialize()
	{
		_runtimeStatus.maxHealth = _enemyStatsSo.maxHealth;
		_runtimeStatus.health = _runtimeStatus.maxHealth;
		_runtimeStatus.attack = _enemyStatsSo.attack;
		_runtimeStatus.defense = _enemyStatsSo.defense;
	}

	protected virtual void Decision()
	{
		
	}

	protected virtual void SetTarget()
	{
		
	}
}