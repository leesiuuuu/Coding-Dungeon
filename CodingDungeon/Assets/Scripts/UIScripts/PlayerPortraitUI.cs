using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPortraitUI : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private int index;
	[SerializeField] private GameObject _activeQueueUI;
	[SerializeField] private GameObject _selectedQueueUI;
	[SerializeField] private AudioClip selectedAudio;

	public static bool CanSelect = true;
	public event Action<Character> OnSelect;
	public event Action<Character> OnClosed;
	public bool IsInteractable = true;
	public event Action<GameObject> OnReturnGameObject;
	public event Action<Character> OnCantAllocMemory;

	private Character _character;

	/// <summary>
	/// Called By UGUI Button
	/// </summary>
	public void SubmitActiveQueue()
	{
		var sum = 0;
		foreach (var i in _character.CardHolder.ActiveQueueCards)
		{
			sum += i.Cost;
		}
		if (sum <= 10)
		{
			PlayerPortraitListUI.Instance.OnActiveQueueSubmitted(_character);
		}
		else
		{
			OnCantAllocMemory?.Invoke(_character);
		}
	}

	private void Start()
	{
		StartCoroutine(InitializeCharacter());
		OnCantAllocMemory += PartyManager.Instance.OnCharacterExpload;
	}

	private IEnumerator InitializeCharacter()
	{
		while (PartyManager.Instance.Characters.Count <= index)
		{
			yield return null;
		}
		
		if (index >= 0 && index < PartyManager.Instance.Characters.Count)
		{
			_character = PartyManager.Instance.Characters[index];
			_activeQueueUI.SetActive(false);

			OnSelect += PartyManager.Instance.OnSelectCharacter;
			OnReturnGameObject += PartyManager.Instance.OnSelectPortrait;

			PlayerPortraitListUI.Instance.OnSelected += character =>
			{
				if (character != _character)
				{
					OnClosed?.Invoke(character);
					_activeQueueUI.SetActive(false);
				}
			};

			Debug.Log($"초상화 초기화 완료: {_character.name} (인덱스: {index})");
		}
		else
		{
			Debug.LogError($"인덱스 {index}는 범위를 벗어났습니다. 캐릭터 수: {PartyManager.Instance.Characters.Count}");
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (_character.IsAlive&&IsInteractable && _character != null)
		{
			SoundManager.Instance.SFXPlay("selected", selectedAudio);
			OnReturnGameObject?.Invoke(gameObject);
			_activeQueueUI.SetActive(true);
			_activeQueueUI.GetComponent<Animator>().SetTrigger("Apear");
			OnSelect?.Invoke(_character);
			_selectedQueueUI.gameObject.SetActive(true);	
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