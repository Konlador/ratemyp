using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace RateMyP.Managers
    {
    public interface ICommentManager
        {
        List<Comment> GetAll();
        Comment GetById(Guid commentId);
        void Add(Comment comment);
        }

    public class CommentManager : ICommentManager
        {
        public List<Comment> GetAll()
            {
            using (var dbConnection = SQLDbConnection.CreateToDb())
            using (var dataContext = new DataContext(dbConnection.Connection))
                {
                return dataContext.GetTable<Comment>().ToList();
                }
            }

        public Comment GetById(Guid commentId)
            {
            return GetAll().First(comment => comment.Id.Equals(commentId));
            }

        public void Add(Comment comment)
            {
            using (var dbConnection = SQLDbConnection.CreateToDb())
            using (var dataContext = new DataContext(dbConnection.Connection))
                {
                var comments = dataContext.GetTable<Comment>();
                comments.InsertOnSubmit(comment);
                dataContext.SubmitChanges();
                }
            }
        }
    }
