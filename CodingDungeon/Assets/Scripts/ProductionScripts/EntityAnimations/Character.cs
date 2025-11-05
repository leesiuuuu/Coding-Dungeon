using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    //이 코드 전체는 디버그용 코드입니다
    //이렇게 캐릭터 애니메이터로 때와서 사용하심 됩니다
    
    [SerializeField] private CharacterAnimator _chAnimator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _chAnimator.SetAnimation(EntityMoves.Hit);//해당 행동 이넘 실행하면 내부적으로 애니메이션 그래프를 조절해서 바꿀 수 있습니다
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            _chAnimator.SetAnimation(EntityMoves.Attack);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _chAnimator.SetAnimation(EntityMoves.Death);
        }
    }
}
