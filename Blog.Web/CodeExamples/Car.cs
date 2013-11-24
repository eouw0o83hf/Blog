using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExamples
{

    public class VehicleService
    {
        protected readonly ExampleDataContext DataContext;
        public VehicleService(ExampleDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public CarModel GetCar(string vin)
        {
            var dbModel = DataContext.GetTable<Car>().FirstOrDefault(a => a.Vin == vin);
            if (dbModel == null)
            {
                return null;
            }

            return new CarModel
            {
                Vin = dbModel.Vin,
                LicensePlate = dbModel.LicensePlate,
                Make = dbModel.Make,
                Model = dbModel.Model,
                Year = dbModel.Year,
                NumberOfDoors = dbModel.NumberOfDoors,
                Odometer = dbModel.Odometer,
                MaxSpeed = dbModel.MaxSpeed,
                HasAllWheelDrive = dbModel.HasAllWheelDrive,
                HasPowerSteering = dbModel.HasPowerSteering,
                HasAirConditioning = dbModel.HasAirConditioning,
                NumberOfAxles = dbModel.NumberOfAxles ?? 0,
                SpeedCount = dbModel.SpeedCount ?? 0,
                HighwayMilesPerGallon = dbModel.HighwayMilesPerGallon ?? 0,
                CityMilesPerGallon = dbModel.CityMilesPerGallon ?? 0,
                HasSunRoof = dbModel.HasSunRoof ?? false,
                HasDoubleSunRoof = dbModel.HasDoubleSunRoof ?? false,
                HasPowerWindows = dbModel.HasPowerWindows ?? false,
                HasAutomaticTransmission = dbModel.HasAutomaticTransmission ?? false
            };
        }
    }


    public class CarModel
    {
        public string Vin { get; set; }
        public string LicensePlate { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public int NumberOfDoors { get; set; }
        public int Odometer { get; set; }
        public int MaxSpeed { get; set; }

        public bool HasAllWheelDrive { get; set; }
        public bool HasPowerSteering { get; set; }
        public bool HasAirConditioning { get; set; }

        public int NumberOfAxles { get; set; }
        public int SpeedCount { get; set; }
        public int HighwayMilesPerGallon { get; set; }
        public int CityMilesPerGallon { get; set; }

        public bool HasSunRoof { get; set; }
        public bool HasDoubleSunRoof { get; set; }
        public bool HasPowerWindows { get; set; }
        public bool HasAutomaticTransmission { get; set; }
    }
}
