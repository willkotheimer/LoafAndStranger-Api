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
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO [Loaves] ([Size],[Type],[WeightInOunces],[Price],[Sliced])
                                OUTPUT inserted.Id
                                VALUES(@Size, @type, @weightInOunces, @Price, @Sliced)";
            cmd.Parameters.AddWithValue("Size", loaf.Size);
            cmd.Parameters.AddWithValue("type", loaf.Type);
            cmd.Parameters.AddWithValue("weightInOunces", loaf.WeightInOunces);
            cmd.Parameters.AddWithValue("Price", loaf.Price);
            cmd.Parameters.AddWithValue("Sliced", loaf.Sliced);

            var id = (int)cmd.ExecuteScalar();
            loaf.Id = id;
        }

        public Loaf Get(int id)
        {
            var sql = @"Select * From Loaves Where Id = @id";

            //Create a connection 
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            //create command
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("id", id);

            //Execute command
            var reader = command.ExecuteReader();
            if(reader.Read())
            {
                var loaf = MapLoaf(reader);
                return loaf;
            }

            return null;
        }

        public void Remove(int id) {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"Delete from Loaves where Id=@id";
            cmd.ExecuteNonQuery();
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
