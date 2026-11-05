using System;
using UnityEngine;

[RequireComponent(typeof(DetecteurBoule))]
public class Surface : MonoBehaviour
{
    /// <summary>
    /// Référence au détecteur de boule attaché à cette surface.
    /// </summary>
    public DetecteurBoule Detecteur { get; private set; }

    [SerializeField, Tooltip("Positions de génération des objets sur la surface.")]
    private Vector3[] positionGeneration;

    [field: SerializeField, Tooltip("Animator de la platforme")]
    public Animator PlatformeAnimator { get; private set; }
    private void Awake()
    {
        Detecteur = GetComponent<DetecteurBoule>();
        if (ControleurJeu.Instance.EnPause == true)
        {
            ChangerEtatAnimation();
        }
    }

    /// <summary>
    /// Ajoute un objet à la surface à une position aléatoire parmi les positions de génération définies.
    /// </summary>
    /// <param name="prefab">Le prefab de l'objet à ajouter à la surface.</param>
    public void AjouterObjet(GameObject prefab)
    {
        if (positionGeneration.Length == 0)
        {
            Debug.LogWarning("Aucune position de génération définie pour la surface.");
            return;
        }

        // Sélectionne une position aléatoire parmi les positions disponibles
        Vector3 positionChoisie = positionGeneration[UnityEngine.Random.Range(0, positionGeneration.Length)];
        // Instancie l'prefab à la position choisie
        GameObject objetGenere = Instantiate(prefab, transform);
        objetGenere.transform.localPosition = positionChoisie; // Utilise localPosition pour positionner l'objet par rapport à la surface
    }

    private void OnDrawGizmos()
    {
        foreach (Vector3 position in positionGeneration)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + position, 0.2f);
        }
    }
    public void ChangerEtatAnimation()
    {
        bool animation = PlatformeAnimator.GetBool("Pause");
        PlatformeAnimator.SetBool("Pause", !animation);
    }
    private void Start()
    {
        ControleurJeu.Instance.AjouterEcoute(ChangerEtatAnimation);
    }
    private void OnDestroy()
    {
        ControleurJeu.Instance.EnleverEcoute(ChangerEtatAnimation);
    }
}
