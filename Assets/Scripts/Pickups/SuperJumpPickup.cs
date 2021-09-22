using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperJumpPickup : MonoBehaviour {

    [Header("Settings")]
    [Tooltip("The effect to create when this pickup is collected")]
    public GameObject pickUpEffect;
    public float timeToWait = 10.0f;
    public float jumpPower = 33.0f;
    private PlayerController playerController;
    private Slider slider;
    private float defaultJumpPower = 25.0f;
    private bool isPickedUp = false;

    float currCountdownValue;
    public IEnumerator StartCountdown(float countdownValue) {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0) {
            slider = GameObject.Find("ProgressBar").GetComponent<Slider>();
            slider.value = currCountdownValue / timeToWait;
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        slider.value = 0.0f;
        //now we return to normal and destory
        playerController.isInSuperState = false;
        playerController.jumpPower = defaultJumpPower;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && !isPickedUp) {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController.isInSuperState) return;
            playerController.isInSuperState = true;
            playerController.jumpPower = jumpPower;
            //hide game object from scene while effect is in play
            this.gameObject.GetComponent<Renderer>().enabled = false;
            StartCoroutine(StartCountdown(timeToWait));
        }
    }
}
