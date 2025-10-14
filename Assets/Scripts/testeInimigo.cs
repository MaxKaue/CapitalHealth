using UnityEngine;

public class testeInimigo : MonoBehaviour
{

    public float damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy")
        {
            if (collision.tag == "Player")
            {
                StatusJogador.statusJogador.CausaDano(damage);
            }
            Destroy(gameObject);
        }
    }
}
