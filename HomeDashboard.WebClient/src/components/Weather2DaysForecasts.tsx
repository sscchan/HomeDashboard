import { useEffect, useState } from "react";

interface WeatherForecastApiResponse {
    dateTime: Date;
    deicticTime: string;
    weatherIconName: string;
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
        const forecastStyle = {
            fontSize: '8vw',
            verticalAlign: 'middle' as const
        }

        const weatherIconStyle = {
            width: '15vw',
            position: "relative" as const,
            top: "50%",
            transform: 'translateY(30%)' as const
        }

        var todaysForecast = weatherForecasts.find(wf => wf.deicticTime === "Today");
        var tomorrowsForecast = weatherForecasts.find(wf => wf.deicticTime === "Tomorrow");

        return (
            <table width="100%">
                <tbody>
                    <tr>
                        <td>
                            <div style={forecastStyle}>
                                <img src={toIconImageUrl(todaysForecast ? todaysForecast?.weatherIconName : "")} style={weatherIconStyle} />
                                {todaysForecast?.minimumTemperature.toFixed(0)}°-{todaysForecast?.maximumTemperature.toFixed(0)}° <br/>
                                {todaysForecast ? Math.round(todaysForecast.maximumRainfall * 10) / 10 : undefined} mm ({todaysForecast?.rainProbabilityPercentage}%)
                            </div>
                        </td>
                        <td>
                        <div style={forecastStyle}>
                                <img src={toIconImageUrl(tomorrowsForecast ? tomorrowsForecast?.weatherIconName : "")} style={weatherIconStyle} />
                                {tomorrowsForecast?.minimumTemperature.toFixed(0)}°-{tomorrowsForecast?.maximumTemperature.toFixed(0)}° <br/>
                                {tomorrowsForecast ? Math.round(tomorrowsForecast.maximumRainfall * 10) / 10: undefined} mm ({tomorrowsForecast?.rainProbabilityPercentage}%)
                        </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        )
    }
}

function toIconImageUrl(iconName: string) : string
{
    return `https://beta.bom.gov.au/themes/custom/bom_theme/bom-react/dist/weather-icons/${iconName}.svg`;
}


export default Weather2DaysForecasts;