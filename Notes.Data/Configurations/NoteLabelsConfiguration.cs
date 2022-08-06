using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Core.Entities;

namespace Notes.Data.Configurations;

public sealed class NoteLabelsConfiguration : IEntityTypeConfiguration<NoteLabel>
{
    public void Configure(EntityTypeBuilder<NoteLabel> builder)
    {
        builder.HasKey(n => new
        {
            n.LabelId,
            n.NoteId
        });
    }
}