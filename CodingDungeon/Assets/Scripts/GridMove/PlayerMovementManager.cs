using System;
using UnityEngine;

public class PlayerMovementManager : SceneSingleMono<PlayerMovementManager>
{
    [SerializeField] private GameObject player;
    private GameObject _currentSelectedPlayer;

    private void Start()
    {
        _currentSelectedPlayer = player;
    }

    public void SetPlayer(GameObject player)
    {
        _currentSelectedPlayer = player;
    }

    private GameObject GetPlayer()
    {
        return _currentSelectedPlayer;
    }

    public void MoveCurrentPlayerPlayer(Vector3 moveDirection)
    {
        _currentSelectedPlayer.GetComponent<GridMovement>().Move(moveDirection);
    }
    
}
