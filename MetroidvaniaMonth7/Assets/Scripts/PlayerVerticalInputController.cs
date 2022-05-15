 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.InputSystem;

 public class PlayerVerticalInputController : MonoBehaviour, IVerticalDirection
{
    [SerializeField]
    PlayerStateSO playerState;
    float moveY;
    Vector2 moveDirectionY;
    private Vector3 moveVec;
    

    public Vector2 MoveDirectionY { get => moveDirectionY; set => moveDirectionY = value; }

    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();
        moveVec = new Vector3(inputVec.x, 0, inputVec.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState.IsPlayerReady())
        {
           // moveY = Input.GetAxisRaw("Vertical");
           moveY = moveVec.y;
           moveDirectionY = new Vector2(0, moveY);
           
        }
    }
}
