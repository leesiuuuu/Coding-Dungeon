using UnityEngine;
using UnityEngine.UI;

public class PlayerPortraitListUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerPortraitList;
    private PartyManager _partyManager;
    private Character _character;
    public Character Character=>_character;
    
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
        _character = character;
    }
    
}
