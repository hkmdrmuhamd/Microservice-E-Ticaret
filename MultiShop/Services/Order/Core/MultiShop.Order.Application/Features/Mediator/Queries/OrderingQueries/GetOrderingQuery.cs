using MediatR;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries
{
    
    public class GetOrderingQuery : IRequest<List<GetOrderingQueryResult>>
//IRequest ile bunun bir istek olduğunu belirtiyoruz. IRequestHandler ile de bu isteği karşılayacak olan sınıfı belirtiyoruz.
// Bu yapı sayesinde her controller'da ayrı ayrı constructor ile sınıfı oluşturmak yerine MediatR'ın sunduğu IRequestHandler yapısını kullanarak sınıfı oluşturuyoruz.
    {
    }
}
