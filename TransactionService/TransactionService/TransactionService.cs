using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Newtonsoft.Json;
using RabbitCommunications.Models;
using RabbitMQ.Client.Events;

namespace TransactionService
{
    public class TransactionService : ITransactionService
    {
        public Response<string> ApplyQuerys(string model)
        {
            var querys = JsonConvert.DeserializeObject<List<string>>(model);

            PerformTransaction(querys);
            
            return new Response<string>();
        }

        private Response<string> PerformTransaction(List<string> querys)
        {
            //SqlTransaction transaction = null;
            //SqlConnection con = null;
            IDbTransaction transaction = null;
            IDbConnection con = null;

            // they will be used to decide whether to commit or rollback the transaction
            bool debitResult = false;
            bool creditResult = false;
            var response = new Response<string>();
            try
            {
                con = new SqlConnection("connection string");
                con.Open();

                // lets begin a transaction here
                transaction = con.BeginTransaction();
                
                foreach (var query in querys)
                {
                    //using (SqlCommand cmd = con.CreateCommand())
                    using (IDbCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = query;
                        cmd.Transaction = transaction;
                    }
                }
                //// Let us do a debit first
                //using (SqlCommand cmd = con.CreateCommand())
                //{
                //    cmd.CommandType = CommandType.Text;
                //    //cmd.CommandText = string.Format(
                //    //    "update Account set Amount = Amount - {0} where ID = {1}",
                //    //    amount, debitAccountID);
                //    cmd.CommandText = querys.First();
                //    // assosiate this command with transaction
                //    cmd.Transaction = transaction;
                //}
                //using (SqlCommand command = con.CreateCommand())
                //{
                //    SqlDataReader reader = command.ExecuteReader();
                //    while (reader.Read())
                //    {
                //        int weight = reader.GetInt32(0);    // Weight int
                //        string name = reader.GetString(1);  // Name string
                //        string breed = reader.GetString(2); // Breed string

                //        reader.get;
                //    }
                //}

                transaction.Commit();
            }
            catch(Exception e)
            {
                transaction.Rollback();
                response.Succes = false;
                response.ExceptionList.Add(e);
                response.Data = null;
            }
            finally
            {
                con.Close();
            }
            return response;
        }
    }

    public interface ITransactionService
    {

    }
}