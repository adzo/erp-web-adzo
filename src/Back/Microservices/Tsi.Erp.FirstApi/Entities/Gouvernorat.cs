using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tsi.Erp.FirstApi.Entities;

public partial class Gouvernorat
{
    [Column("LibelleFR")]
    [StringLength(50)]
    public string LibelleFr { get; set; } = null!;

    [Key]
    [Column("UID")]
    public Guid Uid { get; set; }

    public int Code { get; set; }

    [Column("LibelleAR")]
    [StringLength(50)]
    public string LibelleAr { get; set; } = null!;

    [StringLength(100)]
    public string? Observation { get; set; }

    [StringLength(100)]
    public string? Libelle { get; set; }

    [StringLength(50)]
    public string? CreationUser { get; set; }

    [StringLength(50)]
    public string? ModifUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateSaisie { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateModif { get; set; }

    public bool? Valid { get; set; }

    [InverseProperty("GouvernoratNavigation")]
    public virtual ICollection<Delegation> Delegations { get; } = new List<Delegation>();

    [InverseProperty("GouvernoratsNavigation")]
    public virtual ICollection<Ville> Villes { get; } = new List<Ville>();
}
