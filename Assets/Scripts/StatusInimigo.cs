using UnityEngine;
using UnityEngine.UI;

public class StatusInimigo : MonoBehaviour
{
    public float vida;
    public float vidaMax;

    public GameObject BarraDeVida;
    public Slider BarraDeVidaSlider;

    [Header("Drop de Item")]
    public GameObject itemCuraPrefab; // arraste o prefab do item de cura aqui
    [Range(0f, 1f)] public float chanceDrop = 0.5f; // chance de drop (50%)

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

    private void ChecarMorte()
    {
        if (vida <= 0)
        {
            // Tentativa de drop
            if (Random.value <= chanceDrop && itemCuraPrefab != null)
            {
                Instantiate(itemCuraPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    private float CalcularPorcentagemDeVida()
    {
        return (vida / vidaMax);
    }
}
