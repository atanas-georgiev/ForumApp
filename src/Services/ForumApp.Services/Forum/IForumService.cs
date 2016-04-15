﻿namespace ForumApp.Services.Forum
{
    using System.Linq;

    using ForumApp.Data.Models;

    public interface IForumService
    {
        IQueryable<Forum> GetByPage(int page);

        int GetAllPagesCount();
    }
}
