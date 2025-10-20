using UnityEngine;

public class ItemCura : MonoBehaviour
{
    public float valorCura = 20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StatusJogador.statusJogador.Curapersonagem(valorCura);

            Destroy(gameObject);
        }
    }
}
