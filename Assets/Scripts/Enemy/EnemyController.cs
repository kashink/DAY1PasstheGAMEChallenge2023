using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Space()]
    [Header("Stats")]
    [SerializeField] int life = 1;
    [SerializeField] int damage = 5;
    [SerializeField] int coins = 1;

    [Space()]
    [Header("Config")]
    [SerializeField] float speed = 2f;
    [SerializeField] bool facingLeft = true;

    public void Setup(int life, int damage, int coins)
    {
        this.life = life;
        this.damage = damage;
        this.coins = coins;
    }

    private void Update()
    {
        if (GameManager.Instance.gameState != GameState.GameRunning) return;

        MoveTo(PlayerStatsManager.Instance.planetTransform.position);
    }

    void MoveTo(Vector3 destination)
    {
        FaceDestination(destination);
        this.transform.position = Vector2.MoveTowards(this.transform.position, destination, this.speed * Time.deltaTime);
    }

    void FaceDestination(Vector3 destination)
    {
        Vector2 dir = destination - this.transform.position;
        dir.Normalize();

        if ((dir.x < 0 && this.facingLeft) || (dir.x > 0 && !this.facingLeft)) this.transform.localScale = new Vector3(1, 1, 1);
        if ((dir.x > 0 && this.facingLeft) || (dir.x < 0 && !this.facingLeft)) this.transform.localScale = new Vector3(-1, 1, 1);
    }

    void TakeDamage(float multiplier = 1)
    {
        this.life -= Mathf.CeilToInt(PlayerStatsManager.Instance.damage * multiplier);

        if (this.life <= 0) EnemyDied();
    }

    void EnemyDied()
    {
        PlayerStatsManager.Instance.ReceiveCoins(this.coins);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "PlayerDefense":
                TakeDamage();
                break;
            case "Projectile":
                collision.gameObject.GetComponent<ProjectileController>().HandleDestroy();
                TakeDamage(PlayerStatsManager.Instance.weaponDamageLevel);
                break;
            case "Player":
                PlayerStatsManager.Instance.TakeDamage(this.damage);
                Destroy(this.gameObject);
                break;
        }
    }
}
