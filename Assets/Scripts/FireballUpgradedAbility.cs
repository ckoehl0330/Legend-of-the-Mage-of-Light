using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/New Upgraded Fireball Ability", fileName = "New Upgraded Fireball Ability")]
public class FireballUpgradedAbility : GenericAbility
{
    [SerializeField]
    private GameObject thisProjectile;
    [SerializeField]
    private int numberOfProjectiles;
    [SerializeField]
    private float projectileSpread;

    public override void Ability(Vector2 playerPosition, Vector2 playerFacingDirection, Animator playerAnimator = null, Rigidbody2D playerRigidbody = null)
    {
        float facingRotation = Mathf.Atan2(playerFacingDirection.y, playerFacingDirection.x) * Mathf.Rad2Deg;
        float startRotation = facingRotation + projectileSpread / 2f;
        float angleIncrease = projectileSpread / ((float)numberOfProjectiles - 1f);

        for(int i = 0; i < numberOfProjectiles; i++)
        {
            float tempRot = startRotation - angleIncrease * i;
            GameObject newProjectile = Instantiate(thisProjectile, playerPosition, Quaternion.Euler(0f, 0f, tempRot));
            FireballSpell temp = newProjectile.GetComponent<FireballSpell>();
            if (temp)
            {
                temp.Setup(new Vector2(Mathf.Cos(tempRot * Mathf.Deg2Rad), Mathf.Sin(tempRot * Mathf.Deg2Rad)));
            }
        }
       
    }
}
