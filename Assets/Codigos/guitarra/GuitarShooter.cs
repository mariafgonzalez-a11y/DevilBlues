using UnityEngine;

public class GuitarShooter : MonoBehaviour
{
    public Transform firePoint;
    public float speed    = 15f;
    public float lifetime = 3f;
    public float damage   = 25f;   // Dano por nota

    private (string name, Color color)[] notes = new[]
    {
        ("Dó",  new Color(1f,   0.2f, 0.2f)),
        ("Ré",  new Color(1f,   0.6f, 0f  )),
        ("Mi",  new Color(1f,   1f,   0f  )),
        ("Fá",  new Color(0.2f, 1f,   0.2f)),
        ("Sol", new Color(0.2f, 0.6f, 1f  )),
        ("Lá",  new Color(0.6f, 0.2f, 1f  )),
        ("Si",  new Color(1f,   0.4f, 0.8f)),
    };

    private Collider playerCollider;

    void Start()
    {
        playerCollider = GetComponentInParent<Collider>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int index = Random.Range(0, notes.Length);
            Shoot(notes[index].name, notes[index].color);
        }
    }

    void Shoot(string noteName, Color color)
    {
        GameObject proj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        proj.transform.position   = firePoint ? firePoint.position : transform.position;
        proj.transform.localScale = Vector3.one * 0.3f;

        proj.GetComponent<Renderer>().material.color = color;

        Collider projCollider = proj.GetComponent<Collider>();
        if (playerCollider != null)
            Physics.IgnoreCollision(projCollider, playerCollider);

        Rigidbody rb  = proj.AddComponent<Rigidbody>();
        rb.useGravity = false;

        Vector3 dir = Camera.main.transform.forward;
        rb.linearVelocity = dir * speed;

        // Adiciona o script de dano no projétil
        NoteProjectile note = proj.AddComponent<NoteProjectile>();
        note.damage = damage;

        proj.name = noteName;
        Destroy(proj, lifetime);

        Debug.Log("Nota disparada: " + noteName);
    }
}
