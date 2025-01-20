// using AutoMapper;
// using Microsoft.Extensions.Logging;
// using MongoDB.Driver;
// using Moq;
// using NGrid.Customer.Framework.Templates.Abstraction;
// using NGrid.Customer.Framework.Templates.DataBase.Repository;
// using NGrid.Customer.Framework.Templates.DataBase.Repository.Metrics;
// using NGrid.Customer.ToReplace.Domain.Core;
// using NGrid.Customer.ToReplace.Infrastructure.DAO;
// using NGrid.Customer.ToReplace.Tests.Common.TestData;
//
// namespace NGrid.Customer.ToReplace.Infrastructure.UnitTests;
//
// public class DbContextFixture
// {
//     public Mock<IAuditTrailService> AuditTrailService { get; }
//     public IMapper Mapper { get; }
//     public Mock<IDataBaseContext<ToReplaceDao>> ToReplaceContext { get; }
//     public Mock<IMongoCollection<ToReplaceDao>> ToReplaceMockCollection { get; }
//
//     public List<ToReplaceDao> InitToReplaceCollection { get; }
//
//     public DbContextFixture()
//     {
//         var mappingConfig = new MapperConfiguration(mc =>
//         {
//             mc.AddMaps("NGrid.Customer.ToReplace.Infrastructure");
//         });
//         Mapper = mappingConfig.CreateMapper();
//         AuditTrailService = new Mock<IAuditTrailService>();
//         AuditTrailService.Setup(v => v.GetCorrelationId()).Returns(Guid.NewGuid().ToString());
//
//         ToReplaceMockCollection = new Mock<IMongoCollection<ToReplaceDao>>();
//
//         InitToReplaceCollection = ToReplaceFaker.ToReplaceDaoFaker().Generate(5);
//         ToReplaceMockCollection.Object.InsertMany(InitToReplaceCollection);
//
//         ToReplaceContext = new Mock<IDataBaseContext<ToReplaceDao>>();
//         ToReplaceContext.Setup(c => c.Collection).Returns(ToReplaceMockCollection.Object);
//
//     }
//
//     public ICompositeKeyBaseRepository<Domain.Core.ToReplace> GetToReplaceRepository()
//         => new CompositeKeyBaseRepository<Domain.Core.ToReplace, ToReplaceDao>(
//             ToReplaceContext.Object, Mapper, AuditTrailService.Object,
//             new OpenTelemetryMetrics<ToReplaceDao>("toReplaces"),
//             new Mock<ILogger<CompositeKeyBaseRepository<Domain.Core.ToReplace, ToReplaceDao>>>().Object);
//
//     public void SetupGetFunctionForToReplaceInfo(string id) =>
//         ToReplaceMockCollection
//             .Setup(op =>
//                 op.FindAsync(
//                     It.IsAny<FilterDefinition<ToReplaceDao>>(),
//                     It.IsAny<FindOptions<ToReplaceDao>>(),
//                     It.IsAny<CancellationToken>()))
//             .ReturnsAsync(ContextUtility.SetupAsyncCursor(
//                 InitToReplaceCollection.Where(v => v.Description == id)).Object);
// }