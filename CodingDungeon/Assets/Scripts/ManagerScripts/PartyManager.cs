using System;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : SceneSingleMono<PartyManager>
{
    public List<Character> Characters;
    [SerializeField] private Character _selectedCharacter;
    public Character SelectedCharacter => _selectedCharacter;
    private int _characterLimitIndex = 0;
    public void OnSelectCharacter(Character character)
    {
        _selectedCharacter = character;
    }
    
    
}
