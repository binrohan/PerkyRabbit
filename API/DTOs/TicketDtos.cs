namespace Dtos
{
    public record TicketToCreateDto(string Title, string Description, double Price);
    public record TicketToUpdateDto(int Id, string Title, string Description, double Price);
}