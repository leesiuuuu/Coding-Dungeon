using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSword : Enemy
{

	public override void Decision()
	{
		var party = PartyManager.Instance.Characters;
		var minDist = 10000f;
		GameObject semiTarget = null;
		for (int i = 0; i < party.Count; i++)
		{
			if (Vector2.Distance(gameObject.transform.position, party[i].transform.position) < minDist)
			{
				minDist = Vector2.Distance(gameObject.transform.position, party[i].transform.position);
				semiTarget = PartyManager.Instance.CharacterObjects[party[i]];
			}
			
		}
		_target = semiTarget;
		if (Vector2.Distance(gameObject.transform.position, _target.transform.position) <= RuntimeStatus.attackRange)
		{
			if (_isPrepared)
			{
				//Attack
				Debug.Log(gameObject.name + " 공격함");
				_notice.SetActive(false);
				_isPrepared = false;
				StartCoroutine(AttackFlow());
			}
			else
			{
				Debug.Log(gameObject.name + "준비됨");
				_isPrepared = true;
				_notice.SetActive(true);
			}
		}
		else
		{
			Debug.Log(gameObject.name + " 걷는중");
			_isPrepared = false;
			var targetVector=MapManager.Instance.WorldToCell(_target.transform.position);
			_pathFinder.Move(targetVector);
			_notice.SetActive(false);
			StartCoroutine(MoveFlow());

		}
	}

	private IEnumerator MoveFlow()
	{
		yield return new WaitForSeconds(0.2f);
		EnemyParty.Instance.NextEnemy();
	}

	private IEnumerator AttackFlow()
	{
		yield return null;
		EnemyParty.Instance.NextEnemy();
	}

	private IEnumerator PrepareFlow()
	{
		yield return new WaitForSeconds(0.1f);
		EnemyParty.Instance.NextEnemy();
	}
	
}