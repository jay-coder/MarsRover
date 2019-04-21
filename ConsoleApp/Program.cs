using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Zip.CodingTest.Core.Enums;
using Zip.CodingTest.Core.Factories;
using Zip.CodingTest.Core.Interfaces;
using Zip.CodingTest.Core.Models;
using Zip.CodingTest.Core.Services;

namespace Zip.CodingTest.ConsoleApp
{
    class Program
    {
        private static ServiceProvider _svcProvider;

        private static IRoverFactory _roverFactory;
        private static IRoverService _roverService;
        static void Main(string[] args)
        {
            _svcProvider = new ServiceCollection()
                .AddSingleton<IRoverFactory, RoverFactory>()
                .AddTransient<IRoverService, RoverService>()
                .AddTransient<IValidatorService, ValidatorService>()
                .BuildServiceProvider();

            _roverFactory = _svcProvider.GetService<IRoverFactory>();
            _roverService = _svcProvider.GetService<IRoverService>();

            ProcessRovers();
        }

        static bool GetPlateauFromInput()
        {
            bool isQuit = false;

            Console.WriteLine("Please input Plateau upper-right coordinate e.g. 5 5:");
            string strCoordinate = ReadInput(out isQuit);
            if(isQuit)
            {
                return isQuit;
            }
            Plateau plateau = _roverFactory.InitPlateau(strCoordinate);
            while(plateau == null)
            {
                Console.WriteLine("Invalid Plateau coordinate, please input Plateau upper-right coordinate e.g. 5 5:");
                strCoordinate = ReadInput(out isQuit);
                if(isQuit)
                {
                    return isQuit;
                }
                plateau = _roverFactory.InitPlateau(strCoordinate);
            }
            return isQuit;
        }

        static bool GetRoverFromInput(out Rover rover)
        {
            bool isQuit = false;
            rover = new Rover(new Coordinate(0, 0), EnumFacingDirection.N);

            Console.WriteLine("Please input Rover position e.g. 1 2 N:");
            string strRover = ReadInput(out isQuit);
            if(isQuit)
            {
                return isQuit;
            }
            try
            {
                rover = _roverFactory.CreateRover(strRover);
            }
            catch(ArgumentException aEx)
            {
                rover = null;
                Console.WriteLine(aEx.Message);
            }
            // Get Rover position and direction
            while(rover == null)
            {
                Console.WriteLine("Invalid Rover position, please input Rover position e.g. 1 2 N:");
                strRover = ReadInput(out isQuit);
                if(isQuit)
                {
                    return isQuit;
                }
                try
                {
                    rover = _roverFactory.CreateRover(strRover);
                }
                catch(ArgumentException aEx)
                {
                    rover = null;
                    Console.WriteLine(aEx.Message);
                }
            }
            return isQuit;
        }
        
        static void ProcessRovers()
        {
            var roverList = new List<Rover>();
            bool isQuit = GetPlateauFromInput();
            if(!isQuit)
            {
                while(true)
                {
                    Rover rover = null;
                    // Get rover
                    isQuit = GetRoverFromInput(out rover);
                    if(isQuit)
                    {
                        break;
                    }
                    // Get rover instructions
                    if(rover != null)
                    {
                        Console.WriteLine("Please input Rover instructions e.g. LMLMLMLMM:");
                        string strCommands = ReadInput(out isQuit);
                        if(isQuit)
                        {
                            break;
                        }
                        try
                        {
                            rover = _roverService.BatchDrive(rover, strCommands);
                            roverList.Add(rover);
                            Console.WriteLine("Rover has been created!");
                        }
                        catch(ArgumentException aEx)
                        {
                            Console.WriteLine(aEx.Message);
                            continue;
                        }
                    }
                }
            }
            Console.WriteLine("Rovers:");
            if(roverList.Count > 0)
            {
                foreach(var rover in roverList)
                {
                    Console.WriteLine(rover.ToString());
                }
            }
            else
            {
                Console.WriteLine("Not found");
            }
        }

        static string ReadInput(out bool isQuit)
        {
            string strInput = Console.ReadLine();
            isQuit = CheckQuit(strInput);
            return strInput;
        }
        static bool CheckQuit(string strInput)
        {
            if(
                string.IsNullOrEmpty(strInput) || 
                strInput.Equals("Quit", StringComparison.OrdinalIgnoreCase) || 
                strInput.Equals("Q", StringComparison.OrdinalIgnoreCase)
            )
            {
                return true;
            }
            return false;
        }
    }
}
