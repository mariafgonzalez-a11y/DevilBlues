using UnityEngine;

public class Inimigo : MonoBehaviour
{

private float velocidade = 1.0f;
private Rigidbody inimigoRb;
private GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inimigoRb = GetComponent<Rigidbody>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        inimigoRb.AddForce((Player.transform.position - transform.position).normalized * velocidade);
    }
}
