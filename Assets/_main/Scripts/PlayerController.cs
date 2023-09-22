using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject TornadoMeshAnim;
    public SkinnedMeshRenderer CrashMesh;
    private Rigidbody rb;
    private Animator anim;
    private bool isJumping = false;
    private bool isRunning = false;
    private bool isAttacking = false;
    

    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpDuration = 0.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public void StartJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        isJumping = true;
        StartCoroutine(StopJump(jumpDuration));
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (isRunning)
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isWalk", false);
            }
            else
            {
                anim.SetBool("isWalk", true);
                anim.SetBool("isRunning", false);
            }
        }
        else
        {
            anim.SetBool("isWalk", false);
            anim.SetBool("isRunning", false);
        }

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

        // Detectar el clic izquierdo del mouse para iniciar el ataque
        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking)
            {
                // Iniciar el ataque tipo remolino
                anim.SetTrigger("Attack");

                CrashMesh.enabled = false;

                // Agregar lógica adicional de ataque aquí
                TornadoMeshAnim.SetActive(true);

                // Establecer que el jugador está atacando
                isAttacking = true;
                
                // Después de 2 segundos, desactivar el ataque y la animación de ataque
                StartCoroutine(StopAttack(0.8f));
            }
        }
    }

    IEnumerator StopJump(float duration)
    {
        yield return new WaitForSeconds(duration);

        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        isJumping = false;

        anim.SetBool("IsFalling", true);
    }

    IEnumerator StopAttack(float duration)
    {
        yield return new WaitForSeconds(duration);

        isAttacking = false;

        // Desactivar la animación de ataque y el objeto de VFX
        anim.SetTrigger("StopAttack");
        CrashMesh.enabled = true;
        TornadoMeshAnim.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("IsFalling", false);
            isJumping = false;
        }
    }
}
