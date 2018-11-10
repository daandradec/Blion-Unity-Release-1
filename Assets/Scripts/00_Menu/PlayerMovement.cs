using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public GameController gameController;

    public void MakeAnimation(string sceneToChangeOnFinished)
    {
        StartCoroutine(MoveRight(sceneToChangeOnFinished));        
    }

    private IEnumerator MoveRight(string sceneToChangeOnFinished)
    {
        bool flag = true;
        while (flag)
        {
            //float X = transform.position.x + 1f;
            transform.Translate(Vector3.forward * 6.4f * Time.deltaTime);
            if (transform.position.x >= 6f)
                flag = false;
            yield return new WaitForEndOfFrame();//new WaitForSeconds(0.5f);
        }
        
        gameController.LoadSceneByName(sceneToChangeOnFinished);
    }
}
