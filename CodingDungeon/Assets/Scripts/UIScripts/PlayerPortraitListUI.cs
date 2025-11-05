using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPortraitListUI : SceneSingleMono<PlayerPortraitListUI>
{
    [SerializeField] private GameObject[] _playerPortraitList;
    private PartyManager _partyManager;
    public event Action<Character> OnSelected;
    
    private void Start()
    {
        _partyManager = PartyManager.Instance;
        for (var i = 0; i < _partyManager.Characters.Count; i++)
        {
            _playerPortraitList[i].GetComponent<Image>().sprite = _partyManager.Characters[i].Setting.Image;
            var playerPortraitUI = _playerPortraitList[i].GetComponent<PlayerPortraitUI>();
            playerPortraitUI.SetCharacter(_partyManager.Characters[i]);
            playerPortraitUI.OnSelect += GetCurrentEventCharacter;
        }
    }
    

    public void OnTurnSkip()
    {
        PlayerPortraitUI.CanSelect = true;
    }

    public void GetCurrentEventCharacter(Character character)
    {
        OnSelected?.Invoke(character);
    }
    
}
