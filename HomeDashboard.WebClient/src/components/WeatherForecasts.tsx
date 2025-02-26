import { useEffect, useState } from "react";

interface WeatherApiResponse {
    dateTime: Date;
    deicticTime: string;
    weatherDescription: string;
    rainProbabilityPercentage : number;
    minimumRainfall: number;
    maximumRainfall: number;
    minimumTemperature: number;
    maximumTemperature: number;
}

function WeatherForecasts() 
{
    const [weatherForecasts, setWeatherForecasts] = useState<Array<WeatherApiResponse>>([]);

    useEffect(() => {
        async function fetchWeatherForecasts() {
            const response = await fetch("/api/Weather");
            if (!ignore) {
                var weatherForecasts : Array<WeatherApiResponse> = await response.json();
                setWeatherForecasts(weatherForecasts);
            }
        }

        let ignore = false;
        fetchWeatherForecasts();
        return () => {
            ignore = true;
        };
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
        var weatherForecastDataRows = weatherForecasts.map(wf =>
            <tr key={wf.deicticTime}>
                <td>{wf.deicticTime}</td>
                <td>{wf.weatherDescription}</td>
                <td>{wf.rainProbabilityPercentage}% of {wf.minimumRainfall}-{wf.maximumRainfall}mm</td>
                <td>{wf.minimumTemperature}°C</td>
                <td>{wf.maximumTemperature}°C</td>
            </tr>
        );
        return (
            <table>
                <tbody>
                    <tr>
                        <th>Day</th>
                        <th>Weather</th>
                        <th>Rainfall</th>
                        <th>Min</th>
                        <th>Max</th>
                    </tr>
                    {weatherForecastDataRows}
                </tbody>
            </table>
        )
    }
}

export default WeatherForecasts;