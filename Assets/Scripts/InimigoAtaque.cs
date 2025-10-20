using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InimigoMovimento))]
public class InimigoAtaque : MonoBehaviour
{
    [Header("Ataque")]
    public float alcanceAtaque = 5f;
    public float tempoRecargaAtaque = 2f;
    public float tempoTravamentoAtaque = 0.2f;
    public GameObject ataquePrefab;
    public float forcaAtaque = 5f;
    public float danoMinimo = 5f;
    public float danoMaximo = 10f;
    public float distanciaSpawn = 0.3f;

    private Transform jogador;
    private InimigoMovimento movimento;
    private bool podeAtacar = true;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Player").transform;
        movimento = GetComponent<InimigoMovimento>();
    }

    private void Update()
    {
        if (jogador == null) return;

        float distancia = Vector2.Distance(transform.position, jogador.position);

        if (podeAtacar && distancia <= alcanceAtaque)
            StartCoroutine(Atacar());
    }

    private IEnumerator Atacar()
    {
        podeAtacar = false;
        movimento.podeMover = false;

        Vector2 direcao = (jogador.position - transform.position).normalized;
        movimento.AtualizarAnimacao(direcao);

        yield return new WaitForSeconds(tempoTravamentoAtaque);
        DispararAtaque(direcao);

        movimento.podeMover = true;
        yield return new WaitForSeconds(tempoRecargaAtaque);
        podeAtacar = true;
    }

    private void DispararAtaque(Vector2 direcao)
    {
        Vector2 posicaoSpawn = (Vector2)transform.position + direcao * distanciaSpawn;
        GameObject ataque = Instantiate(ataquePrefab, posicaoSpawn, Quaternion.identity);

        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        ataque.transform.rotation = Quaternion.Euler(0, 0, angulo);

        Rigidbody2D rb = ataque.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = direcao * forcaAtaque; // use velocity (n�o linearVelocity)

        // Em vez de armazenar o dano no proj�til, "injetamos" a fonte (this)
        var danoComp = ataque.GetComponent<DanoInimigo>();
        if (danoComp != null)
        {
            // Passa a refer�ncia do inimigo para que o proj�til consulte o dano real
            danoComp.SetFonteDeAtaque(this);
        }
    }

    // M�todo p�blico que retorna um dano para este inimigo (pode ser din�mico)
    public float ObterDano()
    {
        // Aqui voc� pode calcular dano com base em buffs, dist�ncia, estado, etc.
        return Random.Range(danoMinimo, danoMaximo);
    }
}
