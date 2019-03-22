using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Swordsman : NetworkBehaviour
{
    [Tooltip("Spawn Point for all actions")] public GameObject spawnPoint;

    [Header("Swing settings")]
    [Tooltip("Swing hitbox")] public GameObject swing;
    [SerializeField] [Tooltip("Swing Cooldown")] private float swingCooldown;
    private bool canSwing;

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetMouseButtonDown(0) && canSwing)
        {
            Debug.Log("Swung");
            CmdSpawnSwingHitbox();

            canSwing = false;
            StartCoroutine(waitSwing());
        }
    }
    [Command]
    private void CmdSpawnSwingHitbox()
    {
        GameObject s = Instantiate(swing, spawnPoint.transform);

        NetworkServer.Spawn(s);
    }
    IEnumerator waitSwing ()
    {
        yield return new WaitForSeconds(swingCooldown);
        canSwing = true;
    }
}
