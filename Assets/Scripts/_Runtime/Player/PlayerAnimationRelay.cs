using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    private Player2DMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponentInParent<Player2DMovement>();

    }

    //public void OnSpin()
    //{
    //    playerCombat?.HandleAttack(); // викликає метод з Player
    //}
}

