using UnityEngine;
using TMPro; // Important pour utiliser le texte

public class ChronoGame : MonoBehaviour
{
    public TextMeshProUGUI affichage; // On glissera notre texte ici plus tard
    private float chrono = 0f;
    private bool tourne = false;
    private bool fini = false;

    void Update()
    {
        if (tourne)
        {
            chrono += Time.deltaTime; // Le temps s'écoule
            // On affiche le temps au début, puis des ??? après 3 secondes
            affichage.text = chrono < 3f ? chrono.ToString("F2") : "???";
        }
    }

    // Cette fonction sera appelée par le bouton
    public void ActionBouton()
    {
        if (!tourne && !fini)
        {
            tourne = true; // Démarrer
        }
        else if (tourne)
        {
            ArreterJeu(); // Stop
        }
        else
        {
            Relancer(); // Recommencer
        }
    }

    void ArreterJeu()
    {
        tourne = false;
        fini = true;
        float score = Mathf.Abs(10f - chrono);
        affichage.text = "Temps : " + chrono.ToString("F2") + "s\nEcart : " + score.ToString("F3");
    }

    void Relancer()
    {
        chrono = 0f;
        fini = false;
        affichage.text = "Pret ?";
    }
}