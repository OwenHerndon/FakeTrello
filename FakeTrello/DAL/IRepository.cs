using FakeTrello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeTrello.DAL
{
    interface IRepository
    {
        //list of methods to help deliver features
        //Create
        void AddBoard(string name, ApplicationUser owner);
        void AddList(string name, Board board);
        void AddList(string name, int boardId);
        void AddCard(string name, List list, ApplicationUser owner);
        void AddCard(string name, int listId, string ownerId);

        //Read
        List<Card> GetCardsFromList(int listId);
        List<Card> GetCardsFromBoard(int boardId);
        Card GetCard(int cardId);
        List GetList(int listId);
        List<List> GetListFromBoard(int boardId);
        List<Board> GetBoardFromUser(string userId);
        Board GetBoard(int boardId);
        List<ApplicationUser> GetCardAttendees(int cardId);

        //Update
        //bool AttachUserToBoard
        //attach user to card
        bool AttachUser(int userId, int cardId);
        //void DetachUser(int userId, int cardId);
        bool MoveCard(int oldListId, int newListId, int cardId);
        bool CopyCard(int cardId, int newListId, string newOwnerId);


        //Delete
        bool RemoveBoard(int boardId);
        bool RemoveList(int listId);
        bool RemoveCard(int cardId);


    }
}
