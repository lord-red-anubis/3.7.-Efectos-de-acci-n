using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class john : MonoBehaviour
{
    public GameObject BalasPrefab;
    public float jumpforce;
    public float speed;
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float horizontal;
    private bool grounded;
    private float LastShoot;
    private int Health = 5;
    

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Rigidbody2D.gravityScale = 0; // Desactivar la gravedad inicialmente
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("horizontal");

        if (horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running", horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            grounded = true;
        }
        else grounded = false;

        if (Input.GetKeyDown(KeyCode.W))
        {
            jump();
        }
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void jump()
    {
        Rigidbody2D.AddForce(Vector2.up * jumpforce);
        Rigidbody2D.gravityScale = 1; // Restaurar la gravedad al saltar
        Rigidbody2D.AddForce(Vector2.up * jumpforce);
    }
    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject Balas = Instantiate(BalasPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        Balas.GetComponent<balas>().SetDirection(direction); // Corregir aquí
    }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(horizontal * speed, Rigidbody2D.velocity.y); // Ajuste de velocidad aquí
        grounded = IsGrounded();
        Rigidbody2D.velocity = new Vector2(horizontal * speed, grounded ? 0 : Rigidbody2D.velocity.y);
    }
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null && hit.collider.CompareTag("Ground"); // Asegúrate de que "Ground" sea la etiqueta del Tilemap
    }
    public void Hit()
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);
    }
}
