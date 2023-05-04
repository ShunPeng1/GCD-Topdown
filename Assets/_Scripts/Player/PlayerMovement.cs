/*
	Created by @DawnosaurDev at youtube.com/c/DawnosaurStudios
	Thanks so much for checking this out and I hope you find it helpful! 
	If you have any further queries, questions or feedback feel free to reach out on my twitter or leave a comment on youtube :D

	Feel free to use this in your own games, and I'd love to see anything you make!
 */

using System.Collections;
using UnityEngine;

public class PlayerMovement : GridXYGameObject
{
	//Scriptable object which holds all the player's movement parameters. If you don't want to use it
	//just paste in all the parameters, though you will need to manuly change all references in this script
	public PlayerMovementData Data;

	#region COMPONENTS

	private Rigidbody2D _rigidbody2D { get; set; }
	//Script to handle all player animations, all references can be safely removed if you're importing into your own project.
	#endregion

	#region STATE PARAMETERS
	//Variables control the various actions the player can perform at any time.
	//These are fields which can are public allowing for other sctipts to read them
	//but can only be privately written to.
	private bool _isFacingRight { get; set; }
	
	#endregion

	#region INPUT PARAMETERS
	private Vector2 _moveInput;
	#endregion
	
    private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

    protected override void Start()
	{
		base.Start();
		_isFacingRight = true;
	}

	private void Update()
	{
		#region INPUT HANDLER
		_moveInput.x = Input.GetAxisRaw("Horizontal");
		_moveInput.y = Input.GetAxisRaw("Vertical");

		if (_moveInput.x != 0)
			CheckDirectionToFace(_moveInput.x > 0);
		#endregion

		Debug.Log("Player standing grid position "+ GridXY.GetXY(transform.position));
		
	}

    private void FixedUpdate()
	{
		Run();
    }

	//MOVEMENT METHODS
    #region RUN METHODS
    private void Run()
	{
		//Calculate the direction we want to move in and our desired velocity
		float targetSpeedX = _moveInput.x * Data.RunMaxSpeed;
		float targetSpeedY = _moveInput.y * Data.RunMaxSpeed; // Calculate the target speed in the Y direction

		#region Calculate AccelRate
		//Gets an acceleration value based on if we are accelerating (includes turning) 
		//or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
		
		float accelRateX = Mathf.Abs(targetSpeedX) > 0.01f ? Data.RunAccelAmount : Data.RunDecelerateAmount;
		float accelRateY = Mathf.Abs(targetSpeedY) > 0.01f ? Data.RunAccelAmount : Data.RunDecelerateAmount;

		#endregion
		
		#region Conserve Momentum
		//We won't slow the player down if they are moving in their desired direction but at a greater speed than their maxSpeed
		//Prevent any deceleration from happening, or in other words conserve are current momentum
		//You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
		
		if (Data.DoConserveMomentum && Mathf.Abs(_rigidbody2D.velocity.x) > Mathf.Abs(targetSpeedX) && Mathf.Sign(_rigidbody2D.velocity.x) == Mathf.Sign(targetSpeedX) && Mathf.Abs(targetSpeedX) > 0.01f)
		{
			accelRateX = 0;
		}

		if (Data.DoConserveMomentum && Mathf.Abs(_rigidbody2D.velocity.y) > Mathf.Abs(targetSpeedY) && Mathf.Sign(_rigidbody2D.velocity.y) == Mathf.Sign(targetSpeedY) && Mathf.Abs(targetSpeedY) > 0.01f)
		{
			accelRateX = 0; // Add support for conserving momentum in the Y direction as well
		}
		
		#endregion

		//Calculate difference between current velocity and desired velocity
		
		float speedDifX = targetSpeedX - _rigidbody2D.velocity.x;
		float speedDifY = targetSpeedY - _rigidbody2D.velocity.y; 

		//Calculate force along x-axis to apply to thr player
		
		float movementX = speedDifX * accelRateX;
		float movementY = speedDifY * accelRateX; // Calculate the movement in the Y direction

		//Convert this to a vector and apply to rigidbody
		_rigidbody2D.AddForce( new Vector2(movementX, movementY), ForceMode2D.Force);

		/*
		 * For those interested here is what AddForce() will do
		 * RB.velocity = new Vector2(RB.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / RB.mass, RB.velocity.y);
		 * Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second
		*/
	}

	private void Turn()
	{
		//stores scale and flips the player along the x axis, 
		Vector3 scale = transform.localScale; 
		scale.x *= -1;
		transform.localScale = scale;

		_isFacingRight = !_isFacingRight;
	}
    #endregion


    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
	{
		if (isMovingRight != _isFacingRight)
			Turn();
	}
    #endregion
}

// created by Dawnosaur :D