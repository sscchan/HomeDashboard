import { useEffect, useState } from "react";

interface WeatherObservationApiResponse {
    observationTakenAt: Date;
    dryBulbTemperature: number;
    apparentTemperature: number;
}

interface WeatherForecastApiResponse {
    dateTime: Date;
    deicticTime: string;
    weatherDescription: string;
    rainProbabilityPercentage : number;
    minimumRainfall: number;
    maximumRainfall: number;
    minimumTemperature: number;
    maximumTemperature: number;
}

function TodayWeatherForecastAndObservation() 
{
    const OBSERVATION_DATA_FETCH_INTERVAL_IN_MINUTES : number = 10; 
    const FORECAST_DATA_FETCH_INTERVAL_IN_HOURS : number = 1; 

    const [weatherObservations, setWeatherObservations] = useState<WeatherObservationApiResponse>();
    const [todayWeatherForecast, setTodayWeatherForecast] = useState<WeatherForecastApiResponse>();

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

    useEffect(() => {
            async function fetchWeatherForecasts() {
                const response = await fetch("/api/Weather/Forecasts");
                var weatherForecasts : Array<WeatherForecastApiResponse> = await response.json();
                
                setTodayWeatherForecast(weatherForecasts.find(wf => wf.deicticTime === "Today"));
            }
    
            // Initial data population
            fetchWeatherForecasts();
    
            // Scheduled data refresh
            var timer = setInterval(() => fetchWeatherForecasts(), FORECAST_DATA_FETCH_INTERVAL_IN_HOURS * 60 * 60 * 1000 )
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
            fontSize: '3vw'
        }

        const forecastDivStyle = {
            fontSize: '3vw'
        }
        
        const observationSupStyle = {
            fontSize: '1.5vw'
        }


        return (
            <>
                <div style={observationDivStyle}>
                    <sup style={observationSupStyle}>currently&nbsp;&nbsp;</sup>{weatherObservations.dryBulbTemperature.toFixed(1)}°<br />
                </div>
                <div style={forecastDivStyle}>
                    {todayWeatherForecast?.weatherDescription}<br />
                    {todayWeatherForecast?.minimumTemperature.toFixed(1)}° - {todayWeatherForecast?.maximumTemperature.toFixed(1)}°
                </div>
            </>
        );
    }
}

export default TodayWeatherForecastAndObservation;