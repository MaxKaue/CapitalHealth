using UnityEngine;

public class Dano : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player")
        {
            if(collision.GetComponent<StatusInimigo>()  != null)
            {
                collision.GetComponent<StatusInimigo>().CausaDano(damage);
            }
            Destroy(gameObject);
        }
    }
}
