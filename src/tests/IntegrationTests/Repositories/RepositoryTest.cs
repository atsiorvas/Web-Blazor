using System;
using System.Collections.Generic;
using BlazorApp.ApplicationCore.Entities;
using BlazorApp.ApplicationCore.Interfaces;
using Moq;

namespace BlazorApp.IntegrationTests.Repositories {

    public class RepositoryTest {
        private readonly Mock<IRepository<Customer>> _mockRepo;

        List<Customer> CustomerInMemoryDatabase = new List<Customer>{
            new Customer() {Id = new Guid("ee89dc71-0b96-469b-814c-1dc455b8d46c"), CompanyName = "Book1"},
            new Customer() {Id = new Guid("612f53ad-2159-4fb9-99d1-7a8c41ec1a12"), CompanyName = "Book2"},
            new Customer() {Id = new Guid("117336be-7dd7-45d8-9138-ef3f38c842a7"), CompanyName = "Book3"}
        };

        public RepositoryTest() {
            var _mockRepo = new Mock<IRepository<Customer>>();
        }
    }
}