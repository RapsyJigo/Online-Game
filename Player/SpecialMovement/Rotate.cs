using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Rotate : NetworkBehaviour
{
    public Transform gun;
    public Transform player;

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        gun.right = new Vector2(mousePos.x - player.position.x, mousePos.y - player.position.y);
    }

}
