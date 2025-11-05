using UnityEngine;

public class PlayerTurnUI : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    void Start()
    {
        TurnManager.Instance.OnPlayerSelection+=OnPlayerSelection;
    }

    private void OnPlayerSelection()
    {
        _animator.SetTrigger("Apear");    
    }

    public void ExitPlayerSelection()
    {
        _animator.SetTrigger("Disapear");
    }
}
