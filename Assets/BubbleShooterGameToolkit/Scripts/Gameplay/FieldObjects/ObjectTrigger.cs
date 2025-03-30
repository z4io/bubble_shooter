// // ©2015 - 2025 Candy Smith
// // All rights reserved
// // Redistribution of this software is strictly not allowed.
// // Copy of this software can be obtained from unity asset store only.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// // FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// // THE SOFTWARE.

using BubbleShooterGameToolkit.Scripts.Enums;
using BubbleShooterGameToolkit.Scripts.Gameplay.Managers;
using BubbleShooterGameToolkit.Scripts.Gameplay.PlayObjects;
using UnityEngine;

namespace BubbleShooterGameToolkit.Scripts.Gameplay.FieldObjects
{
    public abstract class ObjectTrigger : MonoBehaviour
    {
        protected DestroyManager destroyManager;
        protected abstract int GetScoreMultiplier(Ball ball);

        [SerializeField]
        private Collider2D[] ignoreEventColliders;

        private bool gameStarted;

        private void Start()
        {
            destroyManager = LevelManager.instance.destroyManager;
        }

        protected virtual void OnEnable()
        {
            EventManager.GetEvent<EStatus>(EGameEvent.Play).Subscribe(GameStarted);
            if(EventManager.GameStatus == EStatus.Play)
                gameStarted = true;
        }
        
        private void OnDisable()
        {
            EventManager.GetEvent<EStatus>(EGameEvent.Play).Unsubscribe(GameStarted);
        }
        private void GameStarted(EStatus obj)
        {
            gameStarted = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!gameStarted)
                return;
            if (ignoreEventColliders != null)
            {
                foreach (var ignoreEventCollider in ignoreEventColliders)
                {
                    if (other == ignoreEventCollider)
                    {
                        return;
                    }
                }
            }
            if (other.CompareTag("Ball"))
            {
                BallCollide(other);
            }
        }

        protected abstract void BallCollide(Collider2D other);

        protected virtual void DestroyBall(Ball ball)
        {
            ScoreManager.instance.AddScore(GetScoreMultiplier(ball), transform.position, false);
            var ballDestructionOptions = new BallDestructionOptions();
            ballDestructionOptions.NoScore = true;
            ball.DestroyBall(ballDestructionOptions);
        }
    }
}