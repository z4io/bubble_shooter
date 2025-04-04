﻿// // ©2015 - 2025 Candy Smith
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

using BubbleShooterGameToolkit.Scripts.Gameplay.Managers;
using BubbleShooterGameToolkit.Scripts.LevelSystem;
using BubbleShooterGameToolkit.Scripts.System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BubbleShooterGameToolkit.Scripts.CommonUI.Popups
{
    public class MenuPlay : Popup
    {
        private int num;
        
        [SerializeField]
        private Button playButton;

        private void OnEnable()
        {
            playButton.onClick.AddListener(StartGame);
            num = PlayerPrefs.GetInt("OpenLevel");
            var level = LevelLoader.instance.LoadLevel(num);
            EventManager.GetEvent<Level>(EGameEvent.LevelLoaded).Invoke(level);
        }

        public void StartGame()
        {

            if (!GameManager.instance.life.IsEnough(1))
            {
                MenuManager.instance.ShowPopup<LifeShop>();
            }
            else
            {
                OnCloseAction = (x) => { SceneLoader.instance.StartGameScene(); };
                result = EPopupResult.Continue;
                Close();
            }
        }
    }
}