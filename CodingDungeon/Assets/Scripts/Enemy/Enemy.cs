using System;
using UnityEngine;

[Serializable]
public class EnemyRuntimeStatus
{
	public int maxHealth;
	public int health;
	public int attack;
	public int defense;
	public int attackRange;
}

public class Enemy : MonoBehaviour
{
	[SerializeField] private EnemyStatusSO _enemyStatsSo;
	[SerializeField] protected PathFinder _pathFinder;
	[SerializeField] protected GameObject _target;
	[SerializeField] protected GameObject _notice;
	protected bool _isPrepared;
	public EnemyRuntimeStatus RuntimeStatus = new();
	
	

	private void Start()
	{
		Initialize();
	}

	protected virtual void Initialize()
	{
		_notice.SetActive(false);
		RuntimeStatus.maxHealth = _enemyStatsSo.maxHealth;
		RuntimeStatus.health = RuntimeStatus.maxHealth;
		RuntimeStatus.attack = _enemyStatsSo.attack;
		RuntimeStatus.defense = _enemyStatsSo.defense;
		RuntimeStatus.attackRange = _enemyStatsSo.attackRange;
	}
	public virtual void Decision(){}
	protected virtual void SetTarget(){}
}