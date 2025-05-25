using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sakani.DA.Data;
using Sakani.DA.Interfaces;
using System;
using System.Threading.Tasks;

namespace Sakani.DA.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SakaniDbContext _context;
        private IDbContextTransaction _transaction;
        private bool _disposed;

        public IStudentRepository Students { get; }
        public IOwnerRepository Owners { get; }
        public IApartmentRepository Apartments { get; }
        public IFeedbackRepository Feedbacks { get; }
        public IRoomRepository Rooms { get; }
        public IBedRepository Beds { get; }
        public IBookingRepository Bookings { get; }

        public UnitOfWork(
            SakaniDbContext context,
            IStudentRepository studentRepository,
            IOwnerRepository ownerRepository,
            IApartmentRepository apartmentRepository,
            IFeedbackRepository feedbackRepository,
            IRoomRepository roomRepository,
            IBedRepository bedRepository,
            IBookingRepository bookingRepository)
        {
            _context = context;
            Students = studentRepository;
            Owners = ownerRepository;
            Apartments = apartmentRepository;
            Feedbacks = feedbackRepository;
            Rooms = roomRepository;
            Beds = bedRepository;
            Bookings = bookingRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
                if (_transaction != null)
                {
                    _transaction.Dispose();
                }
            }
            _disposed = true;
        }
    }
} 