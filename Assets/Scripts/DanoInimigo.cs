using UnityEngine;

public class DanoInimigo : MonoBehaviour
{
    // Referência à fonte do ataque (o script do inimigo que criou este projétil)
    private InimigoAtaque fonteDeAtaque;

    // Setter para receber a referência ao inimigo no momento da criação
    public void SetFonteDeAtaque(InimigoAtaque inimigo)
    {
        fonteDeAtaque = inimigo;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignora colisão com inimigos
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
                // Fallback: se por algum motivo a referência não foi passada,
                // usa um valor padrão (escolha o que for adequado).
                danoAplicar = 5f;
                Debug.LogWarning("DanoInimigo: fonteDeAtaque não definida, usando fallback de dano.");
            }

            StatusJogador.statusJogador.CausaDano(danoAplicar);
        }

        // Destroi o projétil após colidir com qualquer coisa que não seja inimigo
        Destroy(gameObject);
    }
}
