using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [Header("Player Referance")]
    [SerializeField] private Player player;
    [Header("Animation Variables")]
    [SerializeField] private const string IS_WALKING = "IsWalking";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking);
    }
}
