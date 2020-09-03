using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public Rigidbody2D rb;
    public List<CelestialBody> bodies;

    public float mass;
    [SerializeField] public float radius;
    public Vector2 acceleration;
    [SerializeField] public Vector2 velocity;

    public Vector2 momentum;

    private void OnValidate()
    {
        Vector3 scaleChange = new Vector3(radius * 2, radius * 2, 0.0f);
        this.transform.localScale = scaleChange;

        mass = Mathf.PI * radius * radius;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bodies = new List<CelestialBody>(FindObjectsOfType<CelestialBody>());
    }

    void FixedUpdate()
    {
        foreach(CelestialBody body in bodies)
        {
            if(body == null)
            {
                bodies.Remove(body);
            }

            if(body != this)
            {
                float sqrDist = (body.rb.position - this.rb.position).sqrMagnitude;
                Vector2 forceDir = (body.rb.position - this.rb.position).normalized;

                acceleration = forceDir * (Universe.GRAV_CONST * body.mass) / sqrDist;

                velocity += acceleration * Universe.timeScale;

                momentum = mass * velocity;

                this.rb.MovePosition(this.rb.position + velocity);
            }
            else if(body == this)
            {
                this.rb.MovePosition(this.rb.position + velocity);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CelestialBody bodyCollider = collision.gameObject.GetComponent<CelestialBody>();

        //when a collision happens, destory smallest collider and transfer physics
        if(this.mass >= bodyCollider.mass)
        {
            this.mass += bodyCollider.mass;
            this.momentum += bodyCollider.momentum;
            this.velocity = this.momentum / this.mass;

            Destroy(collision.gameObject);
        }
        //else
        //{
        //    Destroy(this);
       // }
    }
}
