using MongoDB.Driver;
using Moq;

namespace NGrid.Customer.ToReplace.Infrastructure.UnitTests;

public static class ContextUtility
{
    public static Mock<IAsyncCursor<T>> SetupAsyncCursor<T>(IEnumerable<T> list)
    {
        var cursor = new Mock<IAsyncCursor<T>>();
        cursor.Setup(_ => _.Current).Returns(list);
        cursor
            .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
            .Returns(true)
            .Returns(false);
        cursor
            .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);
        return cursor;
    }
}
