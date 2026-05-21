namespace App.DTOs;

public record ManufacturerResponse(
    int Id,
    string Abbreviation,
    string FullName,
    DateOnly FoundationDate
    );