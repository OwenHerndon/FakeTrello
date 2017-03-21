using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeTrello.Models;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;

namespace FakeTrello.DAL
{
    public class FakeTrelloRepository : IRepository
    {

        //public FakeTrelloContext Context { get; set; }
        //private FakeTrelloContext context; // Data member
        //DbConnection _trelloConnection;
        SqlConnection _trelloConnection;

        public FakeTrelloRepository()
        {
            //Context = new FakeTrelloContext();
            _trelloConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        //public FakeTrelloRepository(FakeTrelloContext context)
        //{
        //    //Context = context;
        //}


        public void AddBoard(string name, ApplicationUser owner)
        {
            //Board board = new Board { Name = name, Owner = owner };
            //Context.Boards.Add(board);
            //Context.SaveChanges();

            _trelloConnection.Open();

            try
            {

                //var addBoardCommand = new SqlCommand("Select * from Boards", _trelloConnection);

                var addBoardCommand = _trelloConnection.CreateCommand();
                addBoardCommand.CommandText = @"
                    Insert into Boards(Name, Owner_Id)
                    values(@name,@ownerId)         
                    ";
                var nameParameter = new SqlParameter("name", SqlDbType.VarChar);
                nameParameter.Value = name;
                addBoardCommand.Parameters.Add(nameParameter);

                var ownerParameter = new SqlParameter("owner", SqlDbType.Int);
                ownerParameter.Value = owner.Id;
                addBoardCommand.Parameters.Add(ownerParameter);

                addBoardCommand.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _trelloConnection.Close();
            }

        }

        public void AddCard(string name, int listId, string ownerId)
        {
            throw new NotImplementedException();
        }

        public void AddCard(string name, List list, ApplicationUser owner)
        {
            throw new NotImplementedException();
        }

        public void AddList(string name, int boardId)
        {
            throw new NotImplementedException();
        }

        public void AddList(string name, Board board)
        {
            throw new NotImplementedException();
        }

        public bool AttachUser(string userId, int cardId)
        {
            throw new NotImplementedException();
        }

        public bool CopyCard(int cardId, int newListId, string newOwnerId)
        {
            throw new NotImplementedException();
        }

        public Board GetBoard(int boardId)
        {
            // SELECT * FROM Boards WHERE BoardId = boardId 
            //Board found_board = Context.Boards.FirstOrDefault(b => b.BoardId == boardId); // returns null if nothing is found
            //return found_board;

            /* Using .First() throws an exception if nothing is found
             * try {
             * Board found_board = Context.Boards.First(b => b.BoardId == boardId); 
             * return found_board;
             * } catch(Exception e) {
             * return null;
             * }
             */

            _trelloConnection.Open();
            try
            {
                var getBoardCommand = _trelloConnection.CreateCommand();
                getBoardCommand.CommandText =@" 
                    SELECT boardId,Name,URL,Owner_Id 
                    FROM Boards 
                    WHERE BoardId = @boardId";
                var boardIdParam = new SqlParameter("boardId", SqlDbType.Int);
                boardIdParam.Value = boardId;
                getBoardCommand.Parameters.Add(boardIdParam);

                var reader = getBoardCommand.ExecuteReader();
                reader.Read(); //just gets the values of the board, then we have to set them below

                var board = new Board
                {
                    BoardId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    URL = reader.GetString(2),
                    Owner = new ApplicationUser {Id = reader.GetString(3)}
                };
                return board;
            }
            catch(Exception ex) { }
            finally
            {
                _trelloConnection.Close();
            }
            return null;

        }

        public List<Board> GetBoardsFromUser(string userId)
        {
            // return Context.Boards.Where(b => b.Owner.Id == userId).ToList();
            _trelloConnection.Open();

            try
            {
                var getBoardCommand = _trelloConnection.CreateCommand();
                getBoardCommand.CommandText = @" 
                    SELECT boardId,Name,URL,Owner_Id 
                    FROM Boards 
                    WHERE Owner_Id = @userId";
                var userIdParam = new SqlParameter("userId", SqlDbType.VarChar);
                userIdParam.Value = userId;
                getBoardCommand.Parameters.Add(userIdParam);

                var reader = getBoardCommand.ExecuteReader();

                var boards = new List<Board>();
                while (reader.Read())
                {

                    var board = new Board
                    {
                        BoardId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        URL = reader.GetString(2),
                        Owner = new ApplicationUser { Id = reader.GetString(3) }
                    };
                    boards.Add(board);
                }
                
            }
            catch (Exception ex) { }
            finally
            {
                _trelloConnection.Close();
            }
            return new List<Board>();
        }

        public Card GetCard(int cardId)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUser> GetCardAttendees(int cardId)
        {
            throw new NotImplementedException();
        }

        public List<Card> GetCardsFromBoard(int boardId)
        {
            throw new NotImplementedException();
        }

        public List<Card> GetCardsFromList(int listId)
        {
            throw new NotImplementedException();
        }

        public List GetList(int listId)
        {
            throw new NotImplementedException();
        }

        public List<List> GetListsFromBoard(int boardId)
        {
            throw new NotImplementedException();
        }

        public bool MoveCard(int cardId, int oldListId, int newListId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveBoard(int boardId)
        {
            //Board found_board = GetBoard(boardId);
            //if (found_board != null)
            //{
            //    Context.Boards.Remove(found_board);
            //    Context.SaveChanges();
            //    return true;
            //}
            //return false;

            _trelloConnection.Open();

            try
            {

                //var addBoardCommand = new SqlCommand("Select * from Boards", _trelloConnection);

                var removeBoardCommand = _trelloConnection.CreateCommand();
                removeBoardCommand.CommandText = @"
                    Delete
                    From Boards
                    Where BoardId = @boardId         
                    ";
                var boardIdParameter = new SqlParameter("boardId", SqlDbType.Int);
                boardIdParameter.Value = boardId;
                removeBoardCommand.Parameters.Add(boardIdParameter);

                removeBoardCommand.ExecuteNonQuery();

                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _trelloConnection.Close();
            }
            return false;

        }

        public bool RemoveCard(int cardId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveList(int listId)
        {
            throw new NotImplementedException();
        }

        public void EditBoardName(int boardId, string newname)
        {
            //Board found_board = GetBoard(boardId);
            //if (found_board != null)
            //{
            //    found_board.Name = newname; // Akin to 'git add'
            //    Context.SaveChanges(); // Akin to 'git commit'
            //}
            // False Positive: SaveChanges is missing.

            _trelloConnection.Open();

            try
            {

                //var addBoardCommand = new SqlCommand("Select * from Boards", _trelloConnection);

                var editBoardCommand = _trelloConnection.CreateCommand();
                editBoardCommand.CommandText = @"
                    Update Boards
                    Set Name = @name
                    Where BoardId = @boardId         
                    ";
                var nameParameter = new SqlParameter("name", SqlDbType.VarChar);
                nameParameter.Value = newname;
                editBoardCommand.Parameters.Add(nameParameter);

                var boardIdParameter = new SqlParameter("boardId", SqlDbType.Int);
                boardIdParameter.Value = boardId;
                editBoardCommand.Parameters.Add(boardIdParameter);

                editBoardCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _trelloConnection.Close();
            }

        }
    }
}