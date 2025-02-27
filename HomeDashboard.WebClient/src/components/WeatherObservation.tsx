import { useEffect, useState } from "react";

interface WeatherObservationApiResponse {
    observationTakenAt: Date;
    dryBulbTemperature: number;
    apparentTemperature: number;
}

function WeatherObservation() 
{
    const [weatherObservations, setWeatherObservations] = useState<WeatherObservationApiResponse>();

    useEffect(() => {
        async function fetchWeatherObservation() {
            const response = await fetch("/api/Weather/Observations/Latest");
            if (!ignore) {
                var weatherObservations : WeatherObservationApiResponse = await response.json();
                setWeatherObservations(weatherObservations);
            }
        }

        let ignore = false;
        fetchWeatherObservation();
        return () => {
            ignore = true;
        };
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