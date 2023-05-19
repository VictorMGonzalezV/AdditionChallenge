using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLine : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Game Over");
        //Destroy(other.gameObject);
        GameManager.Instance.GameOver();

    }
}
