using UnityEngine;
using System.Collections;

public class Poderzinho : MonoBehaviour
{
    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;
    public float spawnOffset = 1f;
    public float shootLockTime = 5f; 

    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ShootAndLockMovement());
        }
    }

    private IEnumerator ShootAndLockMovement()
    {
        if (playerMovement != null)
            playerMovement.canMove = false;

        ShootProjectile();

        yield return new WaitForSeconds(shootLockTime);

        if (playerMovement != null)
            playerMovement.canMove = true;
    }

    private void ShootProjectile()
    {
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = -Camera.main.transform.position.z;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(mouseScreen);

        Vector2 myPos = transform.position;
        Vector2 direction = (mousePos - myPos).normalized;

        Vector2 spawnPos = myPos + direction * spawnOffset;
        GameObject poder = Instantiate(projectile, spawnPos, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        poder.transform.rotation = Quaternion.Euler(0, 0, angle);

        Rigidbody2D rb = poder.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = direction * projectileForce;

        var danoComp = poder.GetComponent<Dano>();
        if (danoComp != null)
            danoComp.dano = Random.Range(minDamage, maxDamage);
    }
}
