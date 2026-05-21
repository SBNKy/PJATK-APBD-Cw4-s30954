using App.Models;

namespace App.DTOs;

public record PcDetailsResponse(
    int Id,
    string Name,
    double Weight,
    int Warranty,
    DateTime CreatedAt,
    int Stock,
    ICollection<PcComponentResponse> PcComponents
    );