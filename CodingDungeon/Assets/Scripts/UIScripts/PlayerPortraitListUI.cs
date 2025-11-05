using UnityEngine;
using UnityEngine.UI;

public class PlayerPortraitListUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerPortraitList;
    //public event Action On
    private void Start()
    {
        var partyManager = PartyManager.Instance;
        for (var i = 0; i < partyManager.Characters.Count; i++)
        {
            _playerPortraitList[i].GetComponent<Image>().sprite = partyManager.Characters[i].Setting.Image;
            _playerPortraitList[i].GetComponent<PlayerPortraitUI>().SetCharacter(partyManager.Characters[i]);
        }
    }
    

    public void OnTurnSkip()
    {
        //PlayerPortraitUI.CanSelect = true;
    }
    //public void Re
    
}
