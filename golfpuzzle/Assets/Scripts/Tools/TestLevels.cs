using System;
using System.Collections.Generic;
using System.IO;
using Highscores;
using Newtonsoft.Json;
using UnityEngine;

namespace Tools
{
    public class TestLevels : Singleton<TestLevels>
    {
        private void CheckLevelSolutionNumber(List<Level> mLevels)
        {
            string path = "Assets/Resources/solution.txt";

            //Write some text to the test.txt file
            StreamWriter writer = new StreamWriter(path, true);
            for (int i = 1; i <= 92; i++)
            {
                Level checkedLevel = null;
                string levelSolution = "";
                foreach (Level level in mLevels)
                {
                    if (level.levelNumber == i)
                    {
                        checkedLevel = level;
                    }
                }

                if (checkedLevel != null && checkedLevel.solution != null)
                {
                    levelSolution = checkedLevel.solution;
                }

                string mysolution = HighscoreManager.GetInstance().GetLevelSolution(i);

                int solveMoveNumber = 100;
                try
                {
                    solveMoveNumber = checkedLevel.solveMoveNumber;
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }

                int levelSolveNumber = 100;
                int mySolveNumber = 100;
                if (levelSolution.Length > 1)
                {
                    levelSolveNumber = levelSolution.Length;
                }

                if (mysolution.Length > 1)
                {
                    mySolveNumber = mysolution.Length;
                }

                int minSolNumber = Math.Min(levelSolveNumber, mySolveNumber);

                if (minSolNumber != solveMoveNumber)
                {
                    string message = String.Format(
                        "Level number {0} has problem with solution file solution = {1} my solution = {2}",
                        checkedLevel.levelNumber, checkedLevel.solveMoveNumber, minSolNumber);
                    writer.WriteLine(message);

                    if (minSolNumber != 100)
                    {
                        checkedLevel.solveMoveNumber = minSolNumber;
                    }
                }
            }

            writer.Close();
            string mLevelsString = JsonConvert.SerializeObject(mLevels);
            SaveLevelsToFile(mLevelsString);
        }

        private void SaveLevelsToFile(string mLevelsString)
        {
            string path = "Assets/Resources/levels.txt";
            //Write some text to the test.txt file
            StreamWriter writer = new StreamWriter(path, true);
            writer.Write(mLevelsString);
            writer.Close();
        }


        public void PrintLevelSolution(List<Level> mLevels)
        {
            string path = "Assets/Resources/solution.txt";

            //Write some text to the test.txt file
            StreamWriter writer = new StreamWriter(path, true);
            for (int i = 0; i <= 91; i++)
            {
                Level checkedLevel = null;
                string levelSolution = "";
                foreach (Level level in mLevels)
                {
                    if (level.levelNumber == i)
                    {
                        checkedLevel = level;
                    }
                }

                if (checkedLevel != null && checkedLevel.solution != null)
                {
                    levelSolution = checkedLevel.solution;
                }

                string mysolution = HighscoreManager.GetInstance().GetLevelSolution(i);
                string resstring = "";
                try
                {
                    if (mysolution.Equals("") && levelSolution.Equals(""))
                    {
                        resstring = String.Format("Level number = {0}; NO SOLUTION", i);
                        writer.WriteLine(resstring);
                    }
                    else
                    {
                        if (!mysolution.Equals(levelSolution))
                        {
                            resstring = String.Format("Level number = {0}; my solution = {1} ; def solution = {2}", i,
                                mysolution, levelSolution);
                            writer.WriteLine(resstring);
                        }
                        
                        
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }

                
            }

            writer.Close();
        }

        public void SetSolution(List<Level> mLevels)
        {
            for (int i = 0; i <= 91; i++)
            {
                Level checkedLevel = null;
                string levelSolution = "";
                foreach (Level level in mLevels)
                {
                    if (level.levelNumber == i)
                    {
                        checkedLevel = level;
                    }
                }

                if (checkedLevel != null && checkedLevel.solution != null)
                {
                    levelSolution = checkedLevel.solution;
                }

                string mysolution = HighscoreManager.GetInstance().GetLevelSolution(i);
                string resstring = "";
                try
                {
                    if (mysolution.Equals("") && levelSolution.Equals(""))
                    {
                        resstring = String.Format("Level number = {0}; NO SOLUTION", i);
                    }
                    else
                    {
                        resstring = String.Format("Level number = {0}; my solution = {1} ; def solution = {2}", i,
                            mysolution, levelSolution);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }

                
            }
            
            
        }
    }
}