using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionHandler : MonoBehaviour
{
    [Header("Player Stats SO")]
    [SerializeField] private PlayerStatsSO playerStats;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == playerStats.lastSceneName && playerStats.lastSceneNameSaved)
        {
            StartCoroutine(SetPlayerPos());
        }

        if (ObjectPooler.instance.GetBoolObjectsPooled() != true) ObjectPooler.instance.ObjectPoolerInit();
    }

    private IEnumerator SetPlayerPos()
    {
        Vector3 temp = new Vector3(playerStats.playerPosX, playerStats.playerPosY, playerStats.playerPosZ);
        CharacterController cc = GetComponent<CharacterController>();
        cc.enabled = false;
        transform.position = temp;

        yield return new WaitForSeconds(1f);

        cc.enabled = true;
        playerStats.lastSceneNameSaved = false;

        // Debug.Log("[PlayerPositionHandler] Setting player position from memory at: (" + transform.position + ")");
    }
}
