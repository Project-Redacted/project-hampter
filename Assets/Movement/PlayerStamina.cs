using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    private PlayerMovement pm;
    public float stamina;
    float maxStamina;

    public Slider staminaBar;
    public float dValue;

    bool decreased = false;
    void Start()
    {
        pm = GetComponent<PlayerMovement>();

        maxStamina = stamina;
        staminaBar.maxValue = maxStamina;
    }


    void Update()
    {
       if (stamina != maxStamina && !decreased)
            IncreaseStamina();
        decreased = false;
        staminaBar.value = stamina;
    }
   
    public void DecreaseStamina()
    {
        if (stamina != 0)
            stamina -= dValue * Time.deltaTime;

        if(stamina <= 0)
        {
            pm.cansprint = false;
        }
        decreased = true;
    }
    public void IncreaseStamina()
    {
        stamina += dValue * Time.deltaTime;
        if(stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        if(stamina > maxStamina / 2)
        {
            pm.cansprint = true;
        }
    }

    public bool DecreaseByChunk(float amount)
    {
        if(stamina > amount)
        {
            stamina -= amount;
            return true;
        }
        return false;
    }
}