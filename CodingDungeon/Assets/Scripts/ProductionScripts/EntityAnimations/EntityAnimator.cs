using UnityEngine;

public enum EntityMoves
{
    Idle,
    Attack,
    Hit,
    Death
}

public abstract class EntityAnimator : MonoBehaviour
{
    public Animator Ani;

    public virtual void SetAnimation(EntityMoves moves)
    {
        
    }
    public virtual void ChangeAnimation(string code)
    {
        
    }
}
