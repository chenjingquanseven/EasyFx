using System;

namespace EasyFx.Core.Domain.SeedWork
{
    public abstract class Entity:IKey
    {
        int? _requestedHashCode;
        string _Id;
        public virtual string Id
        {
            get
            {
                return _Id;
            }
             set
            {
                _Id = value;
            }
        }


        public bool IsTransient()
        {
            return string.IsNullOrWhiteSpace(this.Id);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; 

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }
        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }


    public abstract class HasCreatorEntity : Entity, IHasCreator
    {
        public  string CreatorId { get; set; }
        public  string CreatorName { get; set; }
        public  DateTime CreationTime { get; set; }
    }

    public abstract class HasModifierEntity : Entity, IHasModifier
    {
        public  string ModifierId { get; set; }
        public  string ModifierName { get; set; }
        public  DateTime? LastModificationTime { get; set; }
    }


    public abstract class HasDeletorEntity : Entity, IHasDeletor
    {
        public  string DeletorId { get; set; }
        public  string DeletorName { get; set; }
        public  bool IsDeleted { get; set; }
        public  DateTime? DeleteTime { get; set; }
    }

    public abstract class HasTrackingEntity : Entity, IHasCreator, IHasModifier
    {
        public DateTime CreationTime { get; set; }
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string ModifierId { get; set; }
        public string ModifierName { get; set; }
    }
    public abstract class FullEntity :Entity, IHasCreator, IHasModifier, IHasDeletor
    {
        public  string CreatorId { get; set; }
        public  string CreatorName { get; set; }
        public  DateTime CreationTime { get; set; }
        public  string ModifierId { get; set; }
        public  string ModifierName { get; set; }
        public  DateTime? LastModificationTime { get; set; }
        public  string DeletorId { get; set; }
        public  string DeletorName { get; set; }
        public  bool IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
    }
}
