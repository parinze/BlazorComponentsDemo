using BlazorComponentsDemo.ComponentsLibrary;
using Bunit;
using BlazorDateRangePicker;
using System.Reflection;

namespace BlazorComponentsDemo.Tests
{
    public class DatePickerTests
    {
        [Fact]
        public void DatePicker_RendersBlazorDateRangePickerComponent()
        {
            // Arrange
            using var ctx = new TestContext();

            // Act
            var cut = ctx.RenderComponent<DatePicker<DateTime?>>();

            // Assert
            Assert.NotNull(cut.FindComponent<DateRangePicker>());
        }

        [Fact]
        public void DatePicker_SelectsDateRange()
        {
            // Arrange
            using var ctx = new TestContext();
            var startDate = new DateTime(2024, 4, 1);
            var startDateOffset = new DateTimeOffset(startDate, TimeSpan.Zero);
            var endDate = new DateTime(2024, 4, 10);
            var endDateOffset = new DateTimeOffset(endDate, TimeSpan.Zero);

            // Act
            var cut = ctx.RenderComponent<DatePicker<DateTime?>>();

            var onRangeSelectMethod = cut.Instance.GetType()
                    .GetMethod("OnRangeSelect", BindingFlags.NonPublic | BindingFlags.Instance);
            var dateRange = new DateRange();
            typeof(DateRange).GetProperty("Start")!.SetValue(dateRange, startDateOffset);
            typeof(DateRange).GetProperty("End")!.SetValue(dateRange, endDateOffset);
            onRangeSelectMethod!.Invoke(cut.Instance, new object[] { dateRange });

            // Assert
            Assert.Equal(startDate, cut.Instance.StartDateValue);
            Assert.Equal(endDate, cut.Instance.EndDateValue);
        }

        [Fact]
        public void DatePicker_SelectsSingleDate()
        {
            // Arrange
            using var ctx = new TestContext();
            var selectedDate = new DateTime(2024, 4, 1);
            var selectedDateOffset = new DateTimeOffset(selectedDate, TimeSpan.Zero);

            // Act
            var cut = ctx.RenderComponent<DatePicker<DateTime?>>();
            cut.SetParametersAndRender(parameters => parameters
                .Add(p => p.EnableSingleDateSelection, true)
            );

            var onRangeSelectMethod = cut.Instance.GetType()
                    .GetMethod("OnRangeSelect", BindingFlags.NonPublic | BindingFlags.Instance);
            var dateRange = new DateRange();
            typeof(DateRange).GetProperty("Start")!.SetValue(dateRange, selectedDateOffset);
            typeof(DateRange).GetProperty("End")!.SetValue(dateRange, selectedDateOffset);
            onRangeSelectMethod!.Invoke(cut.Instance, new object[] { dateRange });

            // Assert
            Assert.Equal(selectedDate, cut.Instance.StartDateValue);
            Assert.Equal(selectedDate, cut.Instance.EndDateValue);
        }

        [Fact]
        public void DatePicker_ClearsDateRange()
        {
            // Arrange
            using var ctx = new TestContext();
            var startDate = new DateTime(2024, 4, 1);
            var startDateOffset = new DateTimeOffset(startDate, TimeSpan.Zero);
            var endDate = new DateTime(2024, 4, 10);
            var endDateOffset = new DateTimeOffset(endDate, TimeSpan.Zero);

            // Act
            var cut = ctx.RenderComponent<DatePicker<DateTime?>>();

            var onRangeSelectMethod = cut.Instance.GetType()
                    .GetMethod("OnRangeSelect", BindingFlags.NonPublic | BindingFlags.Instance);
            var dateRange = new DateRange();
            typeof(DateRange).GetProperty("Start")!.SetValue(dateRange, startDateOffset);
            typeof(DateRange).GetProperty("End")!.SetValue(dateRange, endDateOffset);
            onRangeSelectMethod!.Invoke(cut.Instance, new object[] { dateRange });

            cut.Find("#clear-btn").Click();

            // Assert
            Assert.Equal(DateTime.MinValue, cut.Instance.StartDateValue);
            Assert.Equal(DateTime.MinValue, cut.Instance.EndDateValue);
        }
    }
}
