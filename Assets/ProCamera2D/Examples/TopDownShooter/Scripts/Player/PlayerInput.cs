using System.Collections;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerInput : MonoBehaviour
    {
        public float RunSpeed = 12;
        public float Acceleration = 30;
        public int playerNum;
        public GameObject box;
        float _currentSpeedH;
        float _currentSpeedV;
        Vector3 _amountToMove;
        int _totalJumps;
        float groundPos;
        Collider[] hitBox;
        CharacterController _characterController;

        bool _movementAllowed = true;

        void Start()
        {
            _characterController = GetComponent<CharacterController>();

            var cinematics = FindObjectsOfType<ProCamera2DCinematics>();
            for (int i = 0; i < cinematics.Length; i++)
            {
                cinematics[i].OnCinematicStarted.AddListener(() =>
                    {
                        _movementAllowed = false; 
                        _currentSpeedH = 0;
                        _currentSpeedV = 0;
                    });

                cinematics[i].OnCinematicFinished.AddListener(() =>
                    {
                        _movementAllowed = true; 
                    });
            }
            _amountToMove = new Vector3();
            groundPos = transform.position.y;
        }

        void Update()
        {
            if (!_movementAllowed)
                return;

            var targetSpeedH = Input.GetAxis("Horizontal" + playerNum) * RunSpeed;
            _currentSpeedH = IncrementTowards(_currentSpeedH, targetSpeedH, Acceleration);

            var targetSpeedV = Input.GetAxis("Vertical" + playerNum) * RunSpeed;
            _currentSpeedV = IncrementTowards(_currentSpeedV, targetSpeedV, Acceleration);

            _amountToMove.x = _currentSpeedH;
            _amountToMove.z = _currentSpeedV;

            _characterController.Move(_amountToMove * Time.deltaTime);
            
            if(_amountToMove.magnitude != 0)
                transform.rotation = Quaternion.LookRotation(_amountToMove.normalized);

            Hit();
        }

        // Increase n towards target by speed
        private float IncrementTowards(float n, float target, float a)
        {
            if (n == target)
            {
                return n;   
            }
            else
            {
                float dir = Mathf.Sign(target - n); 
                n += a * Time.deltaTime * dir;
                return (dir == Mathf.Sign(target - n)) ? n : target;
            }
        }

        void Hit() {
            if (Input.GetButtonDown("Fire1_" + playerNum)) {
                Debug.Log("hit"+ playerNum);
                StartCoroutine(HitBox());
                hitBox = Physics.OverlapBox(box.transform.position, box.transform.localScale);
                for (int i = 0; i < hitBox.Length; i++)
                {
                    if(hitBox[i].tag == "Player" && hitBox[i].name != this.name)
                    {
                        Debug.Log(hitBox[i].name);
                    }
                }
            }
        }

        IEnumerator HitBox (){
            box.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            box.SetActive(false);
        }

        
    }
}