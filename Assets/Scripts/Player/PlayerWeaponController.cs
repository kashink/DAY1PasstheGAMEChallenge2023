using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerWeaponController : MonoBehaviour
{
    [Space()]
    [Header("Config")]
    [SerializeField] ProjectileController projectile;
    [SerializeField] float distanceToShoot = 4f;
    [SerializeField] float shootDelay = 2f;
    float currentShootDelay = 0f;
    bool inDelay = false;

    void Update()
    {
        if (GameManager.Instance.gameState != GameState.GameRunning) return;

        if (this.inDelay)
        {
            this.currentShootDelay += Time.deltaTime;
            if (this.currentShootDelay < (this.shootDelay - ((PlayerStatsManager.Instance.weaponCadenceLevel - 1) * 0.2f))) return;
            this.currentShootDelay = 0;
            this.inDelay = false;

            return;
        }

        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemyList)
        {
            float distance = Vector3.Distance(enemy.transform.position, this.transform.position);
            if (distance <= this.distanceToShoot)
            {
                Vector3 direction = enemy.transform.position - this.transform.position;
                RaycastHit2D[] hitsShort = Physics2D.RaycastAll(this.transform.position, direction, distance * 10);

                bool canShoot = true;
                foreach (RaycastHit2D hit in hitsShort)
                {
                    if (hit.collider.gameObject.tag == "Obstacle") canShoot = false;
                }

                if (canShoot)
                {
                    Vector3 projectilePos = this.gameObject.transform.position;
                    ProjectileController projectileInstance = Instantiate(this.projectile, projectilePos, Quaternion.FromToRotation(Vector3.up, enemy.transform.position - projectilePos));
                    projectileInstance.SetTarget((enemy.transform.position - projectilePos) * 1000);
                    this.inDelay = true;
                    return;
                }
            }
        }
    }
}
