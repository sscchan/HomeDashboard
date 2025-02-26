import './App.css'
import Clock from './components/Clock'
import NextBinCollection from './components/NextBinCollection'
import WeatherForecasts from './components/WeatherForecasts'

function App() {

  return (  
    <>
      <div>
        <NextBinCollection />
        <Clock />
        <WeatherForecasts />
      </div>
    </>
  )
}

export default App
