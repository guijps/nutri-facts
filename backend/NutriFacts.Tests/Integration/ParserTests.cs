using Xunit;
using NutriFacts.Service.Parser.OpenFood;
public class ParserTests
{

    [Fact]
    public void Parse_ValidOpenFoodJson_ReturnsListOfNutritionFacts()
    {
        var jsonPath =  Path.Combine(AppContext.BaseDirectory, "openfoods_list_data_parse.json").Replace("bin\\Debug\\net10.0", "");

        var json = File.ReadAllText(jsonPath);
        var parser = new OpenFoodParser();

        var product = parser.ParseList(json);
        product!.ForEach(p =>
        {
            Assert.NotNull(p.Name);
            Assert.NotNull(p.NutritionFacts);
        });
    }

    [Fact]
    
    public void Parse_ValidOpenFoodJson_ReturnsCorrectNutritionFacts()
    {
       var jsonPath =  Path.Combine(AppContext.BaseDirectory, "openfoods_data_parse_1.json").Replace("bin\\Debug\\net10.0", "");

        var json = File.ReadAllText(jsonPath);
        var parser = new OpenFoodParser();

        var product = parser.Parse(json);
        Assert.Equal("Flocons d'avoine", product!.Name);
        Assert.Equal(372, product.NutritionFacts.Calories);
        Assert.Equal(59, product.NutritionFacts.Carbohydrates);
        Assert.Equal(14, product.NutritionFacts.Proteins);
        Assert.Equal(7, product.NutritionFacts.Fat);
    }

    [Fact]
    public void Parse_InvalidJson_ReturnsNull()
    {
        var parser = new OpenFoodParser();

        var product = parser.Parse("{ not valid json }");

        Assert.Null(product);
    }
}
