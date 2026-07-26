using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerComoponent : MonoBehaviour
{

    [Header("Player Atributes")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Motor playerMotor;
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private float ScoreMultiplier = 1;
    


    [Header("Player Input Actions")]
    [SerializeField] private InputActionReference moveAction;


    private float Score = 0;
    private float moveDirection;

    void Start()
    {
           moveAction.action.Enable();
    }

    
    void Update()
    {
        if (moveAction == null) return;

        moveDirection = moveAction.action.ReadValue<float>();

        Vector3 movement = new Vector3(moveDirection * moveSpeed * Time.deltaTime, 0, 0);

        transform.Translate(movement);

        gainScore(ScoreMultiplier);

        DamagedPartsEffects();

    }
    
    void GameOver()
    {
        Debug.Log("Tu auto se daño, Fin del juego");
    }

    void gainScore(float scoreMultiplier)
    {
        Score = Score + (scoreMultiplier * Time.deltaTime);

        ScoreText.text = "Meters: " + Mathf.FloorToInt(Score) + " m";
    }

    void DamagedPartsEffects()
    {
        if (playerMotor == null) return;

        switch (playerMotor.getPartsInGodState())
        {
            case 0:
                GameOver();
                
                break;

            case 1:

                ScoreMultiplier = 0.25f;

                break;

            case 2:

                moveSpeed = 5.5f;

                break;

            case 4:
                ScoreMultiplier = 1.5f;
                moveSpeed = 15;
                break;
            
            default:
                ScoreMultiplier = 1;
                moveSpeed = 10;
                break;
        }

    }



}
