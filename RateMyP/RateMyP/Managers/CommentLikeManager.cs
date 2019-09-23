using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace RateMyP.Managers
    {
    public interface ICommentLikeManager
        {
        List<CommentLike> GetAll();
        CommentLike GetByIds(Guid commentId, Guid studentId);
        void Add(CommentLike commentLike);
        }

    public class CommentLikeManager : ICommentLikeManager
        {
        public List<CommentLike> GetAll()
            {
            using (var dbConnection = SQLDbConnection.CreateToDb())
            using (var dataContext = new DataContext(dbConnection.Connection))
                {
                return dataContext.GetTable<CommentLike>().ToList();
                }
            }

        public CommentLike GetByIds(Guid commentId, Guid studentId)
            {
            return GetAll().First(commentLike => commentLike.CommentId.Equals(commentId) && commentLike.StudentId.Equals(studentId));
            }

        public void Add(CommentLike commentLike)
            {
            using (var dbConnection = SQLDbConnection.CreateToDb())
            using (var dataContext = new DataContext(dbConnection.Connection))
                {
                var commentLikes = dataContext.GetTable<CommentLike>();
                commentLikes.InsertOnSubmit(commentLike);
                dataContext.SubmitChanges();
                }
            }
        }
    }
