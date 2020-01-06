using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Type;
using NHibernate.UserTypes;

namespace ServerApp.Infrastructure.Persistence
{
    public class DateTimeOffsetUserType : ICompositeUserType
    {
        public string[] PropertyNames => new[] {"LocalTicks", "Offset"};

        public IType[] PropertyTypes => new IType[] {NHibernateUtil.Ticks, NHibernateUtil.TimeSpan};

        public object GetPropertyValue(object component, int property)
        {
            var dto = (DateTimeOffset) component;

            switch (property)
            {
                case 0:
                    return dto.UtcTicks;
                case 1:
                    return dto.Offset;
                default:
                    throw new NotImplementedException();
            }
        }

        public void SetPropertyValue(object component, int property, object value)
        {
            throw new NotImplementedException();
        }

        public Type ReturnedClass => typeof(DateTimeOffset);

        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, null) && ReferenceEquals(y, null))
                return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return x.Equals(y);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public object NullSafeGet(IDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            return NullSafeGet(dr as DbDataReader, names, session, owner);
        }

        public object NullSafeGet(DbDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            if (dr.IsDBNull(dr.GetOrdinal(names[0])))
            {
                return null;
            }

            var dateTime = (DateTime) NHibernateUtil.Ticks.NullSafeGet(dr, names[0], session, owner);
            var offset = (TimeSpan) NHibernateUtil.TimeSpan.NullSafeGet(dr, names[1], session, owner);

            return new DateTimeOffset(dateTime, offset);
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            object utcTicks = null;
            object offset = null;

            if (value != null)
            {
                utcTicks = ((DateTimeOffset) value).DateTime;
                offset = ((DateTimeOffset) value).Offset;
            }

            NHibernateUtil.Ticks.NullSafeSet(cmd, utcTicks, index++, session);
            NHibernateUtil.TimeSpan.NullSafeSet(cmd, offset, index, session);
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
        {
            if (settable.All(t => t))
                NullSafeSet(cmd, value, index, session);
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public bool IsMutable => false;

        public object Disassemble(object value, ISessionImplementor session) => value;

        public object Assemble(object cached, ISessionImplementor session, object owner) => cached;

        public object Replace(object original, object target, ISessionImplementor session, object owner) => original;
    }

    public class DateTimeOffsetTypeConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Type == typeof(DateTimeOffset));
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.CustomType<DateTimeOffsetUserType>();
        }
    }
}