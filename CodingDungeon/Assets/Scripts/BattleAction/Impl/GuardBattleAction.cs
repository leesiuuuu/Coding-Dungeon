using System.Collections;
using UnityEngine;

public class GuardBattleAction : AbstractBattleAction
{
	public GuardBattleAction(Character user, IEntity target) : base(user, target)
	{
	}

	public override IEnumerator StartAction()
	{
		var cameraManager = CameraManager.Instance;
		cameraManager.SetTarget(User.gameObject.transform);
		cameraManager.StartFollow(10);
		cameraManager.ZoomIn(3f);
		yield return new WaitForSeconds(1f);
		User.gameObject.GetComponent<CharacterAnimator>().SetAnimation(EntityMoves.Attack);
		yield return new WaitForSeconds(1f);
		DefenseMultiplyEffect effect = new DefenseMultiplyEffect(1, 3.33f);
		Target.Status.AddStatusEffect(effect);
		yield return new WaitForSeconds(1f);
		cameraManager.SetTarget(cameraManager.MidPos.transform);
		cameraManager.StartFollow(10);
		cameraManager.ResetZoom();
		yield break;
	}
}
