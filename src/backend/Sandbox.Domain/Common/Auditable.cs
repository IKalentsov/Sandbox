namespace Sandbox.Domain.Common;

public class Auditable
{
    public Auditable(Guid id, DateTime modified, DateTime created)
    {
        Id = id;
        Modified = modified;
        Created = created;
    }

    public Guid Id { get; }
    public DateTime Created { get; }
    public DateTime Modified { get; }
}
