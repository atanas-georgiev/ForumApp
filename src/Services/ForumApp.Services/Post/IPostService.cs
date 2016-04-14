﻿namespace ForumApp.Services.Forum
{
    using System.Linq;

    using ForumApp.Data.Models;

    public interface IPostService
    {
        IQueryable<Post> GetAll();
    }
}