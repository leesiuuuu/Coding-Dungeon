using System;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : SceneSingleMono<PartyManager>
{
    public List<Character> Characters = new List<Character>();
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
        
        foreach (var character in Characters)
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
        if (!Characters.Contains(character))
        {
            Characters.Add(character);
            CharacterObjects[character] = characterObject;
        }
    }

    public void RemoveCharacter(Character character)
    {
        if (Characters.Contains(character))
        {
            Characters.Remove(character);
            CharacterObjects.Remove(character);
        }
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