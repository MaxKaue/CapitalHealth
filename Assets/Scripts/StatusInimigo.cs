using UnityEngine;
using UnityEngine.UI;

public class StatusInimigo : MonoBehaviour
{
    public float vida;
    public float vidaMax;

    public GameObject BarraDeVida;
    public Slider BarraDeVidaSlider;

    void Start()
    {
        vida = vidaMax;
    }

    public void CausaDano(float damage)
    {
        BarraDeVida.SetActive(true);
        vida -= damage;
        ChecarMorte();
        BarraDeVidaSlider.value = CalcularPorcentagemDeVida();
    }

    public void Curapersonagem (float cura)
    {
        vida -= cura;
        CheckOverHeal();
        BarraDeVidaSlider.value = CalcularPorcentagemDeVida();
    }

    private void CheckOverHeal()
    {
        if (vida > vidaMax) 
        {
            vida = vidaMax;
        }
    }

    private void ChecarMorte()
    {
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    private float CalcularPorcentagemDeVida()
    {
        return (vida / vidaMax);
    }

}
