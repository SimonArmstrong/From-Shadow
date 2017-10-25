using UnityEngine;
using System.Collections;

public class TinkerAnimator : MonoBehaviour {
	public TinkerAnimation[] animations;
	public int currentAnimation = 0;
	public bool destroyAfterPlayed;
	public SpriteRenderer renderer;

	int frameIndex = 0;
	float timer;

	private bool isChild = false;

	private void Start(){
	}

	private void SwitchFrames (){
		// Reset frames if we've reached the end -- Creates Loop
		if (frameIndex > animations [currentAnimation].sprites.Length - 1) {
			if (destroyAfterPlayed)
				Destroy (gameObject);
			frameIndex = 0;
		}

		// If we haven't involved a renderer, involove it
		if (renderer == null) {
			renderer = GetComponent<SpriteRenderer> ();
		}

		// 
		renderer.sprite = animations[currentAnimation].sprites [frameIndex];
	}

	private void Update(){
		if (animations.Length > 0) {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				frameIndex++;
				SwitchFrames ();
				timer = animations [currentAnimation].timeBetweenFrames;
			}
		}
	}

	public TinkerAnimation GetCurrentAnimation(){
		return animations [currentAnimation];
	}

	public string GetAnimationName(){
		return animations [currentAnimation].name;
	}
}
