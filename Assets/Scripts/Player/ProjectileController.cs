using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ProjectileController : MonoBehaviour
{
    [Space()]
    [Header("Setup")]
    [SerializeField] public float movementSpeed = 1;

    Vector2 target;

    private void Update()
    {
        if (GameManager.Instance.gameState != GameState.GameRunning) return;

        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, this.target, this.movementSpeed * Time.deltaTime);
    }

    public void SetTarget(Vector2 _target)
    {
        this.target = _target;
    }

    public void HandleDestroy()
    {
        Destroy(this.gameObject);
    }
}
