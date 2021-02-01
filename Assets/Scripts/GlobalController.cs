using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalController : MonoBehaviour
{
    public float DeadTime;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera cam;
    [SerializeField] private Camera cam1;
    private GameObject _currentPlayer;
    public Transform count;
    public Vector3 startPosition;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        Revive();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_currentPlayer)
        {
            Revive();
        }
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Revive()
    {
        Revive(startPosition, 0);
    }

    public void Revive(Vector3 position)
    {
        Revive(position, 0);
    }

    public void Revive(Vector3 position, int level)
    {
		if(_currentPlayer) Destroy(_currentPlayer);
        _currentPlayer = Instantiate(_playerPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        cam.m_Follow = _currentPlayer.transform;
        RestoreAbility(level);
        StartCoroutine("DeadTimeCount");
    }

    public void SetRevivePosition(Vector3 position)
    {
        startPosition = position;
    }


    public void Recover()   
    {
        if (_currentPlayer)
        {
            Debug.Log("恢复");
            _currentPlayer.gameObject.GetComponent<PlayerController>().Recover(health);
        }

        StopCoroutine("DeadTimeCount");
        StartCoroutine("DeadTimeCount");
    }

    private void RestoreAbility(int level)
    {
        if (_currentPlayer)
        {
            _currentPlayer.gameObject.GetComponent<PlayerController>().RestoreAbility(level);
        }
    }

    private IEnumerator DeadTimeCount()
    {
        float totalTime = DeadTime;
        while (totalTime >= 0)
        {
            // TODO: 此处输出倒计时时间

            yield return new WaitForSeconds(1);
            totalTime -= 1;
            count.GetComponent<Text>().text = totalTime + "";
        }
        
        Revive();
    }

    public Camera GetCamera() // 传递镜头
    {
        return cam1;
    }
    
}