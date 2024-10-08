using System.Collections.Generic;
using UnityEngine;

public class FightCourse : MonoBehaviour
{
    public UbhObjectPool pool;
    public HealthSystem healthSystem;
    public GameObject enemy;
    public bool transitionPhases;

    private bool phase2Triggered = false;  // Flag to track if the second phase has been triggered
    private bool phase3Triggered = false;  // Flag to track if the second phase has been triggered

    private bool victory = false;          // Flag to track if the enemy is defeated

    void Start()
    {
        pool = GameObject.Find("Pool").GetComponent<UbhObjectPool>();
        healthSystem = GetComponent<HealthSystem>();

        // Trigger the initial phase when the enemy is at full health
        TriggerInitialPhase();
    }

    void Update()
    {
        // Check if health is less than or equal to 200 and the second phase hasn't been triggered yet
        if (healthSystem.currentHealth <= healthSystem.maxHealth * 0.60 && !phase2Triggered)
        {
            phase2Triggered = true;  // Set the flag to true to prevent future triggers
            TriggerPhaseTransition(2);  // Call the method to handle the second phase transition
        }
                // Check if health is less than or equal to 200 and the second phase hasn't been triggered yet
        if (healthSystem.currentHealth <= healthSystem.maxHealth * 0.35 && !phase3Triggered)
        {
            phase3Triggered = true;  // Set the flag to true to prevent future triggers
            TriggerPhaseTransition(3);  // Call the method to handle the second phase transition
        }

        // Check if health is less than or equal to 0 and victory hasn't been declared yet
        if (healthSystem.currentHealth <= 0 && !victory)
        {
            victory = true;  // Set the flag to true to prevent future triggers
            TriggerPhaseTransition(4);  // Call the method to handle the victory
        }
    }

    // Method to handle phase transition logic
    private void TriggerPhaseTransition(int phaseNumber)
    {
        
        GetComponent<PlayerLimitations>().disablePlayerMovement();
        GetComponent<PlayerLimitations>().disablePlayerShooting();
        GetComponent<DialogSetUp>().dialogueOnCourse = true;
        pool.ReleaseAllBullet();
        GetComponent<DialogSetUp>().SwitchDialog();

    }

    private void TriggerInitialPhase()
    {
        // Custom logic for the initial phase
        GetComponent<PlayerLimitations>().disablePlayerMovement();
        GetComponent<PlayerLimitations>().disablePlayerShooting();
        //GetComponent<DialogSetUp>().enabled = true;
        GetComponent<DialogSetUp>().dialogueOnCourse = true;
        pool.ReleaseAllBullet();
        
        disableEnemyPhases();
    }

    void disableEnemyPhases()
    {
        pool.ReleaseAllBullet();
        enemy.GetComponent<UbhShotCtrl>().StopShotRoutineAndPlayingShot();
        enemy.GetComponent<EnemyPhasesController>().isPhase1Active = false;
        enemy.GetComponent<EnemyPhasesController>().isPhase2Active = false;
        enemy.GetComponent<EnemyPhasesController>().isPhase3Active = false;
    }
}
