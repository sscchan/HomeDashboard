import { useEffect, useState } from "react";

interface WeatherObservationApiResponse {
    observationTakenAt: Date;
    dryBulbTemperature: number;
    apparentTemperature: number;
}

function SingleDayWeatherObservation() 
{
    const OBSERVATION_DATA_FETCH_INTERVAL_IN_MINUTES : number = 10; 

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
        var timer = setInterval(() => fetchWeatherObservation(), OBSERVATION_DATA_FETCH_INTERVAL_IN_MINUTES * 60 * 1000 )
        return function cleanup() {
            clearInterval(timer)
        }
    }, []);

    if (weatherObservations === null || weatherObservations === undefined)
    {
        return (
            <div>
                Loading Today's Weather Observation & Forecast...
            </div>
        )
    }
    else
    {
        const observationDivStyle = {
            fontSize: '8vw'
        }

        
        const observationSupStyle = {
            fontSize: '4vw'
        }


        return (
            <>
                <div style={observationDivStyle}>
                    <sup style={observationSupStyle}>now&nbsp;&nbsp;</sup>{weatherObservations.dryBulbTemperature.toFixed(1)}°<br />
                </div>
            </>
        );
    }
}

export default SingleDayWeatherObservation;