﻿namespace Lender.Slos.Dal
{
    using System;

    using Lender.Slos.Dao;
    using Lender.Slos.NHibernate;

    using global::NHibernate;

    public class IndividualDal : IRepository<IndividualEntity>
    {
        private readonly NHibernateRepository<IndividualEntity> repository;

        public IndividualDal(string connectionString)
            : this(NHibernateModule.OpenSession(connectionString))
        {
        }

        internal IndividualDal(ISession session)
        {
            repository = new NHibernateRepository<IndividualEntity>(session);
        }

        public int Create(IndividualEntity entity)
        {
            // Guard against invalid arguments.
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (entity.Id > 0)
            {
                throw new InvalidOperationException("Entity is invalid, the Id must be 0.");
            }

            try
            {
                // Returns createdId if a record was created.
                return this.repository.Create(entity);
            }
            catch (Exception exception)
            {
                // Throw an exception if the Id was not returned.
                throw new InvalidOperationException("Failed to create.", exception);
            }
        }

        public IndividualEntity Retrieve(int id)
        {
            // Guard against invalid arguments.
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException("id");
            }

            try
            {
                // Retrieve returns null if a record isn't found.
                return repository.Retrieve(id);
            }
            catch (Exception exception)
            {
                // Throw an exception if the Id was not returned.
                throw new InvalidOperationException("Failed to retrieve.", exception);
            }
        }

        public void Update(IndividualEntity entity)
        {
            // Guard against invalid arguments.
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (entity.Id <= 0)
            {
                throw new InvalidOperationException("Entity is invalid, the Id must be greater than 0.");
            }

            try
            {
                repository.Update(entity);
            }
            catch (Exception exception)
            {
                // Throw an exception if the Id was not returned.
                throw new InvalidOperationException("Failed to update.", exception);
            }
        }

        public void Delete(IndividualEntity entity)
        {
            // Guard against invalid arguments.
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (entity.Id <= 0)
            {
                throw new InvalidOperationException("Entity is invalid, the Id must be greater than 0.");
            }

            var entityToDelete = this.Retrieve(entity.Id);
            if (entityToDelete == null)
            {
                throw new InvalidOperationException("Entity is invalid, entity not found.");
            }

            try
            {
                repository.Delete(entityToDelete);
            }
            catch (Exception exception)
            {
                // Throw an exception if the Id was not returned.
                throw new InvalidOperationException("Failed to delete.", exception);
            }
        }
    }
}
