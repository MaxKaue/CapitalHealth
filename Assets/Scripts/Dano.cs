using UnityEngine;

public class Dano : MonoBehaviour
{
    public float dano;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            if(collision.GetComponent<StatusInimigo>()  != null)
            {
                collision.GetComponent<StatusInimigo>().CausaDano(dano);
            }
            Destroy(gameObject);
        }
    }
}
