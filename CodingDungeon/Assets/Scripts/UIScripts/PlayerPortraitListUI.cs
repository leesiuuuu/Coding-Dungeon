using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPortraitListUI : SceneSingleMono<PlayerPortraitListUI>
{
    [SerializeField] private GameObject[] _playerPortraitList;
    private PartyManager _partyManager;
    private Character _character;
    public Character Character=>_character;
    private GameObject _selectedPortrait;
    public event Action<Character> OnSelected;

    private void Start()
    {
        _partyManager = PartyManager.Instance;
        PartyManager.Instance.InitializeParty += OnRefresh;
        for (var i = 0; i < _partyManager.Characters.Count; i++)
        {
            _playerPortraitList[i].GetComponent<Image>().sprite = _partyManager.Characters[i].Setting.Image;
            var playerPortraitUI = _playerPortraitList[i].GetComponent<PlayerPortraitUI>();
            playerPortraitUI.SetCharacter(_partyManager.Characters[i]);
            playerPortraitUI.OnSelect += GetCurrentEventCharacter;
        }
    }
    


    public void GetCurrentEventCharacter(Character character)
    {
        OnSelected?.Invoke(character);
    }

    public void OnMoveSelected()
    {
        if (PartyManager.Instance.PortraitPrefab == null)
            return;
        
        PlayerPortraitUI portraitUI = PartyManager.Instance.PortraitPrefab.GetComponent<PlayerPortraitUI>();
        if (portraitUI == null)
            return;
        
        if (!portraitUI.Moved)
        {
            TileSelecerManager.Instance.OnSetTile();
        }
        portraitUI.Moved = true;
    }

    public void OnRefresh()
    {
        for (var i = 0; i < _partyManager.Characters.Count; i++)
        {
            var playerPortraitUI = _playerPortraitList[i].GetComponent<PlayerPortraitUI>();
            playerPortraitUI.Moved = false;
        }
    }
    
}