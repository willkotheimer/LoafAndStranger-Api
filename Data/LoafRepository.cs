using System;
using System.Collections.Generic;
using System.Linq;
using LoafAndStranger.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace LoafAndStranger.Data
{
    public class LoafRepository
    { 
        const string ConnectionString = "Server=localhost; Database=LoafAndStranger; Trusted_Connection=True";

        public List<Loaf> GetAll()
        {
            // Loaf array
            var _loaves = new List<Loaf>();

            using var db = new SqlConnection(ConnectionString);

            var sql = @"Select * From Loaves";

            var results = db.Query<Loaf>(sql).ToList();

            return results;

        }

        public void Add(Loaf loaf)
        {
            using var db = new SqlConnection(ConnectionString);
            
            var sql = @"INSERT INTO [Loaves] ([Size],[Type],[WeightInOunces],[Price],[Sliced])
                                OUTPUT inserted.Id
                                VALUES(@Size, @type, @weightInOunces, @Price, @Sliced)";
            var id = db.ExecuteScalar<int>(sql, loaf);

            loaf.Id = id;
        }

        public Loaf Get(int id)
        {
            var sql = @"Select * 
                        From Loaves 
                        Where Id = @id";

            //Create a connection 
            using var db = new SqlConnection(ConnectionString);

            var loaf = db.QueryFirstOrDefault<Loaf>(sql, new { id = id });
            return loaf;
        }

        public void Remove(int id) {
            using var db = new SqlConnection(ConnectionString);
           
            var sql = @"Delete from Loaves where Id=@id";
            db.Execute(sql, new { id });
        }

        Loaf MapLoaf(SqlDataReader reader)
        {
            // get each column value from the reader
            var id = (int)reader["Id"]; //explicit cast (throws exception)
            var size = (LoafSize)reader["Size"];
            var type = reader["Type"] as string; // implicit cast (returns null)
            var price = (decimal)reader["price"];
            var weightInOunces = (int)reader["weightInOunces"];
            var sliced = (bool)reader["sliced"];
            var createdDate = (DateTime)reader["createdDate"];

            var loaf = new Loaf
            {
                Id = id,
                Price = price,
                Size = size,
                Sliced = sliced,
                Type = type,
                WeightInOunces = weightInOunces
            };

            return loaf;

        }

    }
}
