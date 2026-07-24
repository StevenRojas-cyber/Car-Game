using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hamster : MotorPart, IPointerClickHandler
{
    [Header("Hamster Properties")]
    [SerializeField] private int HamsterSleepChance = 50;
    [SerializeField] private float SleepTimeRemainingMultiplier = 1f;
    [SerializeField] private float HamsterSleepTime = 5f;
    [SerializeField] private SpriteRenderer HamsterSprite;
    [SerializeField] private TMP_Text SleepTimeText;

    bool IsHamsterSleeped = true;
    
    void Start()
    {
        PartName = "Hamster";
    }

    
    void Update()
    {

        if (IsHamsterSleeped) 
        { 
            HamsterSprite.color = Color.red;
            currentState = PartsStates.Damaged;
        }
        else
        {
            SleppTimeRemaining();
            HamsterSprite.color = Color.green;
            currentState = PartsStates.Repaired;
        }

    }

    
    void SleppTimeRemaining()
    {
        if (IsHamsterSleeped) return;

        HamsterSleepTime -= SleepTimeRemainingMultiplier * Time.deltaTime;

        SleepTimeText.text = HamsterSleepTime.ToString() + " 's";

        if (HamsterSleepTime <= 0f)
        {
            IsHamsterSleeped = true;
        }

    }


    float SetTimeToSleep()
    {
        float sleepTime = Random.Range(50f, 100f);
        
        return sleepTime;
    }


    

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Hamster clicked");

        if(!IsHamsterSleeped) return;

        IsHamsterSleeped = false;

        HamsterSleepTime = SetTimeToSleep();
    }
}
