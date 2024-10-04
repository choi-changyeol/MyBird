
using UnityEngine;

namespace MyBird
{
    public class SpawnManager : MonoBehaviour
    {
        #region Variables
        public Transform DestroyPipe;
        public GameObject Pipe;
        [SerializeField] private float Timer = 1;
        private float count = 0;

        [SerializeField] private float MaxSpawnTimer = 1.5f;
        [SerializeField] private float MinSpawnTimer = 0.5f;

        //스폰위치
        [SerializeField] private float SpawnMax = 3.5f;
        [SerializeField] private float SpawnMin = -1.5f;
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            count = Timer;
        }

        // Update is called once per frame
        void Update()
        {

            if (GameManager.IsStart == false) return;
            if (count <= 0)
            {
                //스폰
                PipeSpawn();
                //Destroy(Pipe,count + 2);
                //타이머 초기화
                //count = Timer;
                count = Random.Range(MinSpawnTimer, MaxSpawnTimer);
            }
            count -= Time.deltaTime;
        }
        void PipeSpawn()
        {
            if (GameManager.IsDeath) return;
            float spawnY = transform.position.y + Random.Range(SpawnMin, SpawnMax);
            Vector3 vector3 = new Vector3(transform.position.x, spawnY, 0);
            Pipe = Instantiate(Pipe, vector3, Quaternion.identity);
        }
    }
}