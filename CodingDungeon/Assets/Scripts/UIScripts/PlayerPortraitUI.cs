using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPortraitUI : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private Character _character;
    public bool Moved = false;
    public event Action<Character> OnSelect;
    public event Action<GameObject> OnReturnGameObject;
    private void Start()
    {
        OnSelect+=PartyManager.Instance.OnSelectCharacter;
        OnReturnGameObject += PartyManager.Instance.OnSelectPortrait;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Moved)
        {
            OnSelect?.Invoke(_character);
            OnReturnGameObject?.Invoke(gameObject);
        }
    }

    public Character GetCharacter()
    {
        return _character;
    }

    public void SetCharacter(Character character)
    {
        _character = character;
    }
    
}
