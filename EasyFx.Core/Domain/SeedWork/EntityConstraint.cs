using System;
using System.ComponentModel.DataAnnotations;

namespace EasyFx.Core.Domain.SeedWork
{
    public interface IKey
    {
        [Key]
        string Id { get; set; }
    }
    public interface IHasCreationTime
    {
        DateTime CreationTime { get; set; }
    }

    public interface IHasCreator:IHasCreationTime
    {
        string CreatorId { get; set; }

        string CreatorName { get; set; }
    }

    public interface IHasModificationTime
    {
        DateTime? LastModificationTime { get; set; }
    }

    public interface IHasModifier: IHasModificationTime
    {
        string ModifierId { get; set; }

        string ModifierName { get; set; }
    }

    public interface IHasDeletionTime
    {
        bool IsDeleted { get; set; }
        DateTime? DeleteTime { get; set; }
    }

    public interface IHasDeletor: IHasDeletionTime
    {
        string DeletorId { get; set; }

        string DeletorName { get; set; }
    }
}
