public interface IEntity
{
	EntityAttributes Attributes { get; }
	
	EntityStatus Status { get; }

	void SetAttributes(EntityAttributes value);

	void Die();
}