using System;
using UnityEngine;
using UnityEngine.UI;

public class StatusJogador : MonoBehaviour
{
    public static StatusJogador statusJogador;

    public GameObject player;
    public Text textovida;
    public Slider slidervida;
    public Text textoescudo;
    public Slider sliderescudo;

    public float vida;
    public float vidaMax;
    public float escudo;
    public float escudoMax;

    void Awake()
    {
        if (statusJogador != null)
        {
            Destroy(statusJogador);
        }
        else
        {
            statusJogador = this;
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        vida = vidaMax;
        escudo = escudoMax;
        AtualizarUI();
    }

    public void CausaDano(float dano)
    {
        Checarescudo(dano);
        Checarmorte();
        AtualizarUI();
    }

    public void Curapersonagem(float cura)
    {
        vida += cura; 
        CheckOverHeal();
        AtualizarUI();
    }

    private void CheckOverHeal()
    {
        if (vida > vidaMax)
        {
            vida = vidaMax;
        }
    }

    private void Checarmorte()
    {
        if (vida <= 0)
        {
            vida = 0;
            Destroy(player);
        }
    }

    private void Checarescudo(float dano)
    {
        if (escudo <= 0)
        {
            vida -= dano;
        }
        else
        {
            escudo -= dano;
            if (escudo < 0)
            {
                vida += escudo;
                escudo = 0;
            }
        }

        if (vida < 0) vida = 0; 
    }

    float CalcularVida()
    {
        return Mathf.Clamp01(vida / vidaMax);
    }

    float CalcularEscudo()
    {
        return Mathf.Clamp01(escudo / escudoMax);
    }

    private void AtualizarUI()
    {
        sliderescudo.value = CalcularEscudo();
        textoescudo.text = Mathf.Ceil(escudo).ToString() + " / " + Mathf.Ceil(escudoMax).ToString();
        slidervida.value = CalcularVida();
        textovida.text = Mathf.Ceil(vida).ToString() + " / " + Mathf.Ceil(vidaMax).ToString();
    }
}
