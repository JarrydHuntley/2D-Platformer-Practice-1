using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedSense : MonoBehaviour
{
    List<Collider2D> contacts;

    void Awake()
    {
        contacts = new List<Collider2D>();
    }

    void OnEnable()
    {
        contacts.Clear();
    }

	void OnCollisionEnter2D(Collision2D collision)
    {
        contacts.Add(collision.collider);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        contacts.Remove(collision.collider);
    }

    public bool IsGrounded
    {
        get
        {
            return contacts.Count > 0;
        }
    }
}
