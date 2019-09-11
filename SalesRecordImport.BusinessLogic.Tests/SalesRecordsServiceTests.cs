using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SalesRecordImport.BusinessLogic.Services;
using SalesRecordImport.BusinessLogic.Settings;
using SalesRecordImport.DataAccess.Repositories;
using SalesRecordImport.DataAccess.UnitOfWork;
using SalesRecordImport.Domain;
using System;
using System.Threading.Tasks;

namespace SalesRecordImport.BusinessLogic.Tests
{
    [TestClass]
    public class SalesRecordsServiceTests
    {
        private static readonly Mock<ISalesRecordsRepository> _repositoryMock = new Mock<ISalesRecordsRepository>();
        private static readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private static readonly Mock<IServiceProvider> _serviceProviderMock = new Mock<IServiceProvider>();
        private static ISalesRecordsService _salesRecordsService;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            var options = Options.Create(new SalesRecordsParsingSettings());
            _salesRecordsService = new SalesRecordsService(options,
                _repositoryMock.Object,
                _serviceProviderMock.Object,
                _unitOfWorkMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ImportRecordsFromCsvFile_WhenFilePathIsNull_ShouldThrowException()
        {
            await _salesRecordsService.ImportRecordsFromCsvFile(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetSalesRecords_WhenOptionsIsNull_ShouldThrowException()
        {
            await _salesRecordsService.GetSalesRecords(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task UpdateSalesRecord_WhenOptionsIsNull_ShouldThrowException()
        {
            await _salesRecordsService.UpdateSalesRecord(null);
        }

        [TestMethod]
        public async Task UpdateSalesRecords_WhenRepositoryReturnsZero_ShouldReturnFalse()
        {
            var salesRecord = new SalesRecord();
            _repositoryMock.Setup(x => x.UpdateRecord(salesRecord)).ReturnsAsync(0);
            var result = await _salesRecordsService.UpdateSalesRecord(salesRecord);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task UpdateSalesRecords_WhenRepositoryReturnsOne_ShouldReturnTrue()
        {
            var salesRecord = new SalesRecord();
            _repositoryMock.Setup(x => x.UpdateRecord(salesRecord)).ReturnsAsync(1);
            var result = await _salesRecordsService.UpdateSalesRecord(salesRecord);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task DeleteSalesRecord_WhenRepositoryReturnsZero_ShouldReturnFalse()
        {
            var salesRecordId = 1;
            _repositoryMock.Setup(x => x.DeleteRecord(salesRecordId)).ReturnsAsync(0);
            var result = await _salesRecordsService.DeleteSalesRecord(salesRecordId);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task DeleteSalesRecord_WhenRepositoryReturnsOne_ShouldReturnTrue()
        {
            var salesRecordId = 1;
            _repositoryMock.Setup(x => x.DeleteRecord(salesRecordId)).ReturnsAsync(1);
            var result = await _salesRecordsService.DeleteSalesRecord(salesRecordId);

            Assert.IsTrue(result);
        }
    }
}
