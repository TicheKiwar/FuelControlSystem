using System;
using System.Text.Json.Serialization;
using Common.Shared.Enums;

namespace Common.Shared.App
{
    public class TravelTimeCalculator
    {
        // Factores de ajuste para diferentes niveles de dificultad
        private static readonly Dictionary<DifficultyLevel, double> _difficultyFactors = new()
        {
            { DifficultyLevel.Easy, 1.0 },      // Sin penalización
            { DifficultyLevel.Medium, 1.3 },    // 30% más tiempo
            { DifficultyLevel.Hard, 1.7 }       // 70% más tiempo
        };

        // Factor de emergencia (reduce el tiempo estimado)
        private const double EmergencyFactor = 0.75; // 25% más rápido en emergencias

        /// <summary>
        /// Calcula el tiempo estimado de viaje en horas
        /// </summary>
        /// <param name="distance">Distancia en kilómetros</param>
        /// <param name="averageSpeed">Velocidad promedio en km/h</param>
        /// <param name="isEmergency">Indica si es una emergencia</param>
        /// <param name="difficultyLevel">Nivel de dificultad del trayecto</param>
        /// <returns>Tiempo estimado en horas</returns>
        public static double CalculateEstimatedTime(
            double distance, 
            double averageSpeed, 
            DifficultyLevel difficultyLevel = DifficultyLevel.Easy)
        {
            // Validaciones
            if (distance <= 0)
                throw new ArgumentException("La distancia debe ser mayor a 0", nameof(distance));
            
            if (averageSpeed <= 0)
                throw new ArgumentException("La velocidad promedio debe ser mayor a 0", nameof(averageSpeed));

            // Cálculo base del tiempo (distancia / velocidad)
            double baseTime = distance / averageSpeed;

            // Aplicar factor de dificultad
            double adjustedTime = baseTime * _difficultyFactors[difficultyLevel];


            return Math.Round(adjustedTime, 2);
        }

        /// <summary>
        /// Calcula el tiempo estimado de viaje y devuelve un objeto con información detallada
        /// </summary>
        public static TravelTimeResult CalculateDetailedEstimatedTime(
            double distance, 
            double averageSpeed, 
            DifficultyLevel difficultyLevel = DifficultyLevel.Easy)
        {
            double estimatedHours = CalculateEstimatedTime(distance, averageSpeed, difficultyLevel);
            
            return new TravelTimeResult
            {
                EstimatedHours = estimatedHours,
                EstimatedMinutes = estimatedHours * 60,
                Distance = distance,
                AverageSpeed = averageSpeed,
                DifficultyLevel = difficultyLevel,
                BaseTime = distance / averageSpeed,
                DifficultyFactor = _difficultyFactors[difficultyLevel],
            };
        }

        /// <summary>
        /// Convierte horas a formato legible (horas y minutos)
        /// </summary>
        /// <param name="hours">Tiempo en horas</param>
        /// <returns>String con formato "X horas Y minutos"</returns>
        public string FormatTime(double hours)
        {
            int wholeHours = (int)Math.Floor(hours);
            int minutes = (int)Math.Round((hours - wholeHours) * 60);
            
            if (wholeHours == 0)
                return $"{minutes} minutos";
            else if (minutes == 0)
                return $"{wholeHours} horas";
            else
                return $"{wholeHours} horas {minutes} minutos";
        }
    }

    /// <summary>
    /// Resultado detallado del cálculo de tiempo de viaje
    /// </summary>
    public class TravelTimeResult
    {
        public double EstimatedHours { get; set; }
        public double EstimatedMinutes { get; set; }
        public double Distance { get; set; }
        public double AverageSpeed { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DifficultyLevel DifficultyLevel { get; set; }
        
        public double BaseTime { get; set; }
        public double DifficultyFactor { get; set; }
        public double EmergencyFactor { get; set; }

        public string FormattedTime => 
            $"{(int)Math.Floor(EstimatedHours)} horas {(int)Math.Round((EstimatedHours - Math.Floor(EstimatedHours)) * 60)} minutos";
    }

    // Ejemplo de uso
    public class TravelTimeExample
    {
        public static void RunExample()
        {
            var calculator = new TravelTimeCalculator();

            // Ejemplo 1: Viaje TravelTimeCalculator
            double time1 = TravelTimeCalculator.CalculateEstimatedTime(
                distance: 100,           // 100 km
                averageSpeed: 60,        // 60 km/h
                difficultyLevel: DifficultyLevel.Easy
            );
            Console.WriteLine($"Tiempo estimado (normal): {calculator.FormatTime(time1)}");

            // Ejemplo 2: Viaje de emergencia en terreno difícil
            var result2 = TravelTimeCalculator.CalculateDetailedEstimatedTime(
                distance: 150,           // 150 km
                averageSpeed: 80,        // 80 km/h
                difficultyLevel: DifficultyLevel.Hard
            );
            Console.WriteLine($"Tiempo estimado (emergencia, difícil): {result2.FormattedTime}");
            Console.WriteLine($"Tiempo base: {result2.BaseTime:F2} horas");
            Console.WriteLine($"Factor dificultad: {result2.DifficultyFactor}");
            Console.WriteLine($"Factor emergencia: {result2.EmergencyFactor}");
        }
    }
}