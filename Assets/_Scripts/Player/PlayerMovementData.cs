using UnityEngine;

[CreateAssetMenu(menuName = "Player Movement Data")] //Create a new playerData object by right clicking in the Project Menu then Create/Player/Player Data and drag onto the player
public class PlayerMovementData : ScriptableObject
{
    [Header("Run")]
    public float RunMaxSpeed; //Target speed we want the player to reach.
    public float RunAcceleration; //Time (approx.) time we want it to take for the player to accelerate from 0 to the runMaxSpeed.
    [HideInInspector] public float RunAccelAmount; //The actual force (multiplied with speedDiff) applied to the player.
    public float RunDeceleration; //Time (approx.) we want it to take for the player to accelerate from runMaxSpeed to 0.
    [HideInInspector] public float RunDecelerateAmount; //Actual force (multiplied with speedDiff) applied to the player .
    [Space(10)]
    [Range(0.01f, 1)] public float AccelInAir; //Multipliers applied to acceleration rate when airborne.
    [Range(0.01f, 1)] public float DecelerateInAir;
    public bool DoConserveMomentum;


    private void OnValidate()
    {
        //Calculate are run acceleration & deceleration forces using formula: amount = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed
        RunAccelAmount = (50 * RunAcceleration) / RunMaxSpeed;
        RunDecelerateAmount = (50 * RunDeceleration) / RunMaxSpeed;

        #region Variable Ranges
        RunAcceleration = Mathf.Clamp(RunAcceleration, 0.01f, RunMaxSpeed);
        RunDeceleration = Mathf.Clamp(RunDeceleration, 0.01f, RunMaxSpeed);
        #endregion
    }
}