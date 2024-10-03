using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyBird
{
    public class Player : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;
        //private Transform player;
        private Vector3 Birdrot;
        //점프
        [SerializeField] private float jumpForce = 5f;
        private bool keyJump;   //점프입력체크

        //회전 최대값, 최소값
        private float Max = 30f;
        private float Min = -90f;
        //private float rotateZ = 0f;
        //회전 스피드
        [SerializeField] private float rotatespeed = 5f;

        private float movespeed = 10f;

        [SerializeField]private float readyBird = 1f;
        private bool ready = false;
        #endregion

        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            //키입력
            InputBird();
            //버드 대기
            ReadyBird();
            //버드회전
            Birdrotate();
            //player.transform.localRotation = Quaternion.Euler(0, 0, rotateZ);
            //rotateZ = Mathf.Clamp(rotateZ, Min, Max);
            //버드 이동
            BirdMove();

        }
        private void FixedUpdate()
        {
            //점프
            if (keyJump)
            {
                Debug.Log("점프");
                Jumpbird();
                keyJump = false;
            }
            //내려갈때 회전값
            //else if(!keyJump)
            //{
            //rotateZ += -Max * Time.deltaTime * 2;
            //}
        }
        //컨트롤 입력
        void InputBird()
        {
            keyJump |= Input.GetKeyDown(KeyCode.Space);
            keyJump |= Input.GetMouseButtonDown(0);

            if (GameManager.IsStart ==false && keyJump )
            {
                GameManager.IsStart = true;
            }
        }
        //Bird 점프
        void Jumpbird()
        {
            //위쪽으로 힘을 주어 위쪽으로 이동
            //rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb2D.velocity = Vector2.up * jumpForce;
            //회전
            //rotateZ += Max * 2;
        }
        //Bird 회전
        void Birdrotate()
        {
            if (GameManager.IsStart == false)
            {
                return;
            }
            //up + 30, down -90;
            float degree = 0;
            if (rb2D.velocity.y > 0)
            {
                degree = rotatespeed;
            }
            else
            {
                degree = -rotatespeed;
            }
            float rotz = Mathf.Clamp(Birdrot.z + degree, Min, Max);
            Birdrot = new Vector3(0f, 0f, rotz);
            transform.eulerAngles = Birdrot;
        }

        //버드 이동
        void BirdMove()
        {
            if(GameManager.IsStart == false)
            {
                return;
            }
            transform.Translate(Vector3.right * Time.deltaTime * movespeed, Space.World);
        }
        void ReadyBird()
        {
            float rot = 5f;
            if (GameManager.IsStart == true)
            {
                rotatespeed = rot;
                return;
            }
            if(rb2D.velocity.y < 0 && GameManager.IsStart == false)
            {
                rb2D.velocity = Vector2.up * readyBird;
            }
        }
    }
}
