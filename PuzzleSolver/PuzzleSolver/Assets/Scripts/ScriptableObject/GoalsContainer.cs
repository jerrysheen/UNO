using System;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleSolver
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GoalsContainer", order = 1)]
    public class GoalsContainer : ScriptableObject
    {

        public List<Goals> goals;

    }

    [Serializable]
    public class Goals
    {
        public string name;
        public State isFinish;
    }

    public enum State
    {
        Done, NotYetFinish
    }
}
