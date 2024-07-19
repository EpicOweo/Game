using UnityEngine;

public class MetalDoor : Door {


    public Animator animator;

    protected override void PlayCloseAnimation() {
        animator.Play("Close");
    }

    protected override void PlayOpenAnimation() {
        animator.Play("Open");
    }

}
