using System;
using System.Collections.Generic;

namespace Car.Properties
{
    public class Garage
    {
        public List<Car> amountCar = new List<Car>();
        
        public void OpenGarage()
        {
            foreach (Car car in amountCar) 
            {
                Console.WriteLine(car.Name);
            }
        }
    }
}