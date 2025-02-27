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
        const tdStyle = {
            fontSize: '2.5vw'
        }
        var weatherForecastDataRows = weatherForecasts.map(wf =>
            <tr key={wf.deicticTime}>
                <td style={tdStyle} align="left">{wf.deicticTime}</td>
                <td style={tdStyle} align="left">{wf.weatherDescription}</td>
                <td style={tdStyle} align="left">{wf.rainProbabilityPercentage}% of {wf.minimumRainfall}-{wf.maximumRainfall}mm</td>
                <td style={tdStyle} align="left">{wf.minimumTemperature.toFixed(1)}°C</td>
                <td style={tdStyle} align="left">{wf.maximumTemperature.toFixed(1)}°C</td>
            </tr>
        );
        return (
            <table width="100%">
                <tbody>
                    <tr>
                        <th align="left"></th>
                        <th align="left"></th>
                        <th style={tdStyle} align="left">Rainfall</th>
                        <th style={tdStyle} align="left">Min</th>
                        <th style={tdStyle} align="left">Max</th>
                    </tr>
                    {weatherForecastDataRows}
                </tbody>
            </table>
        )
    }
}

export default WeatherForecasts;