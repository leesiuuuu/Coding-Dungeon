using System;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : SceneSingleMono<PartyManager>
{
    public List<Character> Characters;
    [SerializeField] private Character _selectedCharacter;
    public Character SelectedCharacter => _selectedCharacter;
    public void OnSelectCharacter(Character character)
    {
        _selectedCharacter = character;
    }
    
    
}
