using UnityEngine;

public class StatusJogador : MonoBehaviour
{
    public static StatusJogador statusJogador;
   
    public GameObject player;

    public float vida;
    public float vidaMax;


    private void Awake()
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

    private void Start()
    {
        vida = vidaMax;
    }

    public void CausaDano(float dano)
    {
        vida -= dano;
        Checarmorte();
    }

    public void Curapersonagem(float cura)
    {
        vida -= cura;
        CheckOverHeal();
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
            Destroy(player);
        }
    }
}
