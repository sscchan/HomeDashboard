import { useEffect, useState } from "react";

function Clock() 
{
    const [date, setDate] = useState(new Date());
    
    useEffect(() => {
        var timer = setInterval(() => setDate(new Date()), 5000 )
        return function cleanup() {
            clearInterval(timer)
        }
    });

    const clockStyle = {
        fontSize: '9vw'
    }
    return(
        <div style={clockStyle}>
            {date.getDate()}/{date.getMonth() + 1} &nbsp;&nbsp; 
            {date.getHours().toString().padStart(2,'0')}:{date.getMinutes().toString().padStart(2,'0')}
        </div>
    )
}

export default Clock;