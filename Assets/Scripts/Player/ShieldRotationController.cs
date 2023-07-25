using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotationController : MonoBehaviour
{
    float directionMultiplier = 1f;

    void Update()
    {
        if (GameManager.Instance.gameState != GameState.GameRunning) return;

        float rotationLevelBonus = 1 + ((PlayerStatsManager.Instance.shieldSpeedLevel - 1) * 0.5f);

        this.transform.Rotate(Vector3.forward * PlayerStatsManager.Instance.shieldRotationSpeed * rotationLevelBonus * Time.deltaTime * this.directionMultiplier);
    }

    public void ChangeDirection()
    {
        this.directionMultiplier *= -1;
    }
}
