using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Obstacle : MonoBehaviour
{
    [Header("Obtacle Attributes")]
    [SerializeField] private BoxCollider2D HitBox;
    [SerializeField] public float moveMultiplier;


    private Scroller scrollVelocity;

    void Start()
    {



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if (collision.CompareTag("Player"))
        {

            Motor userMotor = collision.GetComponent<Motor>();

            int RandomDamage = Random.Range(1, 4);

            userMotor.DamageAnyMotorPart(RandomDamage);

            Destroy(this.gameObject);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.CompareTag("DeathZone"))
        {
            Destroy(this.gameObject);

            Debug.Log("Destruido con exito");
        }
    }


    // Update is called once per frame
    void Update()
    {
        float velocidadFinal = moveMultiplier + SpeedManager.velocidadExtraObstaculos;
        Vector3 movement = new Vector3(0, (velocidadFinal * Time.deltaTime) * -1, 0);

        transform.Translate(movement);
    }
}
