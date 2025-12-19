using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public PlayerControl player;
    public void JumpEvent() {
        player.SetInGround();
    }
}
