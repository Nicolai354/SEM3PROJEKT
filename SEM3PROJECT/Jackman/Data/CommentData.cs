using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jackman.Models;
using Jackman.Models.Exceptions;

namespace Jackman.Data
{
    public class CommentData : ICommentData
    {
        public IEnumerable<Comment> GetComments(int caseId)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@caseId", caseId, DbType.Int32);
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM [comment] WHERE [case_id] = @caseId");
                dal.ClearParameters();

                return dt.AsEnumerable().Select(r => MapComment(r));
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        public void CreateComment(int caseId, int personId, string text)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@caseId", caseId, DbType.Int32);
                dal.AddParameter("@personId", personId, DbType.Int32);
                dal.AddParameter("@text", text, DbType.String);
                dal.ExecuteStoredProcedure("CommentCreate");
                dal.ClearParameters();
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        private Comment MapComment(DataRow row)
        {
            return new Comment
            {
                Id = (int)row["id"],
                CreatedDate = (DateTime)row["createdDate"],
                Person = new PersonData().GetPerson((int)row["person_id"]),
                Text = row["text"].ToString()
            };
        }
    }
}
