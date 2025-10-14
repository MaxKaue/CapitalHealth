using System.Collections;
using UnityEngine;

public class PoderzinhoInimigo : MonoBehaviour
{
    public GameObject projectile;
    public Transform player;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;
    public float cooldown;


    void Start()
    {
        StartCoroutine(AtirarNoJogador());
    }

    IEnumerator AtirarNoJogador()
    {
        yield return new WaitForSeconds(cooldown);
        if (player != null) 
        {
            GameObject poder = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 myPos = transform.position;
            Vector2 TargetPosition = player.position;
            Vector2 direction = (TargetPosition - myPos).normalized;
            poder.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileForce;
            poder.GetComponent<DanoInimigo>().damage = Random.Range(minDamage, maxDamage);
            StartCoroutine(AtirarNoJogador());
        }
        
    }
}
