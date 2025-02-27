import './App.css'
import Clock from './components/Clock'
import NextBinCollection from './components/NextBinCollection'
import WeatherForecasts from './components/WeatherForecasts'

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
            <td width="25%">
              <NextBinCollection />
            </td>
            <td width="75%">
              <Clock />
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
