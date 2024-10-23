using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float screenPositionFollowTreshold;

    CharacterController controller;
    Animator animator;

    Vector3 clickedScreenPosition;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CalculateMovemet();
        ApplyGravity();
    }

    void CalculateMovemet()
    {
        bool isNotInUIArea = true;//!RectTransformUtility.RectangleContainsScreenPoint(uiArea, Input.mousePosition) || !uiArea.gameObject.activeInHierarchy;

        if (Input.GetMouseButtonDown(0) && isNotInUIArea)
        {
            clickedScreenPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && isNotInUIArea)
        {
            Vector3 difference = Input.mousePosition - clickedScreenPosition;
            Vector3 distance = difference.normalized;
            float maxScreenDistance = screenPositionFollowTreshold * Screen.height;

            if (difference.magnitude > maxScreenDistance)
            {
                clickedScreenPosition = Input.mousePosition - distance * maxScreenDistance;
                difference = Input.mousePosition - clickedScreenPosition;
            }
            difference /= Screen.height;
            difference.z = difference.y;
            difference.y = 0;
            if (difference.normalized != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(difference.normalized);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            animator.SetBool("walk", difference.normalized != Vector3.zero);
            difference.y = 0;
            controller.Move(moveSpeed * Time.deltaTime * difference);
        }
        else
        {
            animator.SetBool("walk", false);
        }

    }

    void ApplyGravity()
    {
        Vector3 moveVector = Vector3.zero;
        if (controller.isGrounded == false)
        {
            moveVector += Physics.gravity;
        }
        controller.Move(moveVector * Time.deltaTime);
    }
}
