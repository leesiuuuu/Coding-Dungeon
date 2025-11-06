using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : SceneSingleMono<PartyManager>
{
	private List<Character> characters = new List<Character>();
	public PlayerSpanwer spanwer;

	// 무한 재귀 방지 플래그
	private bool isSpawning = false;

	public List<Character> Characters
	{
		get
		{
			// 리스트가 비어있고, 현재 스폰 중이 아닐 때만 실행
			if (!isSpawning && (characters == null || characters.Count == 0))
			{
				isSpawning = true; // 플래그 설정
				spanwer?.SpawnPlayers();
				isSpawning = false; // 플래그 해제
			}
			return characters;
		}
		set
		{
			characters = value;
		}
	}

	public Dictionary<Character, GameObject> CharacterObjects = new Dictionary<Character, GameObject>();

	[SerializeField] private Character _selectedCharacter;
	public Character SelectedCharacter => _selectedCharacter;

	[SerializeField] private GameObject _portraitPrefab;
	public GameObject PortraitPrefab => _portraitPrefab;

	[SerializeField] private GameObject _realPlayerPrefab;
	public GameObject RealPlayerPrefab => _realPlayerPrefab;

	public event Action InitializeParty;

	private void Start()
	{
		InitializeCharacterDictionary();
	}

	private void InitializeCharacterDictionary()
	{
		CharacterObjects.Clear();

		// Characters getter를 직접 호출하지 않고 private 필드 사용
		foreach (var character in characters)
		{
			GameObject characterObject = GameObject.Find(character.name);

			if (characterObject != null)
			{
				CharacterObjects[character] = characterObject;
				Debug.Log($"캐릭터 등록: {character.name} -> {characterObject.name}");
			}
			else
			{
				Debug.LogWarning($"캐릭터 오브젝트를 찾을 수 없음: {character.name}");
			}
		}
	}

	public void AddCharacter(Character character, GameObject characterObject)
	{
		// 직접 리스트에 추가 (getter 호출 방지)
		if (!characters.Contains(character))
		{
			characters.Add(character);
			CharacterObjects[character] = characterObject;
			Debug.Log($"캐릭터 추가: {character.name}");
		}
	}

	public void RemoveCharacter(Character character)
	{
		// 직접 리스트에서 제거 (getter 호출 방지)
		if (characters.Contains(character))
		{
			characters.Remove(character);
			CharacterObjects.Remove(character);
			Debug.Log($"캐릭터 제거: {character.name}, 남은 캐릭터: {characters.Count}명");

			// 모두 제거되었는지 체크
			if (characters.Count == 0)
			{
				OnAllCharactersDead();
			}
		}
	}

	// 모든 캐릭터가 사라졌을 때 실행
	private void OnAllCharactersDead()
	{
		Debug.Log("모든 캐릭터가 사라졌습니다!");
		// 게임 오버 로직 추가 가능
	}

	public void SetCharacterObject(Character character)
	{
		if (CharacterObjects.ContainsKey(character))
		{
			if (_realPlayerPrefab != null)
			{
				PathFinder oldPathFinder = _realPlayerPrefab.GetComponent<PathFinder>();
				if (oldPathFinder != null)
				{
					TileSelecerManager.Instance.tileSelector.OnTileSelected -= oldPathFinder.Move;
				}
			}

			_realPlayerPrefab = CharacterObjects[character];

			if (_realPlayerPrefab != null)
			{
				PathFinder newPathFinder = _realPlayerPrefab.GetComponent<PathFinder>();
				if (newPathFinder != null)
				{
					TileSelecerManager.Instance.tileSelector.OnTileSelected += newPathFinder.Move;
				}
			}
		}
	}

	public void OnSelectCharacter(Character character)
	{
		_selectedCharacter = character;
		SetCharacterObject(_selectedCharacter);
	}

	public void OnSelectPortrait(GameObject selectedPortrait)
	{
		_portraitPrefab = selectedPortrait;
	}
}