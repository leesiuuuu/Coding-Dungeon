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
	public bool Moved = false;
    public event Action<GameObject> OnReturnGameObject;

    private void Start()
    {
        _activeQueueUI.SetActive(false);
        OnSelect+=PartyManager.Instance.OnSelectCharacter;
		OnReturnGameObject += PartyManager.Instance.OnSelectPortrait;
        PlayerPortraitListUI.Instance.OnSelected += character =>
        {
            if (character != _character)
            {
                OnClosed?.Invoke(character);
                _activeQueueUI.SetActive(false);
            }
        };
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Moved)
        {
            OnReturnGameObject?.Invoke(gameObject);
            _activeQueueUI.SetActive(true);
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
