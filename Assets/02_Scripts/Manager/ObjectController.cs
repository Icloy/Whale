using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class ObjectController : MonoBehaviour
    {
        [Header("RoomScript")]
        [SerializeField] CheckItem checkItem;

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
        public void Puzzle_Cube_LineChoose()
        {
            pCube.BtnClick_LineChoose();
            GameManager.gm.soundManager.Play(SoundManager.AudioType.Click, true);
        }

        public void Puzzle_Cube_Rotate_CheckAnswer() 
        {
            GameManager.gm.soundManager.Play(SoundManager.AudioType.Click, true);
            pCube.BtnClick_Rotate_CheckAnswer();
        }

        public void Puzzle_Cube_ChooseState()
        {
            GameManager.gm.soundManager.Play(SoundManager.AudioType.Click, true);
            pCube.BtnClick_ChooseState();
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
        public void Puzzle_Room_01()
        {
            checkItem.ItemOk(new Vector3(checkItem.itemList[0].transform.position.x, checkItem.itemList[0].transform.position.y, checkItem.itemList[0].transform.position.z));
            checkItem.ItemOk(new Vector3(checkItem.itemList[1].transform.position.x, checkItem.itemList[1].transform.position.y, checkItem.itemList[1].transform.position.z));
            checkItem.ItemDel(0);
            checkItem.itemBool[0] = true;
        }
        public void Puzzle_Room_02()
        {
            checkItem.ItemOk(new Vector3(checkItem.itemList[2].transform.position.x, checkItem.itemList[2].transform.position.y, checkItem.itemList[2].transform.position.z));
            checkItem.ItemOk(new Vector3(checkItem.itemList[3].transform.position.x, checkItem.itemList[3].transform.position.y, checkItem.itemList[3].transform.position.z));

            checkItem.ItemOk(new Vector3(transform.position.x, transform.position.y, transform.position.z));
            checkItem.ItemDel(1);
            checkItem.itemBool[1] = true;
        }
        public void Puzzle_Room_03()
        {
            checkItem.ItemOk(new Vector3(checkItem.itemList[4].transform.position.x, checkItem.itemList[4].transform.position.y, checkItem.itemList[4].transform.position.z));
            checkItem.ItemOk(new Vector3(checkItem.itemList[5].transform.position.x, checkItem.itemList[5].transform.position.y, checkItem.itemList[5].transform.position.z));

            checkItem.ItemOk(new Vector3(transform.position.x, transform.position.y, transform.position.z));
            checkItem.ItemDel(2);
            checkItem.itemBool[2] = true;
        }
        public void Puzzle_Room_04()
        {
            checkItem.ItemOk(new Vector3(checkItem.itemList[6].transform.position.x, checkItem.itemList[6].transform.position.y, checkItem.itemList[6].transform.position.z));
            checkItem.ItemOk(new Vector3(checkItem.itemList[7].transform.position.x, checkItem.itemList[7].transform.position.y, checkItem.itemList[7].transform.position.z));
            Debug.Log("!");
            checkItem.ItemOk(new Vector3(transform.position.x, transform.position.y, transform.position.z));
            checkItem.ItemDel(3);
            checkItem.itemBool[3] = true;
        }
        #endregion
    }

}