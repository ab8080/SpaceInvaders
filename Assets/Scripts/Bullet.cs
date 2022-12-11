using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Vector3 direction = Vector3.up;
    public System.Action destroyed;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (this.destroyed != null) {
            this.destroyed.Invoke();
        }
        Destroy(this.gameObject);
    }
}
