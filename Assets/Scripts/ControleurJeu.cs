using System;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Contrôleur de jeu qui gère l'état global du jeu et fournit un accès centralisé aux contrôles et aux autres systèmes.
/// </summary>
public class ControleurJeu : MonoBehaviour
{
    /// <summary>
    /// Instance singleton du contrôleur de jeu.
    /// </summary>
    public static ControleurJeu Instance { get; private set; }

    [field: SerializeField, Tooltip("Référence aux contrôles du joueur.")]
    public PlayerInput Controles { get; private set; }

    public Action ChangerEtat;
    public bool EnPause { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            EnPause = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AjouterEcoute(Action ecoute)
    {
        ChangerEtat += ecoute;
    }
    public void EnleverEcoute(Action ecoute)
    {
        ChangerEtat -= ecoute;
    }
    public void ChangerEtatActuelle(bool enPause)
    {
        if (EnPause != enPause)
        {
            EnPause = enPause;
            ChangerEtat?.Invoke();
        }
    }
}
