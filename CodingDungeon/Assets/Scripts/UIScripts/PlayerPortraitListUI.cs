using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPortraitListUI : SceneSingleMono<PlayerPortraitListUI>
{
    [SerializeField] private GameObject[] _playerPortraitList;
    [SerializeField] private GameObject[] _checkObjs;
    [SerializeField] private GameObject _playerHand;
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

        for (var i = 0; i < _checkObjs.Length; i++)
        {
            _checkObjs[i].SetActive(false);
        }
    }
    


    public void GetCurrentEventCharacter(Character character)
    {
        OnSelected?.Invoke(character);
        _playerHand.SetActive(true);
    }

    public void OnMoveSelected()
    {
        if (PartyManager.Instance.PortraitPrefab == null)
            return;
        
        PlayerPortraitUI portraitUI = PartyManager.Instance.PortraitPrefab.GetComponent<PlayerPortraitUI>();
        if (portraitUI == null)
            return;
        PartyManager.Instance.PortraitPrefab.transform.GetChild(1).gameObject.SetActive(true);
        PartyManager.Instance.PortraitPrefab.transform.GetChild(0).gameObject.SetActive(false);
        _playerHand.SetActive(false);
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
        for (var i = 0; i < _checkObjs.Length; i++)
        {
            _checkObjs[i].SetActive(true);
        }
    }
    
}