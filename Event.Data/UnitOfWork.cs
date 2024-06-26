﻿using Event.Data.Models;
using Event.Data.Repository;
using System.Runtime.InteropServices;

namespace Event.Data
{
    public class UnitOfWork
    {
        private readonly Net1704_221_3_EventContext _context;
        private readonly PaymentRepository _payment;
        private readonly RegistrationRepository _registration;
        private readonly TicketRepository _ticket;
        private readonly CustomerRepository _customer;
        private readonly EventRepository _event;

        public UnitOfWork()
        {
            _context ??= new Net1704_221_3_EventContext();
        }
        public PaymentRepository PaymentRepository
        {
            get
            {
                return _payment ?? new PaymentRepository(_context);
            }
        }
        public CustomerRepository CustomerRepository
        {
            get
            {
                return _customer ?? new CustomerRepository(_context);
            }
        }
        public RegistrationRepository RegistrationRepository
        {
            get
            {
                return _registration ?? new RegistrationRepository(_context);
            }
        }
        public TicketRepository TicketRepository
        {
            get
            {
                return _ticket ?? new TicketRepository(_context);
            }
        }
        public EventRepository EventRepository
        {
            get
            {
                return _event ?? new EventRepository(_context);
            }
        }

        ////TO-DO CODE HERE/////////////////

        #region Set transaction isolation levels

        /*
        Read Uncommitted: The lowest level of isolation, allows transactions to read uncommitted data from other transactions. This can lead to dirty reads and other issues.

        Read Committed: Transactions can only read data that has been committed by other transactions. This level avoids dirty reads but can still experience other isolation problems.

        Repeatable Read: Transactions can only read data that was committed before their execution, and all reads are repeatable. This prevents dirty reads and non-repeatable reads, but may still experience phantom reads.

        Serializable: The highest level of isolation, ensuring that transactions are completely isolated from one another. This can lead to increased lock contention, potentially hurting performance.

        Snapshot: This isolation level uses row versioning to avoid locks, providing consistency without impeding concurrency. 
         */

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        #endregion
    }
}