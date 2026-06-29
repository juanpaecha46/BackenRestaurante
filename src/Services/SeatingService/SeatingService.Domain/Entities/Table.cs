using SeatingService.Domain.Enums;
using SeatingService.Domain.Exceptions;

namespace SeatingService.Domain.Entities;

public class Table
{
    public Guid Id { get; private set; }
    public string Number { get; private set; } = null!;
    public int Capacity { get; private set; }
    public TableStatus Status { get; private set; }
    public string? Location { get; private set; }
    public Guid? CurrentOrderId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Table() { }

    public static Table Create(string number, int capacity, string? location)
    {
        return new Table
        {
            Id = Guid.NewGuid(),
            Number = number,
            Capacity = capacity,
            Status = TableStatus.Available,
            Location = location,
            CurrentOrderId = null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string number, int capacity, string? location)
    {
        Number = number;
        Capacity = capacity;
        Location = location;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignOrder(Guid orderId)
    {
        if (Status != TableStatus.Available && Status != TableStatus.Reserved)
            throw new InvalidTableStatusTransitionException(Status, TableStatus.Occupied);

        CurrentOrderId = orderId;
        Status = TableStatus.Occupied;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Release()
    {
        if (Status != TableStatus.Occupied)
            throw new InvalidTableStatusTransitionException(Status, TableStatus.Available);

        CurrentOrderId = null;
        Status = TableStatus.Available;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(TableStatus newStatus)
    {
        ValidateTransition(newStatus);
        Status = newStatus;
        if (newStatus == TableStatus.Available)
            CurrentOrderId = null;
        UpdatedAt = DateTime.UtcNow;
    }

    private void ValidateTransition(TableStatus newStatus)
    {
        bool valid = Status switch
        {
            TableStatus.Available => newStatus is TableStatus.Reserved or TableStatus.OutOfService,
            TableStatus.Reserved => newStatus is TableStatus.Available or TableStatus.Occupied,
            TableStatus.Occupied => newStatus is TableStatus.Available,
            TableStatus.OutOfService => newStatus is TableStatus.Available,
            _ => false
        };

        if (!valid)
            throw new InvalidTableStatusTransitionException(Status, newStatus);
    }
}
