using System.Collections;
using UnityEngine;

public class SmashBattleAction : AbstractBattleAction
{
	public SmashBattleAction(Character user, IEntity target) : base(user, target)
	{
	}

	public override IEnumerator StartAction()
	{
		var cameraManager = CameraManager.Instance;
		//Debug.Log(cameraManager);
		if (Target.Attributes.CurrentHp <= 0)
		{
			cameraManager.SetTarget(cameraManager.MidPos.transform);
			cameraManager.StartFollow(10);
			cameraManager.ResetZoom();
			yield break;
		}
		Debug.Log(User.source);
		cameraManager.SetTarget(User.source.transform);
		cameraManager.StartFollow(10);
		cameraManager.ZoomIn(3f);
		yield return new WaitForSeconds(1f);
		User.gameObject.GetComponent<CharacterAnimator>().SetAnimation(EntityMoves.Attack);
		yield return new WaitForSeconds(1f);
		cameraManager.SetTarget(Target.source.gameObject.transform);
		cameraManager.StartFollow(10);
		cameraManager.ZoomIn(3f);
		yield return new WaitForSeconds(1f);
		Target.source.gameObject.GetComponent<CharacterAnimator>().SetAnimation(EntityMoves.Hit);
		DamageEffect damageEffect = new DamageEffect(0, 11);
		damageEffect.Damage = (int)(damageEffect.Damage * User.Attributes.DamageModifier);
		Target.Status.AddStatusEffect(damageEffect);
		
		yield return new WaitForSeconds(1f);
		cameraManager.SetTarget(cameraManager.MidPos.transform);
		cameraManager.StartFollow(10);
		cameraManager.ResetZoom();
		yield break;;
	}
}
