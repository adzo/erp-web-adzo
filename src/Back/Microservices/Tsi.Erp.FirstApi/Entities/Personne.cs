using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tsi.Erp.FirstApi.Entities;

[Table("Personne")]
[Index("AgenceEm", Name = "IX_FK_AGence_FM")]
[Index("Annee", Name = "IX_FK_AnneesPersonne")]
[Index("BanqueEm", Name = "IX_FK_Banq_Pers")]
[Index("ModeleComptabilistationPaie", Name = "IX_FK_ModeleComptabilistationPaie_Personne")]
[Index("CategorieSocioProfessionnelle", Name = "IX_FK_Pers_CateSocioProf")]
[Index("NiveauInstruction", Name = "IX_FK_Pers_NivInstruction")]
[Index("SectionAnalytique", Name = "IX_FK_Pers_SectAnalyti")]
[Index("CategoriePersonnel", Name = "IX_FK_Persersonnel")]
[Index("Activite", Name = "IX_FK_Personne_Activite")]
[Index("Affectation", Name = "IX_FK_Personne_Affectation")]
[Index("Agence", Name = "IX_FK_Personne_Agence")]
[Index("Banque", Name = "IX_FK_Personne_Banque")]
[Index("BesoinRecrutements", Name = "IX_FK_Personne_BesoinRecrutements")]
[Index("Ribem", Name = "IX_FK_Personne_CompteBancaire")]
[Index("Diplome", Name = "IX_FK_Personne_Diplome")]
[Index("Echelle", Name = "IX_FK_Personne_Echelle")]
[Index("Echelon", Name = "IX_FK_Personne_Echelon")]
[Index("Emploi", Name = "IX_FK_Personne_Emploi")]
[Index("Fonction", Name = "IX_FK_Personne_Fonction")]
[Index("Grade", Name = "IX_FK_Personne_Grade")]
[Index("ModePaiement", Name = "IX_FK_Personne_ModePaie")]
[Index("Modele", Name = "IX_FK_Personne_Modele")]
[Index("PositionPersonne", Name = "IX_FK_Personne_Position")]
[Index("Specialite", Name = "IX_FK_Personne_Specialite")]
[Index("Statut", Name = "IX_FK_Personne_Statut")]
[Index("SousPosition", Name = "IX_FK_SousPosition_Personne")]
public partial class Personne
{
    [StringLength(1)]
    public string? RegimeTravail { get; set; }

    [Key]
    [Column("UID")]
    public Guid Uid { get; set; }

    [StringLength(1)]
    public string EtatCivil { get; set; } = null!;

    [Column(TypeName = "image")]
    public byte[]? Photo { get; set; }

    public double? RetenuesEffectuees { get; set; }

    public int? NombreMoisPayes { get; set; }

    public int? NombrePaies { get; set; }

    public bool? MaladieChronique { get; set; }

    public bool? MaladieChroniqueConjoint { get; set; }

    [StringLength(20)]
    public string? TelephonePersonne { get; set; }

    public Guid? Grade { get; set; }

    [StringLength(1)]
    public string? TypeCalculImpots { get; set; }

    [StringLength(1)]
    public string Sexe { get; set; } = null!;

    public int? RegimeHoraire { get; set; }

    public Guid? TypeGrille { get; set; }

    public Guid? Fonction { get; set; }

    public double? ImpotDu { get; set; }

