using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Respawn : MonoBehaviour
{
    public List<GameObject> respawnPoints = new List<GameObject>();
    public float respawnTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(RespawnPlayer(collision.gameObject));
        }
    }

    private IEnumerator RespawnPlayer(GameObject player) {
        yield return new WaitForSeconds(respawnTime);

        int randomIndex = Random.Range(0, respawnPoints.Count);
        player.transform.position = respawnPoints[randomIndex].transform.position;
    }
}
