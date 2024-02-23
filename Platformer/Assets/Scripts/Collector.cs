using UnityEngine;

[RequireComponent (typeof(Destroyer))]
public class Collector : MonoBehaviour
{
    private int _coinCount;

    private Destroyer _destroyer;

    private void Awake()
    {
        _destroyer = GetComponent<Destroyer>();
    }

    private void OnEnable()
    {
        _destroyer.CoinCollected += TakeCoin;
    }

    private void OnDisable()
    {
        _destroyer.CoinCollected -= TakeCoin;
    }

    private void TakeCoin()
    {
        _coinCount++;
        Debug.Log(_coinCount);
    }
}
