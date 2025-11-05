using UnityEngine;

public class PlayerTurnUI : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    void Start()
    {
        TurnManager.Instance.OnPlayerSelection+=OnPlayerSelection;
        TurnManager.Instance.OnMonsterMoves += ExitPlayerSelection;
    }

    private void OnPlayerSelection()
    {
        _animator.SetTrigger("Apear");    
    }

    private void ExitPlayerSelection()
    {
        _animator.SetTrigger("Disapear");
    }
}
