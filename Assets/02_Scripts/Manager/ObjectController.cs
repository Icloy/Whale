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
            //�ʿ� ��ũ��Ʈ find�� �������� ���۾����ΰ� ������ ���̵�...
            
        }


        //region �ø��鼭 �߰�
        #region Puzzle_cube
        public void Puzzle_Cube_RotateHor() // ť������ ���� �� ȸ��
        {
            pCube.Click_RotateHorizontal();
        }

        public void Puzzle_Cube_RotateVer() // ť������ ���� �� ȸ��
        {
            pCube.Click_RotateVertical();
        }

        public void Puzzle_Cube_ChooseHor() //ť������ ���� �� ����
        {
            pCube.Click_ChangeHorLine();
        }
        public void Puzzle_Cube_ChooseVer() //ť������ ���� �� ����
        {
            pCube.Click_ChangeVerLine();
        }
        public void Puzzle_Cube_CheckAnswer() //ť������ ���� Ȯ��
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