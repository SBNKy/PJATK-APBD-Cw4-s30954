using System.ComponentModel.DataAnnotations;

namespace App.DTOs;

public record CreatePcRequest(
    [MaxLength(50)] string Name,
    double Weight,
    int Warranty,
    DateTime CreatedAt,
    int Stock
    );