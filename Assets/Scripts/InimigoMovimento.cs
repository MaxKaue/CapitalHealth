using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class InimigoMovimento : MonoBehaviour
{
    [Header("Movimentação")]
    public float velocidade = 3f;
    public float distanciaIdealMin = 5f;
    public float distanciaIdealMax = 10f;
    public float distanciaEntreInimigos = 1.5f;

    [HideInInspector] public bool podeMover = true;

    private Transform jogador;
    private Animator animador;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jogador = GameObject.FindWithTag("Player").transform;
        animador = GetComponent<Animator>();
    }

    private void Update()
    {
        if (jogador == null || !podeMover) return;
        // Apenas calcula direção no Update
        Vector2 direcaoParaJogador = (jogador.position - transform.position).normalized;
        float distancia = Vector2.Distance(transform.position, jogador.position);

        Vector2 forcaSeparacao = CalcularForcaSeparacao();
        Vector2 direcaoFinal = Vector2.zero;

        if (distancia > distanciaIdealMax)
            direcaoFinal = direcaoParaJogador;
        else if (distancia < distanciaIdealMin)
            direcaoFinal = -direcaoParaJogador;

        direcaoFinal += forcaSeparacao;
        if (direcaoFinal != Vector2.zero)
            direcaoFinal.Normalize();

        AtualizarAnimacao(direcaoFinal);
    }

    private void FixedUpdate()
    {
        if (jogador == null || !podeMover) return;

        Vector2 direcaoParaJogador = (jogador.position - transform.position).normalized;
        float distancia = Vector2.Distance(transform.position, jogador.position);

        Vector2 forcaSeparacao = CalcularForcaSeparacao();
        Vector2 direcaoFinal = Vector2.zero;

        if (distancia > distanciaIdealMax)
            direcaoFinal = direcaoParaJogador;
        else if (distancia < distanciaIdealMin)
            direcaoFinal = -direcaoParaJogador;

        direcaoFinal += forcaSeparacao;
        if (direcaoFinal != Vector2.zero)
            direcaoFinal.Normalize();

        rb.MovePosition(rb.position + direcaoFinal * velocidade * Time.fixedDeltaTime);
    }

    private Vector2 CalcularForcaSeparacao()
    {
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Enemy");
        Vector2 separacao = Vector2.zero;

        foreach (GameObject outro in inimigos)
        {
            if (outro == gameObject) continue;

            float distancia = Vector2.Distance(transform.position, outro.transform.position);
            if (distancia < distanciaEntreInimigos)
            {
                Vector2 direcaoOposta = (transform.position - outro.transform.position).normalized;
                separacao += direcaoOposta / distancia;
            }
        }

        return separacao;
    }

    public void AtualizarAnimacao(Vector2 direcao)
    {
        if (animador == null) return;

        if (direcao != Vector2.zero)
        {
            animador.SetLayerWeight(1, 1);
            animador.SetFloat("xDir", direcao.x);
            animador.SetFloat("yDir", direcao.y);
        }
        else
        {
            animador.SetLayerWeight(1, 0);
        }
    }
}