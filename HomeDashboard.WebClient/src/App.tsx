import { useState } from 'react'
import './App.css'
import NextBinCollection from './components/NextBinCollection'

function App() {
  const [count, setCount] = useState(0)

  return (  
    <>
      <div>
        <NextBinCollection />
      </div>
    </>
  )
}

export default App
