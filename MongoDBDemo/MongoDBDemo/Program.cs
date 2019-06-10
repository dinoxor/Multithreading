using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace MongoDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new MongoCRUD("AddressBook");

            var person = new Person
            {
                FirstName = "Joe",
                LastName = "Scott",
                PrimaryAdress = new Address
                {
                    StreetAddress = "1234 Cherry Ln.",
                    City = "Scranton",
                    State = "PA",
                    ZipCode = "12345"
                }
            };

            //db.Insert("Users", person);

            var records = db.Load<Person>("Users");

            foreach(var record in records)
            {
                Console.WriteLine($"{record.Id}: {record.FirstName} {record.LastName}");

                

            }

            Console.ReadLine();
        }
    }

    public class Person
    {
        [BsonId] // _id
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address PrimaryAdress { get; set; }
       
    }

    public class Address
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    public class MongoCRUD
    {
        private IMongoDatabase db;

        public MongoCRUD (string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void Insert<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public List<T> Load<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }
    }
}