    public double? FraisProf { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateTitularisation { get; set; }

    [StringLength(50)]
    public string? NomCompletPersonne { get; set; }

    [Column("NomCompletPersonneFR")]
    [StringLength(50)]
    public string NomCompletPersonneFr { get; set; } = null!;

    [Column("NomCompletPersonneAR")]
    [StringLength(50)]
    public string? NomCompletPersonneAr { get; set; }

    [Column(TypeName = "money")]
    public decimal? Arrondi { get; set; }

    [StringLength(20)]
    public string? NumeroCompte { get; set; }

    public int? NbreEnfantAbatt { get; set; }

    [StringLength(1)]
    public string? TypeSalaire { get; set; }

    public Guid? MotifCessation { get; set; }

    [Column("CINLieu")]
    [StringLength(20)]
    public string? Cinlieu { get; set; }

    [Column("CINLieuFR")]
    [StringLength(20)]
    public string? CinlieuFr { get; set; }

    [Column("CINLieuAR")]
    [StringLength(20)]
    public string? CinlieuAr { get; set; }

    public Guid Statut { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PasseportDate { get; set; }

    [StringLength(10)]
    public string? NumeroBadge { get; set; }

    [StringLength(50)]
    public string Matricule { get; set; } = null!;

    [StringLength(50)]
    public string? NomConjoint { get; set; }

    [Column("NomConjointFR")]
    [StringLength(50)]
    public string? NomConjointFr { get; set; }

    [Column("NomConjointAR")]
    [StringLength(50)]
    public string? NomConjointAr { get; set; }

    [Column("CINNumero")]
    [StringLength(8)]
    public string? Cinnumero { get; set; }

    public double? ImposableNet { get; set; }

    public Guid Affectation { get; set; }

    public Guid? Agence { get; set; }

    [StringLength(10)]
    public string? PasseportNumero { get; set; }

    public double? Abattements { get; set; }

    public Guid? Calendrier { get; set; }

    public int? NombreEnfants { get; set; }

    [StringLength(1)]
    public string? Salaire { get; set; }

    public double? RevenuReinvesti { get; set; }

    public Guid? Echelle { get; set; }

    public double? ImposableBrut { get; set; }

    [StringLength(30)]
    public string? ProfessionConjoint { get; set; }

    [Column("ProfessionConjointAR")]
    [StringLength(30)]
    public string? ProfessionConjointAr { get; set; }

    [Column("ProfessionConjointFR")]
    [StringLength(30)]
    public string? ProfessionConjointFr { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateEntree { get; set; }

    [Column("CINDate", TypeName = "datetime")]
    public DateTime? Cindate { get; set; }

    public Guid? CategorieSocioProfessionnelle { get; set; }

    public double? SoldeInitialConge { get; set; }

    public Guid? CategoriePersonnel { get; set; }

    public Guid? Modele { get; set; }

    public Guid? Echelon { get; set; }

    [StringLength(30)]
    public string? Nationalite { get; set; }

    [Column("NationaliteFR")]
    [StringLength(30)]
    public string? NationaliteFr { get; set; }

    [Column("NationaliteAR")]
    [StringLength(30)]
    public string? NationaliteAr { get; set; }

    public bool? SalaireUnique { get; set; }

    [StringLength(200)]
    public string? Adresse { get; set; }

    [Column("AdresseAR")]
    [StringLength(200)]
    public string? AdresseAr { get; set; }

    [Column("AdresseFR")]
    [StringLength(200)]
    public string AdresseFr { get; set; } = null!;

    [StringLength(30)]
    public string? Prenom { get; set; }

    public int? Indice { get; set; }

    [StringLength(20)]
    public string? PasseportLieu { get; set; }

    [Column("PasseportLieuAR")]
    [StringLength(20)]
    public string? PasseportLieuAr { get; set; }

    [Column("PasseportLieuFR")]
    [StringLength(20)]
    public string? PasseportLieuFr { get; set; }

    public Guid? Banque { get; set; }

    public Guid Emploi { get; set; }

    public bool? ChefFamille { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateSortie { get; set; }

    [StringLength(25)]
    public string? NomJeuneFille { get; set; }

    public Guid ModePaiement { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateNaissance { get; set; }

    public double? SalaireDeBase { get; set; }

    public Guid Activite { get; set; }

    public Guid? PositionPersonne { get; set; }

    public bool? AllocationFamiliale { get; set; }

    public Guid? Diplome { get; set; }

    [StringLength(30)]
    public string? Nom { get; set; }

    public double? NombreJoursBonus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PasseportDateExpiration { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateMariage { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDevorcee { get; set; }

    [StringLength(50)]
    public string? NomJeuneFilleConjoint { get; set; }

    [StringLength(50)]
    public string LieuNaissance { get; set; } = null!;

    [Column("LieuNaissanceFR")]
    [StringLength(50)]
    public string LieuNaissanceFr { get; set; } = null!;

    [Column("LieuNaissanceAR")]
    [StringLength(50)]
    public string? LieuNaissanceAr { get; set; }

    [StringLength(50)]
    public string? VilleResidence { get; set; }

    [Column("VilleResidenceAR")]
    [StringLength(50)]
    public string? VilleResidenceAr { get; set; }

    [Column("VilleResidenceFR")]
    [StringLength(50)]
    public string? VilleResidenceFr { get; set; }

    public int CodePostal { get; set; }

    public bool? AdhesionSyndicat { get; set; }

    [StringLength(50)]
    public string? NumAdhesionSyndicat { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateAdhesionSyndicat { get; set; }

    public bool? AdhesionAmical { get; set; }

    [StringLength(50)]
    public string? NumAdhesionAmical { get; set; }

    public int? NbAnneeCotisation { get; set; }

    public bool? BenificeTransport { get; set; }

    public bool? BenificiaireHabillement { get; set; }

    public int? NbrMission { get; set; }

    public Guid? NiveauInstruction { get; set; }

    [Column("MatriculeCNSS")]
    [StringLength(50)]
    public string? MatriculeCnss { get; set; }

    [StringLength(50)]
    public string? MotdePasse { get; set; }

    [StringLength(50)]
    public string? PrenomConjoint { get; set; }

    [Column("PrenomConjointAR")]
    [StringLength(50)]
    public string? PrenomConjointAr { get; set; }

    [Column("PrenomConjointFR")]
    [StringLength(50)]
    public string? PrenomConjointFr { get; set; }

    [Column("CodeCNRPS")]
    [StringLength(50)]
    public string? CodeCnrps { get; set; }

    [Column("DateAffectationCNRPS", TypeName = "datetime")]
    public DateTime? DateAffectationCnrps { get; set; }

    [StringLength(100)]
    public string? AutreOrganismeSociaux { get; set; }

    [Column("AutreOrganismeSociauxAR")]
    [StringLength(100)]
    public string? AutreOrganismeSociauxAr { get; set; }

    [Column("AutreOrganismeSociauxFR")]
    [StringLength(100)]
    public string? AutreOrganismeSociauxFr { get; set; }

    [StringLength(50)]
    public string? MatriculeAutreOrganismeSociaux { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateAmicale { get; set; }

    [StringLength(100)]
    public string? OrganismeEmployeurConjoint { get; set; }

    [Column("OrganismeEmployeurConjointAR")]
    [StringLength(100)]
    public string? OrganismeEmployeurConjointAr { get; set; }

    [Column("OrganismeEmployeurConjointFR")]
    [StringLength(100)]
    public string? OrganismeEmployeurConjointFr { get; set; }

    [StringLength(50)]
    public string? MatriculeConjoint { get; set; }

    public bool? EtatSantePersonne { get; set; }

    public double? NbrAnneeAntertieureCotisation { get; set; }

    [StringLength(50)]
    public string? VilleOrigine { get; set; }

    [Column("VilleOrigineAR")]
    [StringLength(50)]
    public string? VilleOrigineAr { get; set; }

    [Column("VilleOrigineFR")]
    [StringLength(50)]
    public string? VilleOrigineFr { get; set; }

    public Guid? SectionAnalytique { get; set; }

    public double? BrutFixe { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateNaissanceConjoint { get; set; }

    [StringLength(100)]
    public string? Responsable { get; set; }

    [Column("ResponsableFR")]
    [StringLength(100)]
    public string? ResponsableFr { get; set; }

    [Column("ResponsableAR")]
    [StringLength(100)]
    public string? ResponsableAr { get; set; }

    [Column("NomFR")]
    [StringLength(50)]
    public string NomFr { get; set; } = null!;

    [Column("NomAR")]
    [StringLength(50)]
    public string? NomAr { get; set; }

    [Column("PrenomAR")]
    [StringLength(30)]
    public string? PrenomAr { get; set; }

    [Column("PrenomFR")]
    [StringLength(30)]
    public string PrenomFr { get; set; } = null!;

    [Column("NomJeuneFilleConjointFR")]
    [StringLength(50)]
    public string? NomJeuneFilleConjointFr { get; set; }

    [Column("NomJeuneFilleConjointAR")]
    [StringLength(50)]
    public string? NomJeuneFilleConjointAr { get; set; }

    [Column("NomJeuneFilleFR")]
    [StringLength(100)]
    public string? NomJeuneFilleFr { get; set; }

    [Column("NomJeuneFilleAR")]
    [StringLength(100)]
    public string? NomJeuneFilleAr { get; set; }

    [Column(TypeName = "money")]
    public decimal? AbAnnImposable { get; set; }

    [StringLength(100)]
    public string? EmploiEffectif { get; set; }

    [StringLength(100)]
    public string? Evaluation { get; set; }

    public bool? Domiciliation { get; set; }

    [Column("BanqueEM")]
    public Guid? BanqueEm { get; set; }

    [Column("AgenceEM")]
    public Guid? AgenceEm { get; set; }

    public bool? Cavis { get; set; }

    [StringLength(100)]
    public string? CavisMatriculeEmp { get; set; }

    public bool? Chauffeur { get; set; }

    public bool? Disponible { get; set; }

    public int? PermisNum { get; set; }

    public Guid? Specialite { get; set; }

    public bool? Actif { get; set; }

    public Guid? Annee { get; set; }

    [Column("RIBEM")]
    public Guid? Ribem { get; set; }

    [Column(TypeName = "money")]
    public decimal? RedevanceAnnuelDu { get; set; }

    public bool? CalculReposCompensateur { get; set; }

    public Guid? Tiers { get; set; }

    [StringLength(255)]
    public string? AdresseEmail { get; set; }

    public double? BrutFixeAnnuel { get; set; }

    public double? BrutContrat { get; set; }

    public double? BrutContratAnnuel { get; set; }

    [StringLength(1000)]
    public string? ElementsFixesTexte { get; set; }

    public Guid? SuperieurHierarchique { get; set; }

    public Guid? SousPosition { get; set; }

    public Guid? BesoinRecrutements { get; set; }

    [StringLength(10)]
    public string? CodeEtablissementOrigine { get; set; }

    [StringLength(10)]
    public string? CodeEtablissementDetachement { get; set; }

    public Guid? ModeleComptabilistationPaie { get; set; }

    [StringLength(50)]
    public string? CreationUser { get; set; }

    [StringLength(50)]
    public string? ModifUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateSaisie { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateModif { get; set; }

    public bool? Valid { get; set; }

    public Guid? NatureService { get; set; }
}
