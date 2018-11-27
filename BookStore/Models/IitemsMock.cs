using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
   public interface IitemsMock
    {
        IQueryable<item> items { get; }

        item Save(item item);

        void delete(item item);

    }
}
