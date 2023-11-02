using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class ObjectController : MonoBehaviour
    {

        [Header("PuzzleScript")]
        [SerializeField] P_Cube pCube;
        [SerializeField] Maze pMaze;

        [Header("Bool")]
         public bool boolCubeRotate;



        private void Start()
        {
            //필요 스크립트 find로 가져오든 씬작업중인거 끝나면 붙이든...
            
        }


        //region 늘리면서 추가
        #region Puzzle_cube
        public void Puzzle_Cube_RotateHor() // 큐브퍼즐 가로 축 회전
        {
            pCube.Click_RotateHorizontal();
        }

        public void Puzzle_Cube_RotateVer() // 큐브퍼즐 세로 축 회전
        {
            pCube.Click_RotateVertical();
        }

        public void Puzzle_Cube_ChooseHor() //큐브퍼즐 가로 축 선택
        {
            pCube.Click_ChangeHorLine();
        }
        public void Puzzle_Cube_ChooseVer() //큐브퍼즐 세로 축 선택
        {
            pCube.Click_ChangeVerLine();
        }
        public void Puzzle_Cube_CheckAnswer() //큐브퍼즐 정답 확인
        {
            pCube.Click_AnswerCheck();
        }
        #endregion
        #region Puzzle_Maze
        public void Puzzle_Maze_Red()
        {

        }
        public void Puzzle_Maze_Green()
        {

        }
        public void Puzzle_Maze_Blue()
        {

        }
        #endregion
    }
}