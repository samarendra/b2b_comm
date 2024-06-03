using System.Runtime.InteropServices;
using BuildingBlocks;
using MediatR;
using Catalog.Api.Models;
using Marten;
namespace Catalog.Api.Features.CreateProduct;

public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price)
: ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Create product entity from command object 
        var product = new Product
        {
            Name = command.Name,
            Categories = command.Categories,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // Save product to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        // Return Result
        return new CreateProductResult(product.Id);
    }
}

