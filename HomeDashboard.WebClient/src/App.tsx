import './App.css'
import Clock from './components/Clock'
import NextBinCollection from './components/NextBinCollection'
import WeatherForecasts from './components/WeatherForecasts'
import WeatherObservation from './components/WeatherObservation';

function App() {

  const tableStyle = {
    'table-layout': 'fixed',
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
              <WeatherObservation />
            </td>
          </tr>
        </tbody>
      </table>
      <table style={tableStyle}>
        <tbody>
          <tr>
            <td>
              <WeatherForecasts />
            </td>
          </tr>
        </tbody>
      </table>
    </>
  )
}

export default App
