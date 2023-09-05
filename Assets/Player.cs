using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidad = 10;

    Rigidbody2D rb;
    SpriteRenderer sp;
    private Sprite forma;
    private Color color;
    [SerializeField] private float raycastvelocity = 2;
    [SerializeField] LayerMask layers;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Shape")
        {
            forma = collider.GetComponent<SpriteRenderer>().sprite;
            sp.sprite = forma;
        }
        if (collider.tag == "Color")
        {
            color = collider.GetComponent<SpriteRenderer>().color;
            sp.color = color;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.velocity = new Vector2(velocidad * Input.GetAxis("Horizontal"), rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * velocidad);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        }

        Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Debug.DrawRay(transform.position, inputAxis * raycastvelocity, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, inputAxis, out hit, Mathf.Infinity, layers))
        {
            Debug.DrawRay(transform.position, inputAxis * hit.distance, Color.yellow);
            Debug.Log("Did hit");
        }
        else
        {
            Debug.DrawRay(transform.position, inputAxis * 100, Color.white);
            Debug.Log("Done");
        }

    }
}
