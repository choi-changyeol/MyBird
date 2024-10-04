using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{
    public class GroundMove : MonoBehaviour
    {
        [SerializeField]private float move = 5;
        // Update is called once per frame
        void Update()
        {
            if (GameManager.IsDeath) return;
            Move();
        }
        void Move()
        {
            if (GameManager.IsStart == false)
            {
                return;
            }
            transform.Translate(Vector3.left * Time.deltaTime * move, Space.World);
            if (transform.localPosition.x <= -7.8f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x + 7.8f,transform.localPosition.y,transform.localPosition.z);
            }
        }
    }
}