using System;
using System.Threading.Tasks;

namespace Sakani.DA.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }
        IOwnerRepository Owners { get; }
        IApartmentRepository Apartments { get; }
        IFeedbackRepository Feedbacks { get; }
        IRoomRepository Rooms { get; }
        IBedRepository Beds { get; }
        IBookingRepository Bookings { get; }

        Task<int> CompleteAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
} 