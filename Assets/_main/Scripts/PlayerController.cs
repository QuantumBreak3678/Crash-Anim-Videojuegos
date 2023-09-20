using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private bool isJumping = false;

    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpDuration = 0.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Método llamado desde un evento de animación
    public void StartJump()
    {

        // Aplicar fuerza hacia arriba para iniciar el salto
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reiniciar la velocidad vertical
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        isJumping = true;

        // Iniciar la corutina para controlar la duración del salto
        StartCoroutine(StopJump(jumpDuration));
    }

    private void Update()
    {
        
        if (!isJumping && rb.velocity.y == 0f && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    IEnumerator StopJump(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Detener el salto
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        isJumping = false;

        // Cambiar a la animación de caída
        anim.SetBool("IsFalling", true);
    }

    // Llamado cuando el personaje colisiona con el suelo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Cambiar a la animación de aterrizaje
            anim.SetBool("IsFalling", false);
            isJumping = false;
        }
    }
}