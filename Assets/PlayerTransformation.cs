using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
    private bool isVegState = true;  // Track the current state
    private Animator animator;       // Reference to Animator component

    // References to UbhShotCtrl components
    private UbhShotCtrl normalShotCtrl;
    private UbhShotCtrl transformedShotCtrl;
        public ParticleSystem transformEffect;


    void Start()
    {
        // Cache the Animator component for better performance
        animator = GetComponent<Animator>();

        // Find all UbhShotCtrl components in children
        UbhShotCtrl[] shotCtrls = GetComponentsInChildren<UbhShotCtrl>();

        if (shotCtrls.Length < 2)
        {
            Debug.LogError("Less than 2 UbhShotCtrl components found! Make sure there are two for normal and transformed shots.");
            return;
        }

        // Assign the first found UbhShotCtrl to normalShotCtrl and the second to transformedShotCtrl
        normalShotCtrl = shotCtrls[0];
        transformedShotCtrl = shotCtrls[1];

        Debug.Log("Normal Shot Ctrl: " + (normalShotCtrl != null));
        Debug.Log("Transformed Shot Ctrl: " + (transformedShotCtrl != null));

        // Start with normal shot enabled and transformed shot disabled
        SwitchToNormalShot();
    }

  void Update()
{
    // Check if the "M" key is pressed once
    if (Input.GetKeyDown(KeyCode.M) || Input.GetMouseButtonDown(1))
    {
        GetComponent<AudioSource>().Play();
        transformEffect.Play();
        
        // Check the current state and switch to the other state
        if (isVegState)
        {
            animator.SetTrigger("normalToVegg");  // Trigger to switch to "veggie" state
            isVegState = false;  // Update state
            Debug.Log("Switching to transformed shot");
            SwitchToTransformedShot();
        }
        else
        {
            animator.SetTrigger("VegToNormal");  // Trigger to switch to "normal" state
            isVegState = true;  // Update state
            Debug.Log("Switching to normal shot");
            SwitchToNormalShot();
        }
    }
}

void SwitchToNormalShot()
{
    // Stop transformed shot and start normal shot
    if (transformedShotCtrl != null)
    {
        Debug.Log("Stopping Transformed Shot Routine");
        transformedShotCtrl.StopShotRoutine();
    }

    if (normalShotCtrl != null)
    {
        Debug.Log("Starting Normal Shot Routine");
        normalShotCtrl.StartShotRoutine();
    }
}

void SwitchToTransformedShot()
{
    // Stop normal shot and start transformed shot
    if (normalShotCtrl != null)
    {
        Debug.Log("Stopping Normal Shot Routine");
        normalShotCtrl.StopShotRoutine();
    }

    if (transformedShotCtrl != null)
    {
        Debug.Log("Starting Transformed Shot Routine");
        transformedShotCtrl.StartShotRoutine();
    }
}
public void ResetTransformation()
{
    isVegState = true;  // Reset to the default state
    SwitchToNormalShot();  // Start with normal shot
}


}
