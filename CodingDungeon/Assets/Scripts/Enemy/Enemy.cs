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
	public int attackRange;
}

public class Enemy : MonoBehaviour, IEntity
{

	[SerializeField] protected GameObject _target;
	[SerializeField] protected GameObject _notice;
	protected bool _isPrepared;
	[SerializeField] private EnemySo _setting;
	[SerializeField] protected PathFinder _pathFinder;
	[SerializeField] private Collider2D _collider;

	public GameObject source => gameObject;

	public EntityAttributes Attributes { get; private set; }

	
	public EntityStatus Status { get; private set; }
	
	public void SetAttributes(EntityAttributes value)
	{
		Attributes = value;
	}

	public void Die()
	{
		gameObject.GetComponent<CharacterAnimator>().SetAnimation(EntityMoves.Death);
		_collider.enabled = false;
		EnemyParty.Instance.currentEnemyCount--;
		if (EnemyParty.Instance.currentEnemyCount<=0)
		{
			Debug.Log("승리");
		}
		Debug.Log($"[Enemy] {_setting.name} 캐릭터 사망!!");
	}

	private void Start()
	{
		Initialize();
	}

	protected virtual void Initialize()
	{

		_notice.SetActive(false);
		SetAttributes(_setting.Attributes);
		Status = new EntityStatus(this);
	}
	public virtual void Decision(){}
	protected virtual void SetTarget(){}
}