using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartStore.Core.Data;
using SmartStore.Core.Domain.Reviews;

namespace SmartStore.Services.Reviews
{
    public partial class ReviewerService : IReviewerService
    {
        private readonly IRepository<Reviewer> _reviewerRepository;
        public ReviewerService(
            IRepository<Reviewer> reviewerRepository
            )
        {
            _reviewerRepository = reviewerRepository;
        }
        public IList<Reviewer> GetByCategory(int category, int page, int size)
        {
            throw new NotImplementedException();
        }

        public Reviewer GetById(int id)
        {
            return this._reviewerRepository.GetById(id);
        }

        public  IList<Reviewer> TopReviewers(int page = 1, int size = 5, bool isRandom = false)
        {
            var offset = (page - 1) * size;
            return  _reviewerRepository.Table
                .Where(t => t.IsApproved == true)
                .OrderByDescending(r => r.Rating)
                .Skip(offset).Take(size)
                .ToList();
        }
    }
}
