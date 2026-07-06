using UnityEngine;

public class Coin : MonoBehaviour
{
    public float speedBonus = -0.2f;
    public float pushBonus = 2f;
    public float massBonus = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null)
            return;

        player.speed += speedBonus;

        if (player.speed < 2)
            player.speed = 2;

        player.pushForce += pushBonus;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.mass += massBonus;

        Destroy(gameObject);
    }
}