using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    private const string player_tag = "Player";
    
    public PlayerWeapon weapon;
    
    [SerializeField] private Camera cam;

    [SerializeField] private LayerMask mask;

    private void Start()
    {
        if (cam == null)
        {
            Debug.LogError("No camera referenced");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    [Client]
    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
        {
            if (hit.collider.tag == player_tag)
            {
                CmdPlayerShot(hit.collider.name);
            }
        }
    }

    [Command]
    void CmdPlayerShot(string id)
    {
        Debug.Log(id + "has been shot");
    }
}
