using FluentAssertions;
using Moq;
using NUnit.Framework;
using PacManLibrary.Interfaces;
using PacManLibrary.Model;

namespace Tests
{
    [Category(nameof(PacMan))]
    [TestFixture]
    public class PacManTests
    {
        [SetUp] 
        public void Setup()
        {
        }

        [Test]
        public void PacManEat()
        {
            var map = new IPoint[10, 10];
            var food = new BaseFood();
            map[5,6] = food;
            Mock<IEventSink> eventMock = new Mock<IEventSink>();
            var pacman = new PacMan(map, eventMock.Object);
            map[4, 6] = pacman;

            pacman.Eat(food);

            pacman.Score.Should().Be(5);
        }
    }
}