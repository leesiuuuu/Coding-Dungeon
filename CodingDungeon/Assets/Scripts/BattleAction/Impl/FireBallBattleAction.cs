using System.Collections;
using UnityEngine;

public class FireBallBattleAction : AbstractBattleAction
{
	public FireBallBattleAction(Character user, IEntity target) : base(user, target)
	{
	}

	public override IEnumerator StartAction()
	{
		//Debug.Log(CameraManager.Instance);
		
		CameraManager.Instance.SetTarget(User.gameObject.transform);
		CameraManager.Instance.StartFollow(10);
		CameraManager.Instance.ZoomIn(3f);
		yield return new WaitForSeconds(1f);
		User.gameObject.GetComponent<CharacterAnimator>().SetAnimation(EntityMoves.Attack);
		yield return new WaitForSeconds(1f);
		//CameraManager.Instance.SetTarget(()Target);
		EntityDamageEffect damageEffect = new EntityDamageEffect(0, 20);
		damageEffect.Damage = (int)(damageEffect.Damage * User.Attributes.DamageModifier);
		
		Target.Status.AddStatusEffect(damageEffect);
		yield break;
	}
}
