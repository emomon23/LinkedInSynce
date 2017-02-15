using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkInContactManagement.Model;

namespace LinkInContactManagement.Repository
{
    public class ContactRepository
    {
        private ContactContext _context = new ContactContext();
        
        public void Update_IsStillLinkedInContactToFalse(DateTime backDate)
        {
            var sql = "UPDATE Contacts SET IsStillLinkedInContact = 0 WHERE LastUpdateDate < @p0";
            _context.Database.ExecuteSqlCommand(sql, backDate);
        }

        public void AddOrUpdate_IsStillLinkedInContactToTrue(Contact contact)
        {
            var dbContact =
                _context.Contacts.FirstOrDefault(c => c.FirstName == contact.FirstName && c.LastName == contact.LastName);

            if (dbContact == null)
            {
                dbContact = contact;
                _context.Contacts.Add(dbContact);
            }

            dbContact.IsStillLinkedInContact = true;
            dbContact.LastUpdateDate = DateTime.Now;

            _context.SaveChanges();
        }

        public void UpdateLastAutoContact(Contact c)
        {
            c.LastAutoContacted = DateTime.Now;
            c.LastReachOutMethod = "Linked In Auto Contact";
            c.LastUpdateDate = DateTime.Now;

            UpdateContact(c);

        }
        public List<Contact> GetCurrentLinkedInContacts()
        {
            return _context.Contacts.Where(c => c.IsStillLinkedInContact == true).OrderBy(c => c.FirstName).ToList();
        }

        public void UpdateContact(Contact contact)
        {
            var edit = _context.Contacts.Single(c => c.ContactId == contact.ContactId);
            edit.LastName = contact.LastName;
            edit.FirstName = contact.FirstName;
            edit.IsStillLinkedInContact = contact.IsStillLinkedInContact;
            edit.LastAutoContacted = contact.LastAutoContacted;
            edit.LastUpdateDate = contact.LastUpdateDate;
            edit.RawLinkedInName = contact.RawLinkedInName;
            edit.ShouldSkipAutoReachOut = contact.ShouldSkipAutoReachOut;
            edit.Title = contact.Title;
            edit.Email = contact.Email;
            edit.LastReachOutMethod = contact.LastReachOutMethod;
            edit.Notes = contact.Notes;
            edit.Phone = contact.Phone;
            edit.ScheduleNextReachOut = contact.ScheduleNextReachOut;
            edit.LastReachedOut = contact.LastReachedOut;

            _context.SaveChanges();
        }

        public bool AllContactsAreCurrent()
        {
            var backDate = DateTime.Now.AddDays(-1);

            return _context.Contacts.Any(c => c.LastUpdateDate < backDate);
        }
    }

    public class ContactContext : DbContext
    {
        public ContactContext() : base("PrimaryDBConnectionString") { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ToDo> ToDoList { get; set; }
    }
}
