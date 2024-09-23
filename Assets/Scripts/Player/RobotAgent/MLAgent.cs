using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class MLAgent : Agent, IBaseAgent
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayTurn()
    {
        // CollectObservations();
        // OnActionReceived(new float[1]);

    }

    // public override void OnEpisodeBegin()
    // {
    //     Debug.Log("Episode Begin");
    // }

    // public override void CollectObservations()
    // {
    //     AddVectorObs(1);
    // }

    // public override void OnActionReceived(float[] vectorAction)
    // {
    //     output = vectorAction[0];
    // }

    // public override void Heuristic(float[] actionsOut) {}

}
