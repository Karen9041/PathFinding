using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Builds the graph
/// </summary>
public class GraphBuilder : MonoBehaviour
{
    static Graph<Waypoint> graph;

    #region Constructor

    // Uncomment the code below after copying this class into the console
    // app for the automated grader. DON'T uncomment it now; it won't
    // compile in a Unity project

    /// <summary>
    /// Constructor
    /// 
    /// Note: The GraphBuilder class in the Unity solution doesn't 
    /// use a constructor; this constructor is to support automated grading
    /// </summary>
    /// <param name="gameObject">the game object the script is attached to</param>
    //public GraphBuilder(GameObject gameObject) :
    //    base(gameObject)
    //{
    //}

    #endregion

    /// <summary>
    /// Awake is called before Start
    ///
    /// Note: Leave this method public to support automated grading
    /// </summary>
    public void Awake()
    {
        // add nodes (all waypoints, including start and end) to graph
        List<GameObject> waypoints = new List<GameObject>();
        waypoints.AddRange(GameObject.FindGameObjectsWithTag("Start"));
        waypoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
        waypoints.AddRange(GameObject.FindGameObjectsWithTag("End"));
        graph = new Graph<Waypoint>();

        for(int i=0; i < waypoints.Count; i++)
        {
            Waypoint waypoint = waypoints[i].GetComponent<Waypoint>();
            if(waypoint != null)
            {
                graph.AddNode(waypoint);
            }
            else
            {
                Debug.LogError("Waypoint component not found on game object with tag " + waypoint.Id);
            }
        }
        // add neighbors for each node in graph
        foreach(GraphNode<Waypoint> node in graph.Nodes)
        {
            for(int i=0; i < waypoints.Count; i++)
            {
                Waypoint anothernode = waypoints[i].GetComponent<Waypoint>();
                if(Mathf.Abs(anothernode.Position.x - node.Value.Position.x) <= 3.5f && Mathf.Abs(anothernode.Position.y - node.Value.Position.y) <= 3f && anothernode.Id != node.Value.Id)
                {
                    
                    node.AddNeighbor(graph.Find(anothernode), Vector2.Distance(anothernode.Position, node.Value.Position));
                }
            }
                
        }
    }

    /// <summary>
    /// Gets and sets the graph
    /// 
    /// CAUTION: Set should only be used by the autograder
    /// </summary>
    /// <value>graph</value>
    public static Graph<Waypoint> Graph
    {
        get { return graph; }
        set { graph = value; }
    }
}
