﻿using LiteDB;
using RepositoryCore.Interfaces;

namespace Service.Entity.Models
{
    public class Project:IEntity<string>
    {
        [BsonId]
        public string Id { get; set; }
        [BsonIndex]
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string ImageUrl { get; set; }
         
    }

    
}
