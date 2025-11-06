using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPortraitListUI : SceneSingleMono<PlayerPortraitListUI>
{
    [SerializeField] private GameObject[] _playerPortraitList;
    [SerializeField] private GameObject[] _checkObjs;
    [SerializeField] private GameObject[] _playerSelectFrame;
    [SerializeField] private CardHandSystem _playerHand;
    private PartyManager _partyManager;
    private Character _userTemp;
    private GameObject _selectedPortrait;
    public event Action<Character> OnSelected;

    private void Start()
    {
        _partyManager = PartyManager.Instance;
        PartyManager.Instance.InitializeParty += OnRefresh;
        //TurnManager.Instance.OnMonsterMoves += OnRefresh;
        for (var i = 0; i < _partyManager.Characters.Count; i++)
        {
            _playerPortraitList[i].GetComponent<Image>().sprite = _partyManager.Characters[i].Setting.Image;
            var playerPortraitUI = _playerPortraitList[i].GetComponent<PlayerPortraitUI>();
            playerPortraitUI.SetCharacter(_partyManager.Characters[i]);
            playerPortraitUI.OnSelect += GetCurrentEventCharacter;
        }
        for (var i = 0; i < _playerSelectFrame.Length; i++)
        {
            _playerSelectFrame[i].SetActive(false);
        }

        for (var i = 0; i < _checkObjs.Length; i++)
        {
            _checkObjs[i].SetActive(false);
        }
    }

    private bool TryFinishTurn()
    {
        bool canFinish = _checkObjs.All(i => i.activeSelf);
        if (canFinish)
        {
            TurnManager.Instance.SetTurnStatus(TurnStatus.MonsterMoves);
        }
        return canFinish;
    }


    public void GetCurrentEventCharacter(Character character)
    {
        _playerHand.gameObject.SetActive(true);
        for (var i = 0; i < _playerSelectFrame.Length; i++)
        {
            _playerSelectFrame[i].SetActive(false);
        }
        OnSelected?.Invoke(character);
    }

    public void SetPortraitUIInteractable(PlayerPortraitUI portrait, bool active)
    {
        if (portrait == null)
            return;
        PartyManager.Instance.PortraitPrefab.transform.GetChild(1).gameObject.SetActive(!active);
        PartyManager.Instance.PortraitPrefab.transform.GetChild(0).gameObject.SetActive(active);
        portrait.IsInteractable = active;
    }

    private void SetInteractables(bool value)
    {
        foreach (var i in _playerPortraitList)
        {
            i.GetComponent<PlayerPortraitUI>().IsInteractable = value;
        }
    }

    public void OnMoveSelected()
    {
        if (PartyManager.Instance.PortraitPrefab == null)
            return;
        
        PlayerPortraitUI portraitUI = PartyManager.Instance.PortraitPrefab.GetComponent<PlayerPortraitUI>();
        if (portraitUI.IsInteractable)
        {
            SetInteractables(false);
            TileSelecerManager.Instance.tileSelector.OnTileSelected += _ => OnMoveLocationSelected();
            TileSelecerManager.Instance.OnSetTile();
            
            _playerHand.gameObject.SetActive(false);
            SetPortraitUIInteractable(portraitUI, false);
        }
    }

    private void OnMoveLocationSelected()
    {
        TryFinishTurn();
     
        SetInteractables(true);
        _playerHand.gameObject.SetActive(true);
        _playerHand.ClearAllCards();
    }

    public void OnActiveQueueSubmitted(Character user)
    { 
        PlayerPortraitUI portraitUI = PartyManager.Instance.PortraitPrefab.GetComponent<PlayerPortraitUI>();
        if (portraitUI.IsInteractable)
        {
            SetInteractables(false);
            _userTemp = user;
            var requiredTarget = user.CardHolder.GetRequiredTarget();
            SetPortraitUIInteractable(portraitUI, false);
            
            if (requiredTarget != EntityTarget.NONE)
            {
                BattleTargetSelector.Instance.OnSelected += OnBattleTargetSelectedHandler;
                BattleTargetSelector.Instance.StartTargetSelection();
                
                _playerHand.gameObject.SetActive(false);
            }
            else
            {
                // 타겟이 필요없는 경우 바로 실행
                OnBattleTargetSelected(_userTemp, null);
            }
        }
    }

    private void OnBattleTargetSelectedHandler(IEntity target)
    {
        BattleTargetSelector.Instance.OnSelected -= OnBattleTargetSelectedHandler;
        OnBattleTargetSelected(_userTemp, target);
        _userTemp = null;
    }

    private void OnBattleTargetSelected(Character user, IEntity target)
    {
        SetInteractables(true);
        _playerHand.ClearAllCards();
        _playerHand.gameObject.SetActive(true);
        BattleManager.Instance.SubmitAction(user, target);

        TryFinishTurn();
    }

    public void OnRefresh()
    {
        for (var i = 0; i < _partyManager.Characters.Count; i++)
        {
            var playerPortraitUI = _playerPortraitList[i].GetComponent<PlayerPortraitUI>();
            SetPortraitUIInteractable(playerPortraitUI, true);
        }
    }
    
}