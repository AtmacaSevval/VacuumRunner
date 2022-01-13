using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Twenty.Managers;

public class CharacterController : MonoBehaviour
{

    [SerializeField] private float horizontalClamp = 2f;

    private Vector2 currentMoveOffset;

    private SplineFollower splineFollower;
    private Animator animator;


    [Header("Speed Properties")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float followSpeed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        splineFollower = GetComponent<SplineFollower>();
    }
    private void Start()
    {
        animator.SetBool("Idling", false);
        splineFollower.follow = false;
        splineFollower.followSpeed = 0;

    }
    private void OnDisable()
    {
        GameManager.onLevelStart -= GameStarting;
        GameManager.onLevelOver -= GameEnding;
    }
    private void OnEnable()
    {
        GameManager.onLevelStart += GameStarting;
        GameManager.onLevelOver += GameEnding;
    }

    void Update()
    {
        Move();
    }

    private void GameEnding()
    {
        splineFollower.follow = false;
        splineFollower.followSpeed = 0;
        animator.GetComponent<Animator>().enabled = false;
    }
    private void GameStarting()
    {
        splineFollower.follow = true;
        splineFollower.followSpeed = followSpeed;
    }
    private void Move()
    {

        currentMoveOffset = splineFollower.motion.offset;
        currentMoveOffset.x += InputManager.DeltaX * moveSpeed * Time.deltaTime;
        currentMoveOffset.x = Mathf.Clamp(currentMoveOffset.x, -horizontalClamp, horizontalClamp);
        splineFollower.motion.offset = currentMoveOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "barrier")
        {

            GameManager.GameFail();
            
        }
    }
}
