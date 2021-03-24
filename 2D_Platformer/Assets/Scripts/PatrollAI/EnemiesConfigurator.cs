using System;
using Pathfinding;
using PatrollAI;
using Player;
using UnityEngine;


[Serializable]
public struct AIConfig
{
    public float speed;
    public float minDistanceToTarget;
    public Transform[] waypoints;
    internal float minSqrDistanceToTarget;
}
public class EnemiesConfigurator : MonoBehaviour
{                
    [Header("Simple AI")]
    [SerializeField] private AIConfig _simplePatrolAIConfig;
    [SerializeField] private PlayerView _simplePatrolAIView;

    [Header("Stalker AI")]
    [SerializeField] private AIConfig _stalkerAIConfig;
    [SerializeField] private PlayerView _stalkerAIView;
    [SerializeField] private Seeker _stalkerAISeeker;
    [SerializeField] private Transform _stalkerAITarget;

    #region Fields

    private SimplePatrolAI _simplePatrolAI;
    private StalkerAI _stalkerAI;

    #endregion

  
    #region Unity methods

    private void Start()
    {
        _simplePatrolAI = new SimplePatrolAI(_simplePatrolAIView, new SimplePatrolAIModel(_simplePatrolAIConfig));
      
        _stalkerAI = new StalkerAI(_stalkerAIView, new StalkerAIModel(_stalkerAIConfig), _stalkerAISeeker, _stalkerAITarget);
        InvokeRepeating(nameof(RecalculateAIPath), 0.0f, 1.0f);
    }

    private void FixedUpdate()
    {
        if (_simplePatrolAI != null) _simplePatrolAI.FixedUpdate();
        if (_stalkerAI != null) _stalkerAI.FixedUpdate();
    }
  
    #endregion

    #region Methods

    private void RecalculateAIPath()
    {
        _stalkerAI.RecalculatePath();
    }
      
    #endregion
}