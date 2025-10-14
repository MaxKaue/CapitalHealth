using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class Poderzinho : MonoBehaviour
{
    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject poder = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = transform.position;
            Vector2 direction = (mousePos - myPos).normalized;
            poder.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileForce;
            poder.GetComponent<Dano>().damage = Random.Range(minDamage, maxDamage);
        }
    }
}
