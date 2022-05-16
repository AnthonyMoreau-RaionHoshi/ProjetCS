using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private AudioSource sound_coin_collecte;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            sound_coin_collecte.Play();
            DataPlayer.Coins++;
            Destroy(gameObject);
        }
    }
}
