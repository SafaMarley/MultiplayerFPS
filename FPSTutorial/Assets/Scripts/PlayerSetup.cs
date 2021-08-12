using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] private Behaviour[] componentsToDisable;
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            if (Camera.main != null)
            {
                Camera.main.gameObject.SetActive(false);
            }
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
