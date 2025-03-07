import './App.css'
import Clock from './components/Clock'
import DashboardReloader from './components/DashboardReloader';
import NextBinCollection from './components/NextBinCollection'
import Weather7DaysForecasts from './components/Weather7DaysForecasts'
import SingleDayWeatherForecastAndObservation from './components/SingleDayWeatherForecastAndObservation';

function App() {

  const tableStyle = {
    tableLayout: 'fixed' as const,
    verticalAlign: 'middle' as const,
    width: '100%'
  };

  
  return (  
  <>
      <table style={tableStyle}>
        <tbody>
          <tr>
            <td width="15%">
              <NextBinCollection />
            </td>
            <td width="50%">
              <Clock />
            </td>
            <td width="35%">
              <SingleDayWeatherForecastAndObservation />
            </td>
          </tr>
        </tbody>
      </table>
      <br />
      <table style={tableStyle}>
        <tbody>
          <tr>
            <td>
              <Weather7DaysForecasts />
            </td>
          </tr>
          <tr>
            <td>
              <DashboardReloader />
            </td>
          </tr>
        </tbody>
      </table>
    </>
  )
}

export default App
