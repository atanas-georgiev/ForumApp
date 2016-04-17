namespace ForumApp.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Linq;

    using ForumApp.Data.Repositories;

    public class RepositoryMock<T> : IRepository<T>
        where T : class
    {
        private List<T> data;

        public RepositoryMock()
        {
            this.data = new List<T>();
            this.ReadingsCount = 0;
        }

        public int ReadingsCount { get; private set; }

        public void Add(T entity)
        {
            this.data.Add(entity);
        }

        public IQueryable<T> All()
        {
            this.ReadingsCount++;
            return this.data.AsQueryable();
        }

        public T Attach(T entity)
        {
            return null;
        }

        public void Delete(object id)
        {
            return;
        }

        public void Delete(T entity)
        {
            return;
        }

        public void Detach(T entity)
        {
            return;
        }

        public void Dispose()
        {
            return;
        }

        public T GetById(object id)
        {
            return this.data.FirstOrDefault();
        }

        public int SaveChanges()
        {
            return 0;
        }

        public void Update(T entity)
        {
            return;
        }
    }
}