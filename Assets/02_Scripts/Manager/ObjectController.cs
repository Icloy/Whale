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
        [HideInInspector] public bool boolCubeRotate;



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
        public void Puzzle_Cube_Random() //큐브 랜덤 위치 생성
        {
            List<Cube> availableCubes = new List<Cube>(pCube.LubiksCubeScript);
            availableCubes.RemoveAll(cube => cube.cubeKey == 14); // 14번 큐브는 제외
            pCube.PickRandomCube(availableCubes);
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
        #region Puzzle_Room
        public void Puzzle_Room_aa()
        {

        }
        #endregion
    }
}