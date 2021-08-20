namespace Platform.DatabaseHandlers.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Platform.Models;
    using Rokono_Control.Models;

    public class NotesContext : IDisposable
    {
        RokonocontrolContext Context;
        IConfiguration Configuration;
        private bool disposedValue;

        public NotesContext(RokonocontrolContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        internal List<UserNotes> GetUserNotes(int id, int projectId)
        {
            if(projectId == 0)
                return Context.AssociatedAccountNotes.Include(x => x.Note)
                                                     .Where(x => x.UserAccountId == id)
                                                     .Select(x => x.Note)
                                                     .ToList();
            else
                return Context.AssociatedAccountNotes.Include(x => x.Note)
                                                     .Where(x => x.UserAccountId == id && x.ProjectId == projectId)
                                                     .Select(x => x.Note)
                                                     .ToList();

        }


        internal void AddNewUserNote(IncomingNoteRequest note, int id)
        {
            var currentNote = Context.UserNotes.Add(new UserNotes{
                Content = note.Content,
                DateOfMessage = DateTime.Now,
                NoteBackground = note.Background,
                NoteForeground = note.FontColor
            });
            Context.SaveChanges();
            Context.AssociatedAccountNotes.Add(new AssociatedAccountNotes{
                NoteId = currentNote.Entity.Id,
                ProjectId = note.ProjectId,
                UserAccountId = id
            });
            Context.SaveChanges();
        }

        internal void ChangeNotePosition(IncomingNoteRequest note)
        {
            var currentNote = Context.UserNotes.FirstOrDefault(x=>x.Id == note.NoteId);
            currentNote.TopPos = note.Top;
            currentNote.LeftPos = note.Left;
            Context.Attach(currentNote);
            Context.Update(currentNote);
            Context.SaveChanges();
        }

        internal void DeleteNote(IncomingNoteRequest note)
        {
            var currentNote = Context.UserNotes.FirstOrDefault(x=>x.Id == note.NoteId);
            Context.UserNotes.Remove(currentNote);
            var associatedNotes = Context.AssociatedAccountNotes.Where(x=>x.NoteId == note.NoteId).ToList();
            Context.AssociatedAccountNotes.RemoveRange(associatedNotes);
            Context.SaveChanges();
        }



        internal void EditNote(IncomingNoteRequest note)
        {
            var currentNote = Context.UserNotes.FirstOrDefault(x=>x.Id == note.NoteId);
            currentNote.NoteBackground = note.Background;
            currentNote.NoteForeground = note.FontColor;
            currentNote.Content = note.Content;
            Context.Attach(currentNote);
            Context.Update(currentNote);
            Context.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~NotesContext()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}