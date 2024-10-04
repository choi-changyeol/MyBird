using System;
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

        [SerializeField]private float movespeed = 10f;

        [SerializeField]private float readyBird = 1f;

        //게임 UI
        public GameObject readyUI;
        public GameObject GameOverUI;
        #endregion

        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            readyUI.SetActive(true);
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
                MoveStartBrid();
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
        }
        //Bird 점프
        void Jumpbird()
        {
            if (GameManager.IsDeath) return;
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
        //버드 죽음 처리
        void DeathBird()
        {
            if(GameManager.IsDeath)
                return;
            GameManager.IsDeath = true;
            GameOverUI.SetActive(true);
            //Debug.Log("죽음 처리");
        }
        //점수 획득
        void GetPoint()
        {
            if (GameManager.IsDeath)
                return;
            //Debug.Log("점수 획득");
            GameManager.Score++;
        }

        public void MoveStartBrid()
        {
            GameManager.IsStart = true;
            readyUI.SetActive(false);
        }
        //버드 충돌 처리
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Point")
            {
                GetPoint();
            }
            else if(collider.tag == "Pipe")
            {
                DeathBird();
            }
        }
        //버드 그라운드 충돌 처리
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                DeathBird();
            }
        }
    }
}
