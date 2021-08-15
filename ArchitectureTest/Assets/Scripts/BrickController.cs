using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticlesGO;
    [SerializeField] private GameObject graphicGO;

    private Collider2D _brickCollider;
    private Rigidbody2D _brickRigidBody;

    private void Start()
    {
        _brickCollider = GetComponent<Collider2D>();
        _brickRigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Explode();
        var game = GamePlay.Instance;
        game.Score++;
    }

    private void Explode()
    {
        _brickCollider.enabled = false;
        _brickRigidBody.isKinematic = true;
        
        explosionParticlesGO.SetActive(true);
        graphicGO.SetActive(false);
        
    }
}