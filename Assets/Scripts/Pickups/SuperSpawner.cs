using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpawner : MonoBehaviour {

    public GameObject superPickup;
    public float spawnTime = 5.0f;

    private void Start() {
        StartCoroutine(spawnWave());
    }

    private void spawnPickup() {
        if (this.gameObject.transform.childCount > 0) return;
        GameObject current = Instantiate(superPickup) as GameObject;
        current.transform.parent = this.gameObject.transform;
        //set to same pos as parent
        current.transform.localPosition = new Vector2(0, 0);
    }

    IEnumerator spawnWave() {
        while(true) {
            yield return new WaitForSeconds(spawnTime);
            spawnPickup();
        }
    }
}
