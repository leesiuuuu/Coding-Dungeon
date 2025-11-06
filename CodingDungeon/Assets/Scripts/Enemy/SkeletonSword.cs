using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkeletonSword : Enemy
{
    private bool _isActing = false; // 추가

    public override void Decision()
    {
        if (_isActing) return; // 이미 행동 중이면 무시
        
        _isActing = true; // 행동 시작
        
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
        
        if (Vector2.Distance(gameObject.transform.position, _target.transform.position) <= Attributes.AttackRange)
        {
            if (_isPrepared)
            {
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
                StartCoroutine(PrepareFlow());
            }
        }
        else
        {
            Debug.Log(gameObject.name + " 걷는중");
            _isPrepared = false;
            var targetVector = MapManager.Instance.WorldToCell(_target.transform.position);
            _pathFinder.Move(targetVector);
            _notice.SetActive(false);
            StartCoroutine(MoveFlow());
        }
    }

    private IEnumerator MoveFlow()
    {
        yield return new WaitForSeconds(0.2f);
        _isActing = false; // 행동 완료
        EnemyParty.Instance.NextEnemy();
    }

    private IEnumerator AttackFlow()
    {
        yield return null;
        _isActing = false; // 행동 완료
        EnemyParty.Instance.NextEnemy();
    }

    private IEnumerator PrepareFlow()
    {
        yield return new WaitForSeconds(0.1f);
        _isActing = false; // 행동 완료
        EnemyParty.Instance.NextEnemy();
    }
}