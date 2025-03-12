import './App.css'
import Clock from './components/Clock'
import DashboardReloader from './components/DashboardReloader';
import NextBinCollection from './components/NextBinCollection'
import Weather2DaysForecasts from './components/Weather2DaysForecasts'
import SingleDayWeatherObservation from './components/SingleDayWeatherObservation';

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
            <td width="10%">
              <NextBinCollection />
            </td>
            <td width="50%">
              <Clock />
            </td>
            <td width="40%">
              <SingleDayWeatherObservation />
            </td>
          </tr>
        </tbody>
      </table>
      <br />
      <table style={tableStyle}>
        <tbody>
          <tr>
            <td>
              <Weather2DaysForecasts />
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
