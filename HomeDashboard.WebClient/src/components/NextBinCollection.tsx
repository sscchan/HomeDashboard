import { useEffect, useState } from "react";

function NextBinCollection() 
{
    const DATA_FETCH_INTERVAL_IN_HOURS : number = 4;

    const [nextBinCollection, setNextBinCollection] = useState('');

    useEffect(() => {
        async function fetchNextBinCollection() {
            const response = await fetch("/api/WasteBins/next")
            var nextBin = await response.text();
            setNextBinCollection(nextBin);
        }

        // Initial data population
        fetchNextBinCollection();

        // Scheduled data refresh
        var timer = setInterval(() => fetchNextBinCollection(), DATA_FETCH_INTERVAL_IN_HOURS * 60 * 60 * 1000 )
        return function cleanup() {
            clearInterval(timer)
        }
    }, []); 

    if (nextBinCollection == '')
    {
        return (
            <div>
                Loading Next Bin Collection
            </div>
        )
    }
    else
    {
        const imgStyle = {
            width: "8vw"
        }

        return (
            <div>
                <img src={`./images/${nextBinCollection}.jpg`} style={imgStyle} />
            </div>
        )
    }
}

export default NextBinCollection;