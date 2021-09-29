using UnityEngine;
using System.Collections;

public class mageFireball : MonoBehaviour
{
    private int angle;

    void Start()
    {
        //25 e -63
        Random.seed = System.DateTime.Now.Millisecond;
        angle = Random.Range(-63, 26);
        GetComponent<Transform>().eulerAngles = new Vector3(0, 0, angle);
    }

    void FixedUpdate()
    {
        Vector3 force = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.left;
        Vector2 noMovement = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().AddForce(force * 350);
        GetComponent<Rigidbody2D>().velocity = noMovement;
    }

    void OnTriggerEnter2D(Collider2D colisor)
    {
        if (colisor.CompareTag("Player") || colisor.CompareTag("GroundCheck"))
        {
            CircleCollider2D[] colliders = gameObject.GetComponents<CircleCollider2D>();
            foreach (CircleCollider2D circleCollider in colliders)
            {
                circleCollider.enabled = false;
            }
            Destroy(this.gameObject, 0.3f);
        } else if (colisor.CompareTag("PlayerFireball"))
        {
            Destroy(this.gameObject);
        } else if (!colisor.CompareTag("Mage"))
        {
            Destroy(this.gameObject, 1);
        }
    }
}
