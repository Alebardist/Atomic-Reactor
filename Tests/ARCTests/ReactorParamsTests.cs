using AtomicReactorControl.Model;
using System;
using Xunit;

namespace ARCTests
{
    public class ReactorParamsTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void StoredEnergyPropertyStoredEnergyTheory(int storedEnergy)
        {
            //Arrange
            IReactorParams reactorParams = new ParamsForReactor() 
            {
                //Action
                StoredEnergy = storedEnergy 
            };
            //Assert
            Assert.Equal(0, reactorParams.StoredEnergy);
        }
    }
}