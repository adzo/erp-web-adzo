using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tsi.Erp.FirstApi.Entities;

[Table("Delegation")]
[Index("Gouvernorat", Name = "IX_FK_Gouvernorats_Delegation")]
public partial class Delegation
{
    [Key]
    [Column("UID")]
    public Guid Uid { get; set; }

    public int Code { get; set; }

    [StringLength(50)]
    public string Libelle { get; set; } = null!;

    [Column("LibelleFR")]
    [StringLength(50)]
    public string LibelleFr { get; set; } = null!;

    [Column("LibelleAR")]
    [StringLength(50)]
    public string LibelleAr { get; set; } = null!;

    [StringLength(50)]
    public string? Observation { get; set; }

    public Guid? Gouvernorat { get; set; }

    [StringLength(50)]
    public string? CreationUser { get; set; }

    [StringLength(50)]
    public string? ModifUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateSaisie { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateModif { get; set; }

    public bool? Valid { get; set; }

    [ForeignKey("Gouvernorat")]
    [InverseProperty("Delegations")]
    public virtual Gouvernorat? GouvernoratNavigation { get; set; }
}
