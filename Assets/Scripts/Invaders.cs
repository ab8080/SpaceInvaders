using UnityEngine;
using UnityEngine.SceneManagement;

public class Invaders : MonoBehaviour {
    public Invader[] prefabs;
    public int rows = 5;
    public int columns = 11;
    private float spacing = 2.0f;

    public float speed = 0.01f;

    public int amountKilled = 0;
    public int totalInvaders => rows * columns;
    public float percentKilled => (float)amountKilled / (float)totalInvaders;
    Vector3 direction = Vector2.right;

    public float missileAttackRate = 1f;
    public Bullet missilePrefab;

    private void Start() {
        InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
    }

    private void Awake() {
        for (int row = 0; row < this.rows; row++) {
            float height = spacing * (this.rows - 1);
            float width = spacing * (this.columns - 1);
            Vector2 centering = new Vector2(-width/2, -height/2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * spacing), 0f);

            for (int col = 0; col < this.columns; col++) {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled; // функция InvaderKilled будет вызвана, когда монстра убьют 
                Vector3 position = rowPosition;
                position.x += col * spacing;
                invader.transform.localPosition = position;
            }
        }
    }
    

    private void Update() {
        this.transform.position += direction * this.speed;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in this.transform) {
            if (!invader.gameObject.activeInHierarchy) {
                continue;
            }
            if (direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f)) {
                AdvanceRow();
            } else if (direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f)) {
                AdvanceRow();
            }
        }
    
    }

    private void AdvanceRow() {
        direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    private void InvaderKilled() {
        amountKilled++;
        if (amountKilled >= totalInvaders) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void MissileAttack() {
        foreach (Transform invader in this.transform) {
            if (!invader.gameObject.activeInHierarchy) {
                continue;
            }
            if (Random.value < 0.1f) {
                Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    
}
