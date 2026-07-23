using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComoponent : MonoBehaviour
{


    [Header("Player Atributes")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private float moveSpeed;

    private float moveDirection;

    void Start()
    {
           moveAction.action.Enable();
    }

    void Update()
    {
        if (moveAction == null) return;


        moveDirection = moveAction.action.ReadValue<float>();

        print(moveDirection);

        Vector3 movement = new Vector3(moveDirection * moveSpeed * Time.deltaTime, 0, 0);

        transform.Translate(movement);
    }
}
