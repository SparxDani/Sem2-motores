using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidad = 10;
    Rigidbody2D rb;
    SpriteRenderer sp;
    private Sprite shape;
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
            shape = collider.GetComponent<SpriteRenderer>().sprite;
            sp.sprite = shape;
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
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(velocidad * horizontalInput, velocidad * verticalInput);

        Vector2 raycastDirection = new Vector2(horizontalInput, verticalInput).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, raycastvelocity, layers);

        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("Nombre del objeto: " + hitObject.name );
            Debug.Log("Posición del objeto: " + hitObject.transform.position);
            Debug.Log("Tag del objeto: " + hitObject.tag);

            if (hitObject.tag == "Shape")
            {
                Sprite shapeSprite = hitObject.GetComponent<SpriteRenderer>().sprite;
                Debug.Log("Sprite: " + shapeSprite.name);
            }
            else if (hitObject.tag == "Color")
            {
                Color objectColor = hitObject.GetComponent<SpriteRenderer>().color;
                Debug.Log("Color: " + objectColor);
            }
        }
    }
}
