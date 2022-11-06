using System.Collections;
using StoryManagement;
using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        public int reactToStoryyLine = -1;
        float distanceTravelled;
        public bool shouldStartToMove = false;
        public bool shouldGoToNextLine = false;
        private bool prepareToSendMessage = true;
        private bool sentMessage = false;
        public int nextLineIndex = 5;
        public Vector3 lastPos;
        public CharacterMoveState currState;
        public float waitToSendnewLine = 1.5f;

        public enum CharacterMoveState
        {
            Move,
            Idle
        }

        
        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
                lastPos = this.transform.position;
            }

            shouldStartToMove = false;

            //currState = CharacterMoveState.Move;
            prepareToSendMessage = false;
            sentMessage = false;
        }

        void Update()
        {
            if (StoryManager.getInstance.currStory.currStoryLine == reactToStoryyLine)
            {
                shouldStartToMove = true;
            }
            

            var currPos = this.transform.position;
            
            if (pathCreator != null && (shouldStartToMove))
            {
                currState = CharacterMoveState.Move;
                distanceTravelled += speed * Time.deltaTime;
                var nextPos = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.position = new Vector3(nextPos.x, nextPos.y, 0.0f);
                //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                prepareToSendMessage = true;
            }
            
            if (lastPos == currPos)
            {
                currState = CharacterMoveState.Idle;
            }
            else
            {
                currState = CharacterMoveState.Move;
            }
            
            lastPos = currPos;
            if (!sentMessage)
            {
                if (currState == CharacterMoveState.Idle && prepareToSendMessage)
                {
                    Debug.Log("Finish");
                    StartCoroutine(WaitToNextLine(waitToSendnewLine));
                    prepareToSendMessage = false;

                    sentMessage = true;
                }

            }


        }

        IEnumerator WaitToNextLine(float time)
        {
           
            
            yield return new WaitForSeconds(time);
            StoryManager.getInstance.ValiDateState(nextLineIndex);
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}