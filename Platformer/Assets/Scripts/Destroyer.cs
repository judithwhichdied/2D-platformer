using System;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public event Action CoinCollected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Coin>(out Coin coin))
        {
            CoinCollected?.Invoke();

            Destroy(collision.gameObject);
        }
    }
}