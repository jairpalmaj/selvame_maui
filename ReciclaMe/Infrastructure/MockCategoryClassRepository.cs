using ReciclaMe.Domain;

namespace ReciclaMe.Infrastructure;

public sealed class MockCategoryClassRepository : ICategoryClassRepository
{
    private IReadOnlyList<CategoryClass>? _categories = null;
    
    public async Task<IReadOnlyList<CategoryClass>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        if (_categories is null)
        {
            _categories = new List<CategoryClass>()
            {
                new ()
                {
                    Id = 1,
                    Category = Category.Cardboard,
                    Title = "Cartón",
                    IsRecyclable = true,
                    CategoryInformation = "Cartón: Cajas de provisiones o tubos. Recuerden saltar sobre ellos para aplastarlos y que quepan más tesoros en el cofre."
                },
                new ()
                {
                    Id = 2,
                    Category = Category.Glass,
                    Title = "Vidrio",
                    IsRecyclable = true,
                    CategoryInformation = "Vidrio: Frascos de antídotos o botellas. Usan el poder del calor de los volcanes para derretirse y formar recipientes nuevos."
                },
                new ()
                {
                    Id = 3,
                    Category = Category.Metal,
                    Title = "Metal",
                    IsRecyclable = true,
                    CategoryInformation = "Metal: Latas de conservas de nuestro campamento. Son como armaduras invencibles que se pueden fundir y usar en infinitas aventuras."
                },
                new ()
                {
                    Id = 4,
                    Category = Category.Paper,
                    Title = "Papel",
                    IsRecyclable = true,
                    CategoryInformation = "Papel: Mapas viejos, dibujos y libretas de expedición. ¡Al rescatarlos, evitamos que se corten más árboles!"
                },
                new ()
                {
                    Id = 5,
                    Category = Category.Plastic,
                    Title = "Plástico",
                    IsRecyclable = true,
                    CategoryInformation = "Plástico: Cantimploras de agua vacías o envases. Si los dejamos en el suelo, lastimarán a los animales, pero al rescatarlos pueden convertirse en fibra para tiendas de campaña."
                },
                new ()
                {
                    Id = 6,
                    Category = Category.Trash,
                    Title = "Basura",
                    IsRecyclable = false,
                    CategoryInformation = "Basura: Envolturas de los dulces que comimos en el camino, fotografías viejas o pedazos de papel llenos de grasa. La grasa y el pegamento destruyen la magia del reciclaje, así que deben quedarse aquí. \n Desechos Sanitarios (¡Peligro de Esporas Tóxicas!): Papel higiénico usado, pañuelos con mocos, curitas de nuestras heridas o cepillos de dientes. Tienen bacterias peligrosas. Si los mezclan con el cofre gris, ¡toda la misión fracasará y los tesoros mágicos se enfermarán!"
                },
            };
        }
        
        return await Task.FromResult(_categories);
    }

    public async Task<CategoryClass> GetCategoryByClassId(int categoryClassId, CancellationToken cancellationToken = default)
    {
        var category = await GetCategoriesAsync(cancellationToken);
        return category.First(c => c.Id == categoryClassId);
    }
}