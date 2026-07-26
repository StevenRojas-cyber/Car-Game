using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Obtacle Attributes")]
    [SerializeField] private BoxCollider2D HitBox;
    [SerializeField] private GameObject BackGround;
    [SerializeField] private float moveMultiplier;


    private Scroller scrollVelocity;

    void Start()
    {
        if (BackGround == null) return;

        scrollVelocity = BackGround.GetComponent<Scroller>();

        if (scrollVelocity == null) return;

        moveMultiplier = scrollVelocity.GetScrollSpeed();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == null) return;

        if (!collision.CompareTag("Player")) return;

        Motor userMotor = collision.GetComponent<Motor>();

        int RandomDamage = Random.Range(1,4);

        userMotor.DamageAnyMotorPart(RandomDamage);


        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(0, (moveMultiplier * Time.deltaTime) * -1, 0);
        transform.Translate(movement);
    }
}
