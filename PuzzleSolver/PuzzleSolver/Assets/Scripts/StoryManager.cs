using System.Collections;
using System.Collections.Generic;
using SystemManager;
using PuzzleSolver;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class StoreManager : MonoBehaviour
{
    public GoalsContainer goals;
    // Start is called before the first frame update
    public string currGoalName;
    public string currGoalIndex;
    private List<PuzzleSolver.GoalsContainer> currSceneGoals;
    private int currIndex;

    public string nextSceneName;
    void Start()
    {
        currSceneGoals = new List<GoalsContainer>();
        currIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void NextGoal(int currGoal)
    {
        if (currGoal != currIndex) return;
        currIndex++;
        if (currIndex >= currSceneGoals.Count)
        {
            SceneManager.LoadScene(nextSceneName);
        }

    }
    
    // 每一个自己的scene里面，用一些简单的控制函数去一个个控制着走下去。
}
