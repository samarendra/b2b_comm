using Catalog.Api.Features.GetProducts;
using Catalog.Api.Models;

namespace Catalog.Api;

//public record GetProductRequest();

public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetProductQuery());
            var response = result.Adapt<GetProductResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get all products")
        .WithDescription("Get all products from the catalog");
    }
}
