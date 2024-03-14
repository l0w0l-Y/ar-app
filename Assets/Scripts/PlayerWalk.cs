using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 moveDirection;
    public float rotationSpeed = 1.0f;
    public Vector3 rotationAxis = Vector3.up;
    public Quaternion rotation = Quaternion.identity;

    private float currentAngle = 0.0f;

    void Start()
    {
        ChangeDirection();
    }

    void FixedUpdate()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void ChangeDirection(){
        moveDirection = Random.insideUnitSphere.normalized;
        moveDirection.y = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        moveDirection = moveDirection * -1.0f;
    }

    void OnTriggerExit(Collider other){
        ChangeDirection();
    }
}