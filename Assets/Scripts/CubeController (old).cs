using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {

    public QueryController queryRunner;
    public string query;

	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        //if(other.tag == "Hand")
        //{
            //queryRunner.runQuery(query);
        //}
    }

}
