import { useEffect, useState } from "react";

function NextBinCollection() 
{
    const [nextBinCollection, setNextBinCollection] = useState('');

    useEffect(() => {
        async function fetchNextBinCollection() {
            const response = await fetch("/api/WasteBins/next")
            if (!ignore) {
                var nextBin = await response.text();
                setNextBinCollection(nextBin);
            }
        }

        let ignore = false;
        fetchNextBinCollection();
        return () => {
            ignore = true;
        };
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
            width: "12vw"
        }

        return (
            <div>
                <img src={`./images/${nextBinCollection}.jpg`} style={imgStyle} />
            </div>
        )
    }
}

export default NextBinCollection;