using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour {
	
	private Animator _animator;
	private bool _isJumping;
	private bool _currentFacingLeft;
	private Rigidbody2D _rb;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
		_animator.Play("Idle");
		_rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
			
		if (_rb.velocity.x <= -0.1f) {
			_currentFacingLeft = true;
		}
		else if (_rb.velocity.x >= 0.1f) {
			_currentFacingLeft = false;
		} else {
			_isJumping = false;
			
			if (_rb.velocity.sqrMagnitude >= 0.1f * 0.1f) {
				_animator.Play("Walk");
			} else {
				_animator.Play("Idle");
			}
		}

		// Facing
		float valueCheck = _rb.velocity.x;
		
		if (valueCheck >= 0.1f)
		{
			transform.localScale = Vector3.one;
		}
		else if (valueCheck <= -0.1f)
		{
			Vector3 newScale = Vector3.one;
			newScale.x = -1;
			transform.localScale = newScale;
		}
	}
}
