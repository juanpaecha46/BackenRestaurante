namespace MenuService.Domain.Exceptions;

public class MenuItemNotFoundException : Exception
{
    public MenuItemNotFoundException(Guid id) : base($"El ítem de menú {id} no fue encontrado.") { }
}
