using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPortraitUI : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private Character _character;
    [SerializeField] private GameObject _activeQueueUI;
    public static bool CanSelect = true;
    public event Action<Character> OnSelect;
    public event Action<Character> OnClosed;

    private void Start()
    {
        _activeQueueUI.SetActive(false);
        OnSelect+=PartyManager.Instance.OnSelectCharacter;
        PlayerPortraitListUI.Instance.OnSelected += character =>
        {
            if (character != _character)
            {
                Debug.Log("[PlayerPortraitUI] Closed: " + character.name);
                OnClosed?.Invoke(character);
                _activeQueueUI.SetActive(false);
            }
        };
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CanSelect)
        {
            CanSelect = false;
            OnSelect?.Invoke(_character);
            _activeQueueUI.SetActive(true);
            Debug.Log("[PlayerPortraitUI] Selected: " + _character.Setting.Name);
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
