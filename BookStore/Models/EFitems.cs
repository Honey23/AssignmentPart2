using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class EFitems : IitemsMock
    {
        private HoneyBookStorePart db = new HoneyBookStorePart();
        public IQueryable<item> items { get { return db.items; } }

        public void delete(item item)
        {
            db.items.Remove(item);
            db.SaveChanges();
        }

        public item Save(item item)
        {
            if (item.item_id ==0)
            {
                //insert
                db.items.Add(item);

            }
            else
            {
                //update
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();
            return item;
        }
    }
}