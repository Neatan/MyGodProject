using System;
using System.Collections.Generic;
using Car.Properties;

namespace Car
{
    public class Man
    {
        public float Money = 10000;
        //private Car[] amountCar;
        private Garage garage = new Garage();

        public void SelectCar()
        {
            Console.WriteLine("Выберите машину которую хотите купить(Toyota или Bentley) или откройте гараж(OpenGarage) или продать(Sell)\n");
            var carName = Console.ReadLine();

            if (carName == "Toyota" || carName == "toyota")
            {
                Toyota car = new Toyota();
                Buy(car);
            }
           
            if (carName == "Bentley" || carName == "bentley")
            {
                Bentley car = new Bentley();
                Buy(car);
            }

            if (carName == "OpenGarage" || carName == "opengarage")
            {
                garage.OpenGarage();
            }

            if (carName == "Sell" || carName == "sell")
            {
                if (garage.amountCar.Count != 0)
                {
                    Console.WriteLine("Какую машину хотите продать?");
                    var sellName = Console.ReadLine();
                    Sell(sellName);
                }
                else
                {
                    Console.WriteLine("Продавать нечего");
                    SelectCar();
                }
                    
            }
        }
        public void Buy(Car car)
        {
            if (Money >= car.Cost)
            {
                Money -= car.Cost;
                Console.WriteLine($"Вы купили машину за {car.Cost} рублей, у вас осталось {Money} рублей");
                garage.amountCar.Add(car);
                SelectCar();
            }
        }

        public void Sell(string nameCar)
        {
            Car car = null;
            
            foreach (Car _car in garage.amountCar)
            { 
                int i = 0; 
                if (nameCar == _car.Name)
                { 
                    Money += _car.Cost; 
                    Console.WriteLine($"Вы продали машину {_car.Name}, у вас стало {Money} рублей"); 
                    car = _car; 
                    break; 
                } 
            }
            garage.amountCar.Remove(car);
            
            SelectCar();
        }
    }
}