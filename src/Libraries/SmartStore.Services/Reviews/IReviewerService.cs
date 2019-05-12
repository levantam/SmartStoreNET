using SmartStore.Core.Domain.Reviews;
using System.Collections.Generic;

namespace SmartStore.Services.Reviews
{
    public interface IReviewerService
    {
        IList<Reviewer> TopReviewers(int page, int size, bool isRandom);
        Reviewer GetById(int id);
        IList<Reviewer> GetByCategory(int category, int page, int size);
    }
}
