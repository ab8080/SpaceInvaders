using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    public Bullet laserPrefab;

    public float speed = 5f;

    private bool _laserActive;

    // private GameManager gameManager;

    private void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }



    private void Shoot()
    {
        if (!_laserActive) {
            Bullet bullet = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            bullet.destroyed += LaserDestroyed;
            _laserActive = true;
        }
    }

    private void LaserDestroyed() {
        _laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            GameManager.lives--;
            if (GameManager.lives <= 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        
    }

}