using System;

public abstract class BaseEntity {
    public virtual Guid Id { get; protected set; }
}