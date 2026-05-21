namespace App.DTOs;

public record ComponentResponse(
    string Code,
    string Name,
    string Description,
    ManufacturerResponse Manufacturer,
    ComponentTypeResponse Type
    );