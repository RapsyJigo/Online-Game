using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Sniper : NetworkBehaviour
{
    public Transform firePoint;
    public BoxCollider2D body;
    public CircleCollider2D head;
    public GameObject player;

    [Header("Bullet Settings")]
    [Tooltip("Bullet Prefab")]public GameObject bullet;
    [SerializeField][Tooltip("Attack speed")]private float shotCooldown = 1.5f;
    private bool canShoot = true;

    [Header("Trap Settings")]
    [Tooltip("Trap gameobject from player assets")] public GameObject trap;
    [SerializeField][Tooltip("Cooldown Of Trap")] private float trapCooldown = 4.5f;
    [SerializeField][Tooltip("The speed with which the trap leaves the Fire Point")] private float trapSpeed = 70f;
    [SerializeField][Tooltip("The duration of the debuff")] private float trapDuration = 0.75f;
    private bool canTrap = true;

    [Header("Rapidfire Settings")]
    [Tooltip("Bullet prefab")] public GameObject rapidfireBullet;
    [SerializeField] [Tooltip("Cooldown between rapidfires")] private float rapidCooldown;
    [SerializeField] [Tooltip("Ammount of bullets / burst")] private int rapidShots;
    [SerializeField] [Tooltip("Time between shots")] private float fireRate;
    private bool canRapid = true;

    [Header("Return Settings")]
    [SerializeField] [Tooltip("Cooldown between returns")] private float returnCooldown;
    [SerializeField] [Tooltip("Time before being returned")] private float returnTime;
    private bool canReturn = true;

    [Header("Time Warp Settings")]
    [SerializeField] [Tooltip("Time warp cooldown")] private float timeCooldown;
    [SerializeField] [Tooltip("Time warp duration")] private float timeDuration;
    [SerializeField] [Tooltip("Time warp reduction")] private float timeDistortion;
    private bool canTime = true;

    private void Start()
    {
        if (isLocalPlayer)
            trap.GetComponent<SpriteRenderer>().color = Color.green;

        trap.GetComponent<TrapLogic>().duration = trapDuration;
        Physics2D.IgnoreCollision(head, trap.GetComponent<CircleCollider2D>());
        Physics2D.IgnoreCollision(body, trap.GetComponent<CircleCollider2D>());
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            CmdSpawnBullet();

            canShoot = false;
            StartCoroutine(waitShoot(shotCooldown));
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && canTrap) //Trap button
        {
            CmdSpawnTrap();

            canTrap = false;
            StartCoroutine(waitTrap(trapCooldown));
        } 
        if (Input.GetKeyDown(KeyCode.Alpha2) && canRapid) //Rapidfire button
        {
            StartCoroutine(rapidFire());

            canRapid = false;
            StartCoroutine(rapidWait());
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && canReturn) //Speed up button
        {
            Vector3 position = player.transform.position;
            StartCoroutine(returnPoint(position));

            canReturn = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && canTime) //Time button
        {
            CmdtimeDistort(timeDistortion);

            canTime = false;
            StartCoroutine(waitTime());
        }
    }
    //Bullet code
    [Command]
    private void CmdSpawnBullet()
    {
        GameObject b = Instantiate(bullet, firePoint.position, firePoint.rotation);

        NetworkServer.Spawn(b);
    }
    IEnumerator waitShoot(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
    //Trap code
    private void CmdSpawnTrap()
    {
        trap.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        trap.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
        trap.GetComponent<Rigidbody2D>().velocity = firePoint.transform.right * trapSpeed;
    }
    IEnumerator waitTrap (float time)
    {
        yield return new WaitForSeconds(time);
        canTrap = true;
    }
    //Rapidfire code
    IEnumerator rapidFire ()
    {
        for (int i = 0; i < rapidShots; i++)
        {
            yield return new WaitForSeconds(fireRate);
            CmdspawnRapid();
        }
    }
    [Command]
    private void CmdspawnRapid ()
    {
        GameObject b = Instantiate(rapidfireBullet, firePoint.position, firePoint.rotation);

        NetworkServer.Spawn(b);
    }
    IEnumerator rapidWait ()
    {
        yield return new WaitForSeconds(rapidCooldown);
        canRapid = true;
    }
    //Retrun code
    IEnumerator returnPoint(Vector3 position)
    {
        yield return new WaitForSeconds(returnTime);
        player.transform.position = position;
        StartCoroutine(returnWait());
    }
    IEnumerator returnWait()
    {
        yield return new WaitForSeconds(returnCooldown);
        canReturn = true;
    }
    //Time code
    IEnumerator timeReset()
    {
        yield return new WaitForSecondsRealtime(timeDuration);
        CmdtimeDistort(1f);
    }
    [Command]
    private void CmdtimeDistort(float ammount)
    {
        RpcTimeDistort(ammount);
    }
    [ClientRpc]
    private void RpcTimeDistort (float ammount)
    {
        Time.timeScale = ammount;
        if (ammount != 1f)
        {
            StartCoroutine(timeReset());
        }
    }
    IEnumerator waitTime ()
    {
        yield return new WaitForSeconds(timeCooldown);
        canTime = true;
    }

}
