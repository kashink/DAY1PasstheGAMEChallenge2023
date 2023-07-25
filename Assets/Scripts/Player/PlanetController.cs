using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [Space()]
    [Header("Config")]
    [SerializeField] ShieldRotationController shieldRotationController;

    private void OnMouseDown()
    {
        if (GameManager.Instance.gameState != GameState.GameRunning) return;

        this.shieldRotationController.ChangeDirection();
    }
}
