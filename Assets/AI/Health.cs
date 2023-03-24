using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    public int CurrentHealth;
    public int MaxHealth;
    public bool Immune = false;
    public float ImmuneTime = 2f;

    private void Start()
    {
        CurrentHealth = MaxHealth;


    }

    public void PlayerHit()
    {

        if(Immune)
        {
            return;
        }

        CurrentHealth--;
        if(CurrentHealth <= 0)
        {
            Die();
        }
        Immune = true;
        Invoke("MakeNotImmune", ImmuneTime);
    }

    public void MakeNotImmune()
    {
        Immune = false;
    }

    public void Die()
    {
        SceneManager.LoadScene("TempMainMenu"); // load the next scene
        Cursor.lockState = CursorLockMode.None;
    }

}
