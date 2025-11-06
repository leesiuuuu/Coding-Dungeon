using UnityEngine;

public interface IEntity
{
	GameObject source { get; }
	
	EntityAttributes Attributes { get; }
	
	EntityStatus Status { get; }

	void SetAttributes(EntityAttributes value);

	void Die();
}