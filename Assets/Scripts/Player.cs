using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    [SerializeField] Behaviour[] componentsDisable;

    private void Start()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Game2Player);

        if (!isLocalPlayer)
        {
            foreach (var component in componentsDisable)
            {
                component.enabled = false;
            } 
        }
    }

    void OnPlayerDisconnected(NetworkPlayer pl)
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Menu);
        Network.RemoveRPCs(pl);
        Network.DestroyPlayerObjects(pl);
    }
}
