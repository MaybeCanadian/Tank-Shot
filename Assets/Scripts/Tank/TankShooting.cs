using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankShooting : MonoBehaviour
{
    private TankStats stats = null;

    private bool attackInput = false;

    #region Init Functions
    private void Awake()
    {
        stats = GetComponent<TankStats>();
    }
    #endregion

    #region Update
    private void Update()
    {
        float delta = Time.deltaTime;

        if (attackInput)
        {
            CheckAttackCooldown(ref stats.shootingAttackTimer);
        }

        TickAttackTimer(delta, ref stats.shootingAttackTimer);
    }
    private void TickAttackTimer(float delta, ref float attackTimer)
    {
        float attackCooldown = stats.defualts.shootingStats.shotCooldown.value;

        if(attackTimer < attackCooldown)
        {
            attackTimer += delta;
        }
    }
    #endregion


    #region Attacking
    private void CheckAttackCooldown(ref float attackTimer)
    {
        float attackCooldown = stats.defualts.shootingStats.shotCooldown.value;

        if (attackTimer > attackCooldown)
        {
            attackTimer = 0.0f;

            FireShot();
        }
    }
    private void FireShot()
    {
        Debug.Log("Pew Pew");
    }
    private void SpawnProjectile()
    {

    }
    #endregion

    #region Input Events
    public void ShootInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            attackInput = true;
            return;
        }

        if(context.canceled)
        {
            attackInput = false;
            return;
        }
    }
    #endregion
}
