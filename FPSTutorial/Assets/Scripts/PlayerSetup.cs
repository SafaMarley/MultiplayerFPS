using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] private Behaviour[] componentsToDisable;

    [SerializeField] private string remoteLayerName = "RemotePlayer";
    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            if (Camera.main != null)
            {
                Camera.main.gameObject.SetActive(false);
            }
        }
        RegisterPlayer();
    }

    void RegisterPlayer()
    {
        string id = "Player" + GetComponent<NetworkIdentity>().netId;
        transform.name = id;
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    private void OnDisable()
    {
        if (Camera.main != null)
        {
            Camera.main.gameObject.SetActive(true);
        }
    }
}
