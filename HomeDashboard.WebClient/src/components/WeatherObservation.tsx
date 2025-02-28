import { useEffect, useState } from "react";

interface WeatherObservationApiResponse {
    observationTakenAt: Date;
    dryBulbTemperature: number;
    apparentTemperature: number;
}

function WeatherObservation() 
{
    const DATA_FETCH_INTERVAL_IN_MINUTES : number = 10; 
    const [weatherObservations, setWeatherObservations] = useState<WeatherObservationApiResponse>();

    useEffect(() => {
        async function fetchWeatherObservation() {
            const response = await fetch("/api/Weather/Observations/Latest");
            var weatherObservations : WeatherObservationApiResponse = await response.json();
            setWeatherObservations(weatherObservations);
        }

        // Initial data population
        fetchWeatherObservation();

        // Scheduled data refresh
        var timer = setInterval(() => fetchWeatherObservation(), DATA_FETCH_INTERVAL_IN_MINUTES * 60 * 1000 )
        return function cleanup() {
            clearInterval(timer)
        }
    }, []); 

    if (weatherObservations === null || weatherObservations === undefined)
    {
        return (
            <div>
                Loading Weather Observation...
            </div>
        )
    }
    else
    {
        const divStyle = {
            fontSize: '6vw'
        }
        
        const supStyle = {
            fontSize: '2vw'
        }

        return (
            <div style={divStyle}>
                <sup style={supStyle}>currently&nbsp;&nbsp;</sup>{weatherObservations.dryBulbTemperature.toFixed(1)}°
            </div>
        );
    }
}

export default WeatherObservation;