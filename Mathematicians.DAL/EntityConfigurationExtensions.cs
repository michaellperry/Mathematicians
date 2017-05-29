using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq.Expressions;

namespace Mathematicians.DAL
{
    static class EntityConfigurationExtensions
    {
        public static void HasUniqueIndex<TEntity, TProperty>(this EntityTypeConfiguration<TEntity> entityConfiguration, Expression<Func<TEntity, TProperty>> propertyExpression)
            where TEntity : class
            where TProperty : struct
        {
            var configuration = entityConfiguration
                .Property(propertyExpression);
            string tableName = typeof(TEntity).Name;
            var body = propertyExpression.Body as MemberExpression;
            if (body != null)
            {
                string propertyName = body.Member.Name;
                configuration
                    .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(
                        new IndexAttribute($"UX_{tableName}_{propertyName}") { IsUnique = true }));
            }
        }
    }
}
