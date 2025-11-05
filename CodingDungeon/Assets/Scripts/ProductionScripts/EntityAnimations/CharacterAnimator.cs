using UnityEngine;

public class CharacterAnimator : EntityAnimator
{
    private string _aniTriggerHash1 = "2_Attack";
    private string _aniTriggerHash2 = "3_Damaged";
    private string _aniTriggerHash3 = "4_Death";
    private string _aniBooleanHash1 = "isDeath";
    private bool _isDeath = false;
    
    public override void SetAnimation(EntityMoves playerMoves)
    {
        switch (playerMoves) 
        {
            case EntityMoves.Attack:
                Ani.SetTrigger(_aniTriggerHash1);
                break;
            case EntityMoves.Hit:
                Ani.SetTrigger(_aniTriggerHash2);
                break;
            case EntityMoves.Death:
                _isDeath = true;
                Ani.SetTrigger(_aniTriggerHash3);
                Ani.SetBool(_aniBooleanHash1, _isDeath);
                break;
        }
    }   

}
