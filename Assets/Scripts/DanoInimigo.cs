using UnityEngine;

public class DanoInimigo : MonoBehaviour
{
    // Refer�ncia � fonte do ataque (o script do inimigo que criou este proj�til)
    private InimigoAtaque fonteDeAtaque;

    // Setter para receber a refer�ncia ao inimigo no momento da cria��o
    public void SetFonteDeAtaque(InimigoAtaque inimigo)
    {
        fonteDeAtaque = inimigo;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignora colis�o com inimigos
        if (collision.CompareTag("Enemy")) return;

        // Se for player, aplica o dano vindo da fonte (inimigo)
        if (collision.CompareTag("Player"))
        {
            float danoAplicar = 0f;

            if (fonteDeAtaque != null)
            {
                danoAplicar = fonteDeAtaque.ObterDano();
            }
            else
            {
                // Fallback: se por algum motivo a refer�ncia n�o foi passada,
                // usa um valor padr�o (escolha o que for adequado).
                danoAplicar = 5f;
                Debug.LogWarning("DanoInimigo: fonteDeAtaque n�o definida, usando fallback de dano.");
            }

            StatusJogador.statusJogador.CausaDano(danoAplicar);
        }

        // Destroi o proj�til ap�s colidir com qualquer coisa que n�o seja inimigo
        Destroy(gameObject);
    }
}
