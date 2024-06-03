

namespace Catalog.Api.Features.CreateProduct
{
    /// <summary>
    /// Represents the request to create a product.
    /// </summary>
    public record CreateProductRequest(
        string Name,
        List<string> Categories,
        string Description,
        string ImageFile,
        decimal Price
    );

    /// <summary>
    /// Represents the response after creating a product.
    /// </summary>
    public record CreateProductResponse(Guid Id);

    /// <summary>
    /// Represents the endpoint for creating a product.
    /// </summary>
    public class CreateProductEndpoint : ICarterModule
    {
        /// <summary>
        /// Adds routes for creating a product to the specified endpoint route builder.
        /// </summary>
        /// <param name="app">The endpoint route builder.</param>
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();
                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductResponse>();
                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Creates a new product")
            .WithDescription("Creates a new product");
        }
    }
}
