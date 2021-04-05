using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoafAndStranger.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace LoafAndStranger.Data
{
    public class TopsRepository
    {
        const string ConnectionString = "Server=localhost; Database=LoafAndStranger; Trusted_Connection=True";

        public List<Top> GetAll()
        {
            using var db = new SqlConnection(ConnectionString);
            var sql = "SELECT * FROM Tops";
            var tops = db.Query<Top>(sql).ToList();
            return tops;
        }
    }
}
