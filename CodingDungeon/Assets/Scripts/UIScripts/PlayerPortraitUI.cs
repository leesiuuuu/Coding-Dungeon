using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPortraitUI : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private int index;
    [SerializeField] private GameObject _activeQueueUI;
    public static bool CanSelect = true;
    public event Action<Character> OnSelect;
    public event Action<Character> OnClosed; 
    public bool IsInteractable = true;
    public event Action<GameObject> OnReturnGameObject;

    private Character _character;

    /// <summary>
    /// Called By UGUI Button
    /// </summary>
    public void SubmitActiveQueue()
    {
        PlayerPortraitListUI.Instance.OnActiveQueueSubmitted(_character);
    }

	private void Start()
    {
		_character = PartyManager.Instance.Characters[index];
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
        if (IsInteractable)
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
