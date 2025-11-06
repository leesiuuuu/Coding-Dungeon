using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class EnemyRuntimeStatus
{
	public int maxHealth;
	public int health;
	public int attack;
	public int defense;
}

public class Enemy : MonoBehaviour, IEntity
{
	[SerializeField] private EnemySo _setting;
	[SerializeField] protected Fsm _fsm;
	[SerializeField] protected PathFinder _pathFinder;
	public EntityAttributes Attributes { get; private set; }
	
	public EntityStatus Status { get; private set; }
	
	public void SetAttributes(EntityAttributes value)
	{
		Attributes = value;
	}

	public void Die()
	{
		Debug.Log($"[Enemy] {_setting.name} 캐릭터 사망!!");
	}

	private void Start()
	{
		Initialize();
	}

	protected virtual void Initialize()
	{
		SetAttributes(_setting.Attributes);
		Status = new EntityStatus(this);
	}

	protected virtual void Decision()
	{
		
	}

	protected virtual void SetTarget()
	{
		
	}
}