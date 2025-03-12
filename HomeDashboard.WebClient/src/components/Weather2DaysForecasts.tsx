import { useEffect, useState } from "react";

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

function Weather2DaysForecasts() 
{
    const DATA_FETCH_INTERVAL_IN_HOURS : number = 1; 

    const [weatherForecasts, setWeatherForecasts] = useState<Array<WeatherForecastApiResponse>>([]);

    useEffect(() => {
        async function fetchWeatherForecasts() {
            const response = await fetch("/api/Weather/Forecasts");
            var weatherForecasts : Array<WeatherForecastApiResponse> = await response.json();
            setWeatherForecasts(weatherForecasts);
        }

        // Initial data population
        fetchWeatherForecasts();

        // Scheduled data refresh
        var timer = setInterval(() => fetchWeatherForecasts(), DATA_FETCH_INTERVAL_IN_HOURS * 60 * 60 * 1000 )
        return function cleanup() {
            clearInterval(timer)
        }
    }, []); 

    if (weatherForecasts.length === 0)
    {
        return (
            <div>
                Loading Weather Forecasts...
            </div>
        )
    }
    else
    {
        const tdStyle = {
            fontSize: '2.5vw'
        }

        const forecastStyle = {
            fontSize: '8vw'
        }
        
        const forecastSupStyle = {
            fontSize: '2vw'
        }

        var todaysForecast = weatherForecasts.find(wf => wf.deicticTime === "Today");
        var tomorrowsForecast = weatherForecasts.find(wf => wf.deicticTime === "Tomorrow");

        return (
            <table width="100%">
                <tbody>
                    <tr>
                        <td>
                            <div style={forecastStyle}>
                                <sup style={forecastSupStyle}>{todaysForecast?.deicticTime} &nbsp;</sup>
                                {todaysForecast?.minimumTemperature.toFixed(0)}°-{todaysForecast?.maximumTemperature.toFixed(0)}°<br></br>
                                {Math.round(todaysForecast?.maximumRainfall * 10) / 10} mm ({todaysForecast?.rainProbabilityPercentage}%)

                            </div>
                        </td>
                        <td>
                        <div style={forecastStyle}>
                                <sup style={forecastSupStyle}>{tomorrowsForecast?.deicticTime} &nbsp;</sup>
                                {tomorrowsForecast?.minimumTemperature.toFixed(0)}°-{tomorrowsForecast?.maximumTemperature.toFixed(0)}°<br></br>
                                {Math.round(tomorrowsForecast?.maximumRainfall * 10) / 10} mm ({tomorrowsForecast?.rainProbabilityPercentage}%)
                        </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        )
    }
}

export default Weather2DaysForecasts;