using BraidGirl;
using UnityEngine;
using TMPro;

public class UiHp : MonoBehaviour
{
    private UnitHealth _playerHealth;
    private TMP_Text _text;
    private GameObject _player;
    private int _lastCheckpointId;

    private void Start()
    {
        GameObject character = GameObject.Find("Character");
        _playerHealth = character.GetComponent<UnitHealth>();
        _text = GetComponent<TMP_Text>();
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        _lastCheckpointId = _player.GetComponent<Player>().GetLastCheckPointId();
        _text.text = _playerHealth.CurrentHealth + "/" + _playerHealth.CurrentMaxHealth + " checkpoint=" + _lastCheckpointId;
    }
}
