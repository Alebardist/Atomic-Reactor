using AtomicReactorControl.ViewModel;
using AtomicReactorControl.ViewModel.Interfaces;

using Xunit;

namespace ARCTests
{
    public class ReactorParamsTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void StoredEnergyPropertyShouldContainCorrectValueTheory(int storedEnergy)
        {
            //Arrange
            int expected = 0;

            IReactorParams reactorParams = new ParamsForReactor()
            {
                //Action
                StoredEnergy = storedEnergy
            };

            var actual = reactorParams.StoredEnergy;

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}