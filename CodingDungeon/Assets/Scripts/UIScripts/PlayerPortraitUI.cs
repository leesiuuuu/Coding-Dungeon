using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPortraitUI : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private Character _character;
    public bool CanSelect = true;
    public event Action<Character> OnSelect;

    private void Start()
    {
        OnSelect+=PartyManager.Instance.OnSelectCharacter;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CanSelect)
        {
            CanSelect = false;
            OnSelect?.Invoke(_character);
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
