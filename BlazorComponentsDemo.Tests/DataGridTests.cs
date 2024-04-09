using BlazorComponentsDemo.ComponentsLibrary;
using BlazorComponentsDemo.DataModels.Models;
using Bunit;
using Radzen.Blazor;

namespace BlazorComponentsDemo.Tests
{
    public class DataGridTests
    {

        [Fact]
        public void DataGridRadzen_RendersRadzenDataGridComponent()
        {
            // Arrange
            using var ctx = new TestContext();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            // Act
            var cut = ctx.RenderComponent<DataGridRadzen<TestData>>();

            // Assert
            Assert.NotNull(cut.FindComponent<RadzenDataGrid<TestData>>());
        }

        [Fact]
        public void DataGridRadzen_RendersMarkup()
        {
            // Arrange
            using var ctx = new TestContext();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            // Act
            var cut = ctx.RenderComponent<DataGridRadzen<TestData>>();

            // Assert
            Assert.NotNull(cut.Find(".rz-data-grid"));
            Assert.NotNull(cut.Find(".rz-data-grid-data"));
            Assert.NotNull(cut.Find(".rz-paginator"));
        }

        [Fact]
        public void DataGridRadzen_RendersPageOptionsMarkup()
        {
            // Arrange
            using var ctx = new TestContext();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            // Act
            var cut = ctx.RenderComponent<DataGridRadzen<TestData>>();

            // Assert
            Assert.NotNull(cut.Find(".rz-dropdown-item[aria-label='10']"));
            Assert.NotNull(cut.Find(".rz-dropdown-item[aria-label='20']"));
            Assert.NotNull(cut.Find(".rz-dropdown-item[aria-label='50']"));
            Assert.NotNull(cut.Find(".rz-dropdown-item[aria-label='100']"));
            Assert.NotNull(cut.Find(".rz-dropdown-item[aria-label='500']"));
        }

        [Fact]
        public void DataGridRadzen_ChangesPageOptionsTo20()
        {
            // Arrange
            using var ctx = new TestContext();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            // Act
            var cut = ctx.RenderComponent<DataGridRadzen<TestData>>();
            var pageOption20 = cut.Find(".rz-dropdown-item[aria-label='20']");
            pageOption20.Click();

            // Assert
            var pageOption10 = cut.Find(".rz-dropdown-item[aria-label='10']");
            Assert.DoesNotContain("rz-state-highlight", pageOption10.ClassList);

            var pageOption50 = cut.Find(".rz-dropdown-item[aria-label='50']");
            Assert.DoesNotContain("rz-state-highlight", pageOption50.ClassList);

            var pageOption100 = cut.Find(".rz-dropdown-item[aria-label='100']");
            Assert.DoesNotContain("rz-state-highlight", pageOption100.ClassList);

            var pageOption500 = cut.Find(".rz-dropdown-item[aria-label='500']");
            Assert.DoesNotContain("rz-state-highlight", pageOption500.ClassList);

            Assert.Contains("rz-state-highlight", pageOption20.ClassList);
        }

        [Fact]
        public void DataGridRadzen_RendersHeadersVerifiesCount()
        {
            // Arrange
            using var ctx = new TestContext();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            // Act
            var cut = ctx.RenderComponent<DataGridRadzen<TestData>>();
            var type = typeof(TestData);
            var properties = type.GetProperties();
            var totalPropertiesCount = properties.Length;

            // Assert
            Assert.Equal(totalPropertiesCount, cut.FindAll(".rz-grid-table th").Count);
        }

        [Fact]
        public void DataGridRadzen_RendersHeadersVerifiesTextContent()
        {
            // Arrange
            using var ctx = new TestContext();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            // Act
            var cut = ctx.RenderComponent<DataGridRadzen<TestData>>();
            var type = typeof(TestData);
            var properties = type.GetProperties();
            var propertiesText = new List<string>();

            foreach (var prop in properties)
            {
                propertiesText.Add(prop.Name);
            }

            var headers = cut.FindAll(".rz-column-title-content");

            // Assert
            foreach (var header in headers)
            {
                Assert.Contains(header.TextContent, propertiesText);
            }
        }
    }
}