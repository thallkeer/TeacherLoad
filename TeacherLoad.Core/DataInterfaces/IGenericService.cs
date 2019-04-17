using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TeacherLoad.Core.DataInterfaces
{
    public interface IGenericService<TEntity>
    {
        /// <summary>
        /// Получить объект по указанным критериям
        /// </summary>
        /// <param name="filter">Критерии поиска</param>
        /// <param name="orderBy">Направление сортировки</param>
        /// <param name="includeProperties">Навигационные свойства</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        /// <summary>
        /// Получить все сущности, при этом подгружая 
        /// необходимые навигационные свойства
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();
        /// <summary>
        /// Получить объект по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetByID(object id);
        /// <summary>
        /// Добавить объект в базу
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);
        /// <summary>
        /// Удалить объект по идентификатору в базе
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);
        /// <summary>
        /// Удалить объект
        /// </summary>
        /// <param name="entityToDelete"></param>
        void Delete(TEntity entityToDelete);
        /// <summary>
        /// Обновить объект
        /// </summary>
        /// <param name="entityToUpdate"></param>
        void Update(TEntity entityToUpdate);        
    }
}