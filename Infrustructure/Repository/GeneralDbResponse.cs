using Application.DTO.Response;

namespace Infrustructure.Repository;

public static class GeneralDbResponse
{
    public static ServiceResponse ItemAlreadyExist(string itemName) => new(false, $"{itemName} already created");
    public static ServiceResponse ItemNotFound(string itemName) => new(false, $"{itemName} not found");
    public static ServiceResponse ItemCreated(string itemName) => new(true, $"{itemName} successfully created");
    public static ServiceResponse ItemUpdate(string itemName) => new(true, $"{itemName} successfully updated");
    public static ServiceResponse ItemDelete(string itemName) => new(true, $"{itemName} successfully deleted");
}
