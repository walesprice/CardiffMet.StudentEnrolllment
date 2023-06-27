using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CardiffMetroUni.StudentEnrollment.Core.Contracts
{
    public interface IRepository<T> where T : Entity
    {

        Task<Result<IReadOnlyList<T>>> FindAll();

        Task<Result<T>> FindById(int id);

        Task<Result<IReadOnlyList<T>>> FindAllBy(Expression<Func<T, bool>> query);
        Task<Result<T>> FindSingleBy(Expression<Func<T, bool>> query);
    }
}
