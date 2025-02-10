using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    [SerializeField] private List<float> movementSpeed = new List<float>();
    private List<Transform> parts = new List<Transform>();
    [SerializeField] private float movePoint;
    [SerializeField] Transform otherMover;

    private void Awake()
    {
        //Get the nummber of children movement speed has been added for
        for(int i = 0; i < movementSpeed.Count; i++)
        {
            parts.Add(transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        ResetPosition();
        UpdateMovement();
    }

    public void IncreaseMovementSpeed(float speed)
    {
        for(int i = 0;i < movementSpeed.Count; i++)
        {
            movementSpeed[i] += speed;
        }
    }

    private void UpdateMovement()
    {
        for (int count = 0; count < movementSpeed.Count; count++)
        {
            this.parts[count].Translate(Vector2.left * movementSpeed[count] * Time.deltaTime);
        }
    }

    private void ResetPosition()
    {
        foreach(Transform part in parts)
        {
            if(part.position.x <= -movePoint)
            {
                part.position = otherMover.Find(part.transform.name).position + Vector3.right * movePoint;
            }
        }
    }
}
