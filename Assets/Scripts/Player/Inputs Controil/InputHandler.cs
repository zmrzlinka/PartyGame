using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    PlayerInput inputMap;
    SelectionMenuElement selection;
    PlayerControl playerPrefab;
    PlayerControl InGamePlayer;
    private void Awake()
    {
        inputMap = GetComponent<PlayerInput>();
    }

    public void Init(SelectionMenuElement selControl)
    {
        selection = selControl;
        selection.Activate();
    }

    //Selecting UI Controls
    public void SelectingChange(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (!selection.active)
            {
                selection.Activate();
                return;
            }
            if (ctx.ReadValue<float>() > 0)
            {
                selection.ChangeRight();
            }
            else
            {
                selection.ChangeLeft();
            }
        }
    }
    public void CorfirmSlecrtion(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            playerPrefab = selection.Ready();
        }
    }

    //Spawning players
    public void SpawnPlayer(Transform position,Material mat)
    {
        InGamePlayer = Instantiate(playerPrefab);
        InGamePlayer.transform.SetParent(this.transform);
        InGamePlayer.transform.position = position.position;
        InGamePlayer.gameObject.GetComponent<SpriteRenderer>().material = mat;
    }
    public void DestroyPlayer()
    {
        if (InGamePlayer == null) return;
        Destroy(InGamePlayer);
    }
    //switching control maps
    public void SwitchControlsToGame()
    {
        inputMap.SwitchCurrentActionMap("Game");
    }
    public void SwitchControlsToMenu()
    {
        inputMap.SwitchCurrentActionMap("UI");
    }


    //Game controls
    public void Move(InputAction.CallbackContext ctx)
    {
        if (CheckInGamePlayer() && ctx.performed)
        {
            InGamePlayer.move.OnMove(ctx.ReadValue<float>());
        }
        else if (CheckInGamePlayer() && ctx.canceled)
        {
            InGamePlayer.move.Stop();
        }
    }
    public void Jump(InputAction.CallbackContext ctx)
    {
        if (CheckInGamePlayer() && ctx.started)
        {
            InGamePlayer.move.OnJump();
        }
    }
    public void Drop(InputAction.CallbackContext ctx)
    {
        if (CheckInGamePlayer() && ctx.performed)
        {
            InGamePlayer.move.OnDrop();
        }
    }
    public void Action(InputAction.CallbackContext ctx)
    {
        if (CheckInGamePlayer() && ctx.started)
        {
            InGamePlayer.OC.OnAction();
        }
    }
    //checking if p[layer is alive
    bool CheckInGamePlayer()
    {
        if (InGamePlayer == null) return false;
        return true;
    }
}